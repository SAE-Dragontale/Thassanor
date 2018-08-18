/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			UnitGroup.cs
   Version:			0.5.1
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
	[SerializeField] protected Transform _anchor;			// The target the group is following.
	public Transform _Anchor {
		set { _anchor = value?.Find("RallyPoint") ?? value; }
	}

	protected Vector3 _lastPosition = new Vector3();        // The last position of the target host. Check against to summise whether we've moved.
	protected bool _forcePositionUpdate = false;			// Whether we're ignoring the check and forcing the position to update.

	[SerializeField] protected UnitStyle _unitStyle;		// The type of unit that we contain.
	[SerializeField] protected GameObject _unitTemplate;	// The basic unit template.
	[SerializeField] protected Unit[] _everyUnit;			// The units within the group.

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
		Passive,	// Idle movement and will not engage in combat.
		Idle,       // Idle movement but will defend themselves.
		Active      // Stand at attention and engage all hostiles.
	};

	protected IEnumerator setMyLastPosition;   // Reclusive reference for UpdateLastPos().

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Data Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	[Space] [Header("Variables")]
	[SerializeField] protected GroupState _groupState;  // The current state of the group.

	[SerializeField] protected bool _permanent;    // Whether the group will Destroy() if it has no units.

	// Health functionality for UnitGroups.
	[SyncVar] [SerializeField] protected float _health;
	public float SetHealth {
		set {
			_health = value;
			UnitsFromHealth();
		}
	}

	[Space] [Header("Rules of Formations")]

	[Range(1, 30)]
	[SerializeField] protected int _formationColumns;   // This controls how many columns are present within the UnitGroup formation.

	[Range(0.5f,3)]
	[SerializeField] protected float _formationSpread;  // The amount of space in Vector Math between each unit. Horizontal and Vertical.

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Update().
	protected virtual void Start() {

		// If shit is broken, fucking warn me please.
		LogErrorsOnStart();

		// If a UnitGroup starts pre-initialised, we have to recognise this.
		UnitsFromChildren();

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

	// Change UnitGroup by Unit.
	public void AddUnit(int numberOf = 0) => ChangeHealth(_unitStyle._health * numberOf);
	public void MinusUnit(int numberOf = 0) => ChangeHealth(-_unitStyle._health * numberOf);

	// Change UnitGroup by Health.
	public void ChangeHealth(float _healthModification) => SetHealth = _health + _healthModification;

	/* ----------------------------------------------------------------------------- */

	// This function is used to update incremental changes to a Unit's health.
	protected virtual void UnitsFromHealth() {

		// Health should not drop below zero.
		_health = Mathf.Max(_health, 0);
		
		// How many units does our health indicate that we should have?
		int updatedHealth = Mathf.CeilToInt(_health / _unitStyle._health);

		// If our total health pool exceeds the maximum health pool of our units...
		if (updatedHealth  > _everyUnit.Length)
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

			GameObject unit = Instantiate(_unitTemplate, _anchor.position, Quaternion.identity, transform);
			unit.GetComponent<Unit>()._UnitStyle = _unitStyle;

		}

	}

	// We need to despawn units instead, which once again is implicitly networked.
	protected void UnitSubtractFromHealth(int updatedHealth) {

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

		_everyUnit = GetComponentsInChildren<Unit>();
		_forcePositionUpdate = true;

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Standard GroupCycle behaviour. Override functions that extend this cycle to change functionality.
	protected void Update() {

		// If we have no health remaining, then stop running functionality and 
		if (_health <= 0) {

			if (_permanent) { return; } 
			else { RemoveGroup(); }
			
		}

		switch (_groupState) {

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
	// Formation priority: All units maintain their formation and await further commands from their Host.

	protected virtual void BehaviourLoopActive() {

		if (NeedToUpdatePosition(_anchor.position))
			RulesOfFormation();

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
	// This is executed in place of the standard update loop when the UnitGroup has no health.

	protected virtual void RemoveGroup() {

		Destroy(gameObject);

	}

	/* ----------------------------------------------------------------------------- */
	// Before we do anything complicated, check whether we even need to update our position to start with.

	protected bool NeedToUpdatePosition(Vector3 positionToCheck) {

		// We can manually force a position update if we need to.
		if (_forcePositionUpdate) {
			_forcePositionUpdate = false;
			return true;
		}

		// Log our last position.
		if (setMyLastPosition == null)
			StartCoroutine(setMyLastPosition = UpdateLastPos(positionToCheck));

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
		setMyLastPosition = null;

	}

	/* ----------------------------------------------------------------------------- */
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
