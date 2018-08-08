/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Unit.cs
   Version:			0.2.0
   Description: 	The base container class for all non player character actors. This script handles invidiual behaviour, which is limited to: Visuals
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	[HideInInspector] public Transform _trDestination;	// Accessed by the UnitGroup in order to direct our unit to a formation.
	protected Vector3 _v3PosToMove;						// The last _trDestination.position that we were instructed to move towards.

	// These references are decoupled as a child from this script, however for all intents and purposes they are this script's components.
	protected Transform _tr;
	protected Animator _an;
	protected SpriteRenderer _sr;
	protected NavMeshAgent _ai;

	protected UnitStyle _unitStyle;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	protected enum UnitState {
		Following,	// When the unit is simply moving towards its destination.
		Attacking,	// When the unit is engaged in combat.
		Dead		// If the unit has been killed and is awaiting resurrection.
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
		_tr = transform.Find("Sprite").GetComponent<Transform>();
		_an = _tr.GetComponent<Animator>();
		_sr = _tr.GetComponent<SpriteRenderer>();
		_ai = _tr.GetComponent<NavMeshAgent>();

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

			case (UnitState.Following):
				BehaviourLoopFollowing();
				return;

			case (UnitState.Attacking):
				BehaviourLoopAttacking();
				return;

			case (UnitState.Dead):
				BehaviourLoopDead();
				return;

		}

	}

	/* ----------------------------------------------------------------------------- */
	// Following

	protected void BehaviourLoopFollowing() {

		// If our new location is the same as our old location, don't run.
		if (_v3PosToMove == _trDestination.position)
			return;

		// Instruct the Nav Agent to move towards our new location.
		if (_unitState == UnitState.Following)
			_ai?.SetDestination(_trDestination.position);

	}

	/* ----------------------------------------------------------------------------- */
	// Attacking

	protected void BehaviourLoopAttacking() {

	}

	/* ----------------------------------------------------------------------------- */
	// Dead

	protected void BehaviourLoopDead() {

	}

	/* ----------------------------------------------------------------------------- */

}
