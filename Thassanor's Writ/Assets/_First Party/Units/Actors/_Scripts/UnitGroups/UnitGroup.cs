/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			UnitGroup.cs
   Version:			0.6.0
   Description: 	The primary container for the Unit-Group-Controller. This handles groups of units and allocates mechanics between them.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class UnitGroup : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Space] [Header("References")]

	[Tooltip("The host is the transform that the UnitGroup is associated with. This can be a player, actor, or even a terrain set.")]
	[SerializeField] protected Transform _trHost;			// The target the group is following.
	protected Vector3 _v3LastPos = new Vector3(0, 0, 0);	// The last position of the target host. Check against to summise whether we've moved.

	public Transform _TrHost {
		set { _trHost = value?.Find("RallyPoint") ?? value; }
	}

	[Tooltip("The type of unit that this group control.s")]
	[SerializeField] protected UnitStyle _unitStyle;

	[Tooltip("All units currently associated with this group.")]
	[SerializeField] protected List<Unit> _lscUnits;    // The units within the group.

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Method Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	protected enum GroupState {
		Passive,	// Idle movement and will not engage in combat.
		Idle,       // Idle movement but will defend themselves.
		Active      // Stand at attention and engage all hostiles.
	};

	protected IEnumerator _ieLastPos;   // Reclusive reference for UpdateLastPos().

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Data Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	[Space] [Header("Variables")]
	[SerializeField] protected GroupState _groupState;  // The current state of the group.

	[SerializeField] protected bool _isKillable;    // Whether the group will Destroy() if it has no units.

	protected float _flUnitHealth;      // How much health does each unit individually contribute?
	protected float _flCurrentHealth;   // The current total pool of health the UnitGroup currently has.

	[Space] [Header("Rules of Formations")]
	[SerializeField] protected bool _usesRoF;       // If false, we're looking at mass chaos as every unit vies for host senpai's rally point.

	[Range(1, 20)]
	[SerializeField] protected int _itRoFColumns;   // This controls how many columns are present within the UnitGroup formation.
	[SerializeField] protected float _flRoFSpread;  // The amount of space in Vector Math between each unit. Horizontal and Vertical.

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	protected virtual void Awake() {

	}

	// Called before Update().
	protected virtual void Start() {

		ReloadUnits();

	}

	[InspectButton]
	public virtual void ReloadUnits() {

		_lscUnits.Clear();

		foreach (Unit unit in GetComponentsInChildren<Unit>()) {

			_lscUnits.Add(unit);
			unit._UnitStyle = _unitStyle;

		}

		

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	/* ----------------------------------------------------------------------------- */
	// Creation

	

	/* ----------------------------------------------------------------------------- */
	// Combat

	public void ModifyHealth(float itHealthModifier) {



	}

	public void ModifyUnitCount(int it) {

		// Instantiate(goNewUnit);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Standard GroupCycle behaviour. Override functions that extend this cycle to change functionality.
	protected void Update() {

		if (_flCurrentHealth <= 0) {
			if (_isKillable) {

				RemoveGroup();
				return;
			}
		}

		NewPosition();

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

		NewPosition();

	}

	/* ----------------------------------------------------------------------------- */
	// Unit priority: All units gain 'freedom of movement' within the Host's Area of Influence.

	protected virtual void BehaviourLoopIdle() {

		// No functionality here.

	}

	/* ----------------------------------------------------------------------------- */
	// No priority: All units gain 'freedom of movement' and will not engage in combat.

	protected virtual void BehaviourLoopPassive() {

		// No functionality here.

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	/* ----------------------------------------------------------------------------- */
	// This is executed in place of the standard update loop when the UnitGroup has no health.

	protected virtual void RemoveGroup() {

		Destroy(gameObject);

	}

	/* ----------------------------------------------------------------------------- */
	// Move our group towards the Host's Rally Point and adjust our formation as required.

	protected void NewPosition() {

		// Log our last position.
		if (_ieLastPos == null)
			StartCoroutine(_ieLastPos = UpdateLastPos(_trHost.position));

		// If we are at the same location as before, we don't need to update our unit's destinations.
		if (_trHost.position == _v3LastPos)
			return;

		// If we have no units, we don't need to organise anyone.
		if (_lscUnits.Count == 0)
			return;

		// And finally, now we're sure we need to update our position, we choose our destination style.
		if (_usesRoF)
			RulesOfFormation();
		else
			RulesOfChaos();

	}

	// The "Rules of Formation" provide a framework position for the units to move to as an army. They'll be arrayed in columns and rows.
	protected virtual void RulesOfFormation() {

		int itRemainingUnits = _lscUnits.Count; // The units that are currently unpositioned in our iteration.
		int itCurrentUnitIndex = 0;             // How many units we've positioned so far by array index.

		// As long as we've still got unpositioned units, we should continue iterating based on the number of units we have left.
		while (itRemainingUnits > 0) {

			// We only want to iterate on units row by row. For each row, we add units to a currently selected pile, then place those and go back one row.
			int itSelectedUnits = Mathf.Min(itRemainingUnits, _itRoFColumns);

			for (int it = 0; it < itSelectedUnits; it++) {

				// First, we need to space each unit out evenly, so we take the total units, divide it in half, and add our array position as an offset.
				// Then, we do basically the same thing backwards by dividing the amount of rows we already have and offsetting us by Spread.
				// Finally, we need to add our calculated vector position to the local vector of the Rally Point to add rotational values easily.

				float flPlaceAcross = (((itSelectedUnits - 1) * _flRoFSpread) / 2 * -1) + (_flRoFSpread * it);
				float flPlaceBehind = (itCurrentUnitIndex / _itRoFColumns) * _flRoFSpread;

				_lscUnits[it + itCurrentUnitIndex]._trDestination.position = PositionVariance(_trHost.TransformPoint(flPlaceAcross, 0, flPlaceBehind));

			}

			// We've now placed these units, so shelve them for the meantime and increment how many loops we've performed.
			itRemainingUnits -= itSelectedUnits;
			itCurrentUnitIndex += itSelectedUnits;

		}

	}

	// For some reason this unit does not care for formation and will jam themselves as close as possible to the destination as they can.
	protected virtual void RulesOfChaos() {

		foreach (Unit unit in _lscUnits) {

			unit._trDestination.position = PositionVariance(_trHost.TransformPoint(0,0,0));

		}

	}

	// We use this to determine whether we should update the UnitGroup Destinations, or instead return to be more efficient.
	protected IEnumerator UpdateLastPos(Vector3 v3) {

		yield return new WaitForFixedUpdate();

		_v3LastPos = v3;
		_ieLastPos = null;

	}

	// We can choose to diffuse our formation's position's by overwriting this function.
	protected virtual Vector3 PositionVariance(Vector3 v3) {

		return v3;

	}

	/* ----------------------------------------------------------------------------- */

}
