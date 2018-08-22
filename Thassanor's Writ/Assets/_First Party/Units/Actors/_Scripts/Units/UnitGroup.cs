/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			UnitGroup.cs
   Version:			0.7.0
   Description: 	The primary container for the Unit-Group-Controller. This handles groups of units and allocates mechanics between them.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class UnitGroup : NetworkBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Space] [Header("References")]

	[Tooltip("The host is the transform that the UnitGroup is associated with. This can be a player, actor, or even a terrain set.")]
	[SerializeField] protected Transform _anchor;           // The target the group is following.
	public Transform _Anchor {
		set { _anchor = value?.Find("RallyPoint") ?? value; }
	}

	protected Vector3 _lastPosition = new Vector3();        // The last position of the target host. Check against to summise whether we've moved.
	protected bool _forcePositionUpdate = false;            // Whether we're ignoring the check and forcing the position to update.

	[SerializeField] protected UnitStyle _unitStyle;        // The type of unit that we contain.
	[SerializeField] protected GameObject _unitTemplate;    // The basic unit template.
	[SerializeField] protected Unit[] _everyUnit;           // The units within the group.

	public UnitStyle _UnitStyle {
		set {
			_unitStyle = value;
			UnitsFromChildren();
		}
	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Method Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	protected enum GroupState {
		Passive,    // Idle movement and will not engage in combat.
		Idle,       // Idle movement but will engage like Active.
		Active,     // Stand at attention and engage all hostiles.
		Aggro       // Currently aggro'd onto an enemy and engaging.
	};

	protected IEnumerator _setMyLastPosition;   // Reclusive reference for UpdateLastPos().

	protected IEnumerator _sitRep;              // This is used to control how often we recheck for opposing collisions after we've started to fight.
	protected IEnumerator _dealDamagePlayer;	// This is used to increment damage over time to an opponent.
	protected IEnumerator _dealDamageUnit;		// 

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Data Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Space]
	[Header("Status")]

	[SerializeField] protected GroupState _groupState;      // The current state of the group.
	[SerializeField] protected GroupState _defaultState;    // The 'original' state of the group.

	[SerializeField] protected bool _permanent;             // Whether the group is removed when it's health reaches 0, or simply just waits until populated again.
	[SyncVar] [SerializeField] protected float _health;     // The health is a direct sum of all units within. Each health increment is effectively one unit.

	public float Health {
		set { _health = value; UnitsFromHealth(); }
		get { return _health; }
	}

	[Space]
	[Header("Offensive Statistics")]

	[SerializeField] protected Collider _target;            // The reference to our area-scan target. While this is not null, we are fighting.

	[Space]
	[SerializeField] protected float _aggroRange;           // The range at which it is acceptable to assault your enemies.
	[SerializeField] protected float _disengageRange;       // The range at which is considered "too far" to continue fighting.

	[Space]
	[SerializeField] protected float _attackRate;			// The delay in seconds between dealing damage.
	[SerializeField] protected float _combatTickRate;		// How evenly is our damage distributed between our _attackRate ticks.
	[SerializeField] protected float _checkToContinue;      // How often we check to see if we should continue fighting.

	[Space] [Header("Rules of Formations")]

	[Range(1, 30)]
	[SerializeField] protected int _formationColumns;   // This controls how many columns are present within the UnitGroup formation.

	[Range(0.5f, 3)]
	[SerializeField] protected float _formationSpread;  // The amount of space in Vector Math between each unit. Horizontal and Vertical.

	private Vector3 _nextSpawn;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Update().
	protected virtual void Start() {

		LogErrorsOnStart(); // If shit is broken, fucking warn me please.
		UnitsFromChildren(); // If a UnitGroup starts pre-initialised, we have to recognise this.

		_defaultState = _groupState;

	}

	// Just make a few quick checks to safeguard us from err.
	protected virtual void LogErrorsOnStart() {

		if (_unitStyle._health == 0)
			Debug.LogError($"The health value for your UnitStyle cannot be {_unitStyle._health}.");

		if (_formationColumns == 0)
			Debug.LogError($"_formationsColumns cannot be {_formationColumns++}. Changed to {_formationColumns}.");

		if (_formationSpread == 0)
			Debug.LogError($"It is not wise to have a Rules of Formation spread value of {_formationSpread}.");

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Adding units takes the optional 'location' parameter, as the often-use case for this is to spawn one unit at a time.
	public void AddUnit(int numberOf = 1, Vector3? spawnLocation = null) {

		_nextSpawn = spawnLocation ?? _anchor.position;
		ChangeHealth(_unitStyle._health * numberOf);

	}

	// These health increments will always default to _anchor.position as their spawn location for ease of use.
	public void MinusUnit(int numberOf = 1) => ChangeHealth(-_unitStyle._health * numberOf);
	public void ChangeHealth(float _healthModification) => Health += _healthModification;

	/* ----------------------------------------------------------------------------- */

	// This function is used to update incremental changes to a Unit's health.
	protected virtual void UnitsFromHealth() {

		// Health should not drop below zero.
		_health = Mathf.Max(_health, 0);

		// How many units does our health indicate that we should have?
		int updatedHealth = Mathf.CeilToInt(_health / _unitStyle._health);

		// If our total health pool exceeds the maximum health pool of our units...
		if (updatedHealth > _everyUnit.Length)
			UnitAddFromHealth(updatedHealth);

		// If our total health pool leaves a unit without any remaining health...
		else if (updatedHealth < _everyUnit.Length)
			UnitSubtractFromHealth(updatedHealth);

		// Update our UnitList with the hierarchy components.
		UpdateUnitList();

	}

	// We need to spawn units here then. This doesn't need networking commands because it is implicitly networked with a SyncVar.
	protected void UnitAddFromHealth(int updatedHealth) {

		for (int i = updatedHealth - _everyUnit.Length; i > 0; i--) {

			GameObject unit = Instantiate(_unitTemplate, _nextSpawn, Quaternion.identity, transform);
			unit.GetComponent<Unit>()._UnitStyle = _unitStyle;

		}

	}

	// We need to despawn units instead, which once again is implicitly networked.
	protected void UnitSubtractFromHealth(int updatedHealth) {

		// Don't reduce health if we're already fookin' dead mate.
		if (_health < 1)
			return;

		// Because Destroy doesn't immediately destroy the object on the current frame, we need to remove it as a parent for the remainder of our code to funciton.
		for (int i = _everyUnit.Length - updatedHealth; i > 0; i--) {

			_everyUnit[i].transform.parent = null;
			Destroy(_everyUnit[i].gameObject);

		}

	}

	/* ----------------------------------------------------------------------------- */

	// This function is used to update a collection of units, generally from the editor.
	protected void UnitsFromChildren() {

		// Update our UnitList with the hierarchy components.
		UpdateUnitList();

		// Load in our initial health value.
		_health = _unitStyle._health * _everyUnit.Length;

		// Then iterate through our units and apply our current style to each unit.
		foreach (Unit unit in _everyUnit)
			unit._UnitStyle = _unitStyle;

	}

	/* ----------------------------------------------------------------------------- */

	protected void UpdateUnitList() {

		// Refresh our currently generated list of units.
		_everyUnit = GetComponentsInChildren<Unit>();

		// Make sure we update and reset our default values for the next loop.
		_forcePositionUpdate = true;
		_nextSpawn = _anchor.position;

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Standard GroupCycle behaviour. Override functions that extend this cycle to change functionality.
	protected void Update() {

		// If we have no health remaining, then stop running functionality and 
		if (_health <= 0) {

			if (!_permanent)
				DestroyNonPermanent();

			return;

		}

		switch (_groupState) {

			case (GroupState.Aggro):
				BehaviourLoopAggro();
				return;

			case (GroupState.Active):
				BehaviourLoopActive();
				return;

			case (GroupState.Idle):
				BehaviourLoopIdle();
				return;

			case (GroupState.Passive):
				BehaviourLoopPassive();
				return;

		}

	}

	/* ----------------------------------------------------------------------------- */
	// Unit priority: All units gain 'freedom of movement' within the Host's Area of Influence.

	protected virtual void BehaviourLoopAggro() {

		if (_sitRep == null)
			SitRep();

		if (_target == null)
			CancelAssault();

		else
			ThreatAssessement(_target);

	}

	/* ----------------------------------------------------------------------------- */
	// Formation priority: All units maintain their formation and await further commands from their Host.

	protected virtual void BehaviourLoopActive() {

		if (NeedToUpdatePosition(_anchor.position))
			RulesOfFormation();

		WatchForHostiles(_aggroRange);

	}

	/* ----------------------------------------------------------------------------- */
	// Unit priority: All units gain 'freedom of movement' within the Host's Area of Influence.

	protected virtual void BehaviourLoopIdle() { }

	/* ----------------------------------------------------------------------------- */
	// No priority: All units gain 'freedom of movement' and will not engage in combat.

	protected virtual void BehaviourLoopPassive() { }

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	/* ----------------------------------------------------------------------------- */
	// Surroundings functions.

	// Scans the area for the opposing player.
	protected virtual Collider[] ScanForHostiles(int layer, float radius) => Physics.OverlapSphere(transform.position, _aggroRange, 1 << layer, QueryTriggerInteraction.Collide);

	// Checks the situation every few seconds and orders a rescan of the area at an enlarged radius.
	protected IEnumerator SitRep() {

		yield return new WaitForSeconds(_checkToContinue);
		WatchForHostiles(_disengageRange);

		_sitRep = null;

	}

	// Called to assess the current situation after an area scan.
	protected virtual void WatchForHostiles(float range) {

		_target = null;

		Collider[] thingsWithinAggro = ScanForHostiles(12, range);

		if (thingsWithinAggro.Length == 0)
			return;

		_target = thingsWithinAggro[0];
		_groupState = GroupState.Aggro;

	}

	// When we're aggro'd, we call constant threat assessements to choose our priority target.
	// Warriors get chosen first, then Archers, and then finally the Necromancer themselves.

	protected virtual void ThreatAssessement(Collider threat) {

		var everyHostile = threat.transform.parent.GetComponent<CharSpells>()._minions;

		foreach (UnitGroup hostile in everyHostile) {
			if (hostile._health > 0) {

				TargetMinions(hostile);
				return;

			}
		}

		TargetNecromancer(threat.transform.parent.GetComponent<CharStats>());

	}

	// Targetting a unitgroup of the opposing player.

	protected void TargetMinions(UnitGroup currentTarget) {

		if (_dealDamagePlayer != null)
			CancelPlayerDamage();

		if (_dealDamageUnit == null)
			StartCoroutine(DealDamageUnit(currentTarget));

		for (int i = 0; i < _everyUnit.Length; i++)
			FindTarget(currentTarget._everyUnit);

	}

	protected IEnumerator DealDamageUnit(UnitGroup currentTarget) {

		while (true) {
			yield return new WaitForSeconds(_attackRate / _everyUnit.Length);
			currentTarget.Health -= _unitStyle._damage;
		}
	}

	// Targeting the opposing Player.

	protected void TargetNecromancer(CharStats enemyNecromancer) {

		if (_dealDamageUnit != null)
			CancelUnitDamage();

		if (_dealDamagePlayer == null)
			StartCoroutine(DealDamagePlayer(enemyNecromancer));
		
		for (int i = 0; i < _everyUnit.Length; i++)
			MoveUnit(i, FindTarget(enemyNecromancer));

	}

	protected IEnumerator DealDamagePlayer(CharStats enemyNecromancer) {

		while (true) {
			yield return new WaitForSeconds(_attackRate / _everyUnit.Length);
			enemyNecromancer._playerHealth -= _unitStyle._damage;
		}
	}

	// Position our units so they visually represent what is going on.
	protected Vector3 FindTarget(Unit[] targets) => targets[Random.Range(0, targets.Length - 1)].transform.position;
	protected Vector3 FindTarget(CharStats target) => target.transform.position;

	// End whatever madness that we've started.

	protected void CancelAssault() {
		
		CancelUnitDamage();
		CancelPlayerDamage();		

		_groupState = _defaultState;

	}

	protected void CancelUnitDamage() {
		StopCoroutine(DealDamageUnit(null));
		_dealDamageUnit = null;
	}

	protected void CancelPlayerDamage() {
		StopCoroutine(DealDamagePlayer(null));
		_dealDamagePlayer = null;
	}

	/* ----------------------------------------------------------------------------- */
	// Health functions.

	// This is executed in place of the standard update loop when the UnitGroup has no health.
	protected virtual void DestroyNonPermanent() => Destroy(gameObject);

	/* ----------------------------------------------------------------------------- */
	// Movement Functions

	// Before we do anything complicated, check whether we even need to update our position to start with.
	protected bool NeedToUpdatePosition(Vector3 positionToCheck) {

		// We can manually force a position update if we need to.
		if (_forcePositionUpdate) {
			_forcePositionUpdate = false;
			return true;
		}

		// Log our last position.
		if (_setMyLastPosition == null)
			StartCoroutine(_setMyLastPosition = UpdateLastPos(positionToCheck));

		// If we are at the same location as before, we don't need to update our unit's destinations.
		if (positionToCheck == _lastPosition)
			return false;

		// If we have no units, we don't need to organise anyone.
		if (_everyUnit.Length == 0)
			return false;

		return true;

	}

	// We use this to determine whether we should update the UnitGroup Destinations, or instead return to be more efficient.
	protected IEnumerator UpdateLastPos(Vector3 lastPosition) {

		yield return new WaitForFixedUpdate();

		_lastPosition = lastPosition;
		_setMyLastPosition = null;

	}

	// Move our group towards the Host's Rally Point and adjust our formation as required.
	// The "Rules of Formation" provide a framework position for the units to move to as an army. They'll be arrayed in columns and rows.

	protected void RulesOfFormation() {

		int remainingUnits = _everyUnit.Length; // The units that are currently unpositioned in our iteration.
		int currentUnitIndex = 0;             // How many units we've positioned so far by array index.

		// As long as we've still got unpositioned units, we should continue iterating based on the number of units we have left.
		while (remainingUnits > 0) {

			// We only want to iterate on units row by row. For each row, we add units to a currently selected pile, then place those and go back one row.
			int selectedUnits = Mathf.Min(remainingUnits, _formationColumns);

			for (int i = 0; i < selectedUnits; i++) {

				// First, we need to space each unit out evenly, so we take the total units, divide it in half, and add our array position as an offset.
				// Then, we do basically the same thing backwards by dividing the amount of rows we already have and offsetting us by Spread.
				// Finally, we need to add our calculated vector position to the local vector of the Rally Point to add rotational values easily.

				float positionAcross = (((selectedUnits - 1) * _formationSpread) / 2 * -1) + (_formationSpread * i);
				float positionBehind = (currentUnitIndex / _formationColumns) * _formationSpread;

				MoveUnit(currentUnitIndex + i, _anchor.TransformPoint(positionAcross, 0, positionBehind));

			}

			// We've now placed these units, so shelve them for the meantime and increment how many loops we've performed.
			remainingUnits -= selectedUnits;
			currentUnitIndex += selectedUnits;

		}

	}

	// We can choose to diffuse our formation's position's by overwriting this function.
	protected virtual Vector3 PositionVariance(Vector3 position) => position;

	// And finally, a quick shorthand function to actually move the unit. This is just for clean reading.
	protected void MoveUnit(int unit, Vector3 moveTo) {

		_everyUnit[unit]._destination.position = PositionVariance(moveTo);

	}

	/* ----------------------------------------------------------------------------- */

}
