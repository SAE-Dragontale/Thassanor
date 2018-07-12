﻿/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			UnitGroup.cs
   Version:			0.1.0
   Description: 	The primary container for the Unit-Group-Controller. This handles groups of units and allocates mechanics between them.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections.Generic;
using UnityEngine;

public abstract class UnitGroup : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Space] [Header("References")]

	[Tooltip("The host is the transform that the UnitGroup is associated with. This can be a player, actor, or even a terrain set.")]
	[SerializeField] protected Transform _trHost;						// The target the group is following.

	[SerializeField] protected List<Transform> _ltrUnits;				// The units within the group.
	[SerializeField] protected List<Transform> _ltrUnitDestinations;	// The desired location of any units that might be in the group. 1:1 with _ltrUnits.

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public enum GroupState {
		Passive,    // Idle within an area.
		Standing,   // Stand at attention within an area.
		Following   // Follow a designated unit.
	};

	[Space] [Header("State")]
	[SerializeField] protected GroupState _groupState = GroupState.Standing;	// The current state of the group.

	[Space] [Header("Variables")]
	[SerializeField] protected float _flHealth;		// The health of the group.
	[SerializeField] protected bool _isKillable;	// Whether the group will Destroy() if it has no units.

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	protected virtual void Awake() {
		
		_trHost = _trHost.GetComponent<Transform>();

	}

	protected virtual void Start() {


	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Standard GroupCycle behaviour. Override functions that extend this cycle to change functionality.
	protected void Update() {

		if (_flHealth < 0)
			DissipateGroup();

		switch (_groupState) {

			case (GroupState.Passive):
				BehaviourLoopPassive();
				return;

			case (GroupState.Standing):
				BehaviourLoopStand();
				return;

			case (GroupState.Following):
				BehaviourLoopFollow();
				return;

		}

	}

	/* ----------------------------------------------------------------------------- */
	// Death

	protected virtual void DissipateGroup() {

		if (!_isKillable)
			return;

		Destroy(gameObject);

	}

	/* ----------------------------------------------------------------------------- */
	// Passive

	protected virtual void BehaviourLoopPassive() {

	}

	/* ----------------------------------------------------------------------------- */
	// Standing

	protected virtual void BehaviourLoopStand() {

	}

	/* ----------------------------------------------------------------------------- */
	// Follow

	protected virtual void BehaviourLoopFollow() {

	}


	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */



	/* ----------------------------------------------------------------------------- */

}
