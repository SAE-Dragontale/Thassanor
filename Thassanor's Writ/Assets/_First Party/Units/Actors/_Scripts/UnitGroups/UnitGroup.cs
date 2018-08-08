/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			UnitGroup.cs
   Version:			0.1.0
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
	[SerializeField] protected Transform _trHost;           // The target the group is following.
	protected Vector3 _v3LastPos = new Vector3(0, 0, 0);        // The last position of the target host. Check against to summise whether we've moved.

	public Transform _TrHost {
		set { _trHost = value?.Find("RallyPoint") ?? value; }
	}

	[Tooltip("The type of unit that this group control.s")]
	[SerializeField] protected GameObject _goUnitType;

	[Tooltip("All units currently associated with this group.")]
	[SerializeField] protected List<Unit> _lscUnits;    // The units within the group.

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	protected enum GroupState {
		Idle,       // Idle movement when abandoned.
		Active      // Stand at attention within an area.
	};

	[Space] [Header("State")]
	[SerializeField] protected GroupState _groupState;  // The current state of the group.

	protected IEnumerator _ieLastPos;   // Our position one fixed frame ago.

	[Space] [Header("Variables")]
	[SerializeField] protected bool _isKillable;    // Whether the group will Destroy() if it has no units.

	protected float _flUnitHealth;      // How much health does each unit individually contribute?
	protected float _flCurrentHealth;   // The current total pool of health the UnitGroup currently has.

	[Space] [Header("Rules of Formations")]
	[SerializeField] protected bool _usesRoF;       // If false, we're looking at mass chaos as every unit vies for host senpai's rally point.

	[Range(1, 10)]
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

		foreach (Unit unit in GetComponentsInChildren<Unit>()) {

			_lscUnits.Add(unit);

		}

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public void ModifyHealth(float itHealthModifier) {



	}

	public void AddUnit(GameObject goNewUnit) {

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

		PositionUnits();

		switch (_groupState) {

			case (GroupState.Active):
				BehaviourLoopActive();
				return;

			case (GroupState.Idle):
				BehaviourLoopIdle();
				return;

		}

	}

	/* ----------------------------------------------------------------------------- */
	// Formation priority: All units maintain their formation and await further commands from their Host.

	protected virtual void BehaviourLoopActive() {

		PositionUnits();

	}

	/* ----------------------------------------------------------------------------- */
	// Unit priority: All units gain 'freedom of movement' within the Host's Area of Influence.

	protected virtual void BehaviourLoopIdle() {

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

	protected virtual void PositionUnits() {

		// Log our last position.
		if (_ieLastPos == null)
			StartCoroutine(_ieLastPos = UpdateLastPos(_trHost.position));

		// If we are at the same location as before, we don't need to update our unit's destinations.
		if (_trHost.position == _v3LastPos)
			return;

		// If we have no units, we don't need to organise anyone.
		if (_lscUnits.Count == 0)
			return;

		// Now that we've determined we need to update our position, we can begin to initialise any variables we need.

		int itRemainingUnits = _lscUnits.Count; // The units that are currently unpositioned in our iteration.
		int itCurrentUnitIndex = 0;             // How many units we've positioned so far by array index.

		// As long as we've still got unpositioned units, we should continue iterating based on the number of units we have left.
		while (itRemainingUnits > 0) {

			// We only want to iterate on units row by row. For each row, we add units to a currently selected pile, then place those and go back one row.
			int itSelectedUnits = Mathf.Min(itRemainingUnits, _itRoFColumns);

			for (int it = 0; it < itSelectedUnits; it++) {

				// First, we need to space each unit out evenly, so we take the total units, divide it in half, and add our array position as an offset between Spread.
				// Then, we do basically the same thing backwards by dividing the amount of rows we already have and offsetting us by Spread.
				// Finally, we need to add our calculated vector position to the local vector of the Rally Point to add rotational values easily.

				float flPlaceAcross = (((itSelectedUnits - 1) * _flRoFSpread) / 2 * -1) + (_flRoFSpread * it);
				float flPlaceBehind = (itCurrentUnitIndex / _itRoFColumns) * _flRoFSpread;
				
				_lscUnits[it + itCurrentUnitIndex]._trDestination.position = _trHost.TransformPoint(flPlaceAcross, 0, flPlaceBehind);

			}

			// We've now placed these units, so shelve them for the meantime and increment how many loops we've performed.
			itRemainingUnits -= itSelectedUnits;
			itCurrentUnitIndex += itSelectedUnits;

		}

	}

	// We use this to help determine the direction that the UnitGroup should face towards.
	protected IEnumerator UpdateLastPos(Vector3 v3) {

		yield return new WaitForFixedUpdate();

		_v3LastPos = v3;
		_ieLastPos = null;

	}

	// We can choose to diffuse our formation's positions by overwriting this function.
	protected virtual Vector3 PositionVariance(Vector3 v3) {

		return v3;

	}

	/* ----------------------------------------------------------------------------- */

}
