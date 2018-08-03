/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Unit.cs
   Version:			0.0.0
   Description: 	The base container class for all non player character actors. This script handles invidiual behaviour, which is limited to: Visuals
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class Unit : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	protected Transform _trThisUnit; // Our unit as represented in the game world. This is decoupled from this script.
	public Transform _trDestination; // Accessed by the UnitGroup in order to direct our unit to a formation.

	protected Animator _an;
	protected SpriteRenderer _sr;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public enum UnitState {
		Idling,		// When the unit has remained at its destination for long enough to 'idle'.
		Following,	// When the unit is simply moving towards its destination.
		Attacking,	// When the unit is engaged in combat.
		Killed		// If the unit has been killed and is awaiting resurrection.
	}

	[SerializeField] protected UnitState _unitState;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	private void Awake() {

		// Access and save the transform of our Destination.
		_trDestination = transform.Find("Destination").GetComponent<Transform>();

		// Access and save the transform of our Unit as visualised.
		_trThisUnit = transform.Find("Sprite").GetComponent<Transform>();
		_an = _trThisUnit.GetComponent<Animator>();
		_sr = _trThisUnit.GetComponent<SpriteRenderer>();

	}

	// Called before Update().
	private void Start () {
		
	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */



	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	// Update is called once per frame.
	protected void Update () {
		
		switch (_unitState) {

			case (UnitState.Idling):
				BehaviourLoopIdling();
				return;

			case (UnitState.Following):
				BehaviourLoopFollowing();
				return;

			case (UnitState.Attacking):
				BehaviourLoopAttacking();
				return;

			case (UnitState.Killed):
				BehaviourLoopKilled();
				return;

		}

	}

	/* ----------------------------------------------------------------------------- */
	// Idling

	protected void BehaviourLoopIdling() {

	}

	/* ----------------------------------------------------------------------------- */
	// Following

	protected void BehaviourLoopFollowing() {

	}

	/* ----------------------------------------------------------------------------- */
	// Attacking

	protected void BehaviourLoopAttacking() {

	}

	/* ----------------------------------------------------------------------------- */
	// Killed

	protected void BehaviourLoopKilled() {

	}

	/* ----------------------------------------------------------------------------- */

}
