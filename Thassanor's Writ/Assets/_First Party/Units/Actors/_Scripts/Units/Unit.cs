/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Unit.cs
   Version:			0.3.2
   Description: 	The base container class for all non player character actors. This script handles invidiual behaviour, which is limited to: Visuals
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	[HideInInspector] public Transform _destination;	// Accessed by the UnitGroup in order to direct our unit to a formation.
	protected Vector3 _lastDestination;					// The last _trDestination.position that we were instructed to move towards.

	// These references are decoupled as a child from this script, however for all intents and purposes they are this script's components.
	protected Transform _tr;
	protected Animator _an;
	protected SpriteRenderer _sr;
	protected NavMeshAgent _ai;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	protected enum UnitState {
		Moving,	// When the unit is simply moving towards its destination.
		Attacking,	// When the unit is engaged in combat.
		Dead		// If the unit has been killed and is awaiting resurrection.
	}

	[SerializeField] protected UnitState _unitState;

	protected UnitStyle _unitStyle;

	public UnitStyle _UnitStyle {
		get { return _unitStyle; }
		set {
			_unitStyle = value;
			LoadUnitStyle();
		}
	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	protected void Awake() {

		// Access and save the transform of our Destination.
		_destination = transform.Find("Destination").GetComponent<Transform>();

		// Access and save the transform of our Unit as visualised.
		_tr = transform.Find("Sprite").GetComponent<Transform>();
		_an = _tr.GetComponent<Animator>();
		_sr = _tr.GetComponent<SpriteRenderer>();
		_ai = _tr.GetComponent<NavMeshAgent>();

	}

	// Assign our Visual UnitStyles to our character.
	protected void LoadUnitStyle() {

		_an.runtimeAnimatorController = _unitStyle._animatorController;

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */



	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	// Update is called once per frame.
	protected void Update () {

		switch (_unitState) {

			case (UnitState.Moving):
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

		AnimatorMovement();

		// If our new location is the same as our old location, don't run.
		if (_lastDestination == _destination.position)
			return;

		_ai?.SetDestination(_destination.position);
		_lastDestination = _destination.position;			

	}

	/* ----------------------------------------------------------------------------- */
	// Attacking

	protected void BehaviourLoopAttacking() {

		AnimatorMovement();

	}

	/* ----------------------------------------------------------------------------- */
	// Dead

	protected void BehaviourLoopDead() {

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Here we're making sure to update the current animator variables according to our movement.
	protected void AnimatorMovement() {

		// Change the facing of the sprite based on the x velocity.
		if (_ai.velocity.x < -0.25f)
			_sr.flipX = true;

		else if (_ai.velocity.x > 0.25f)
			_sr.flipX = false;

		// Then we'll use the "overall speed" of the velocity and convert it roughly into a percentage.
		_an.SetFloat("flVelocity01", Dragontale.MathFable.Remap( _ai.velocity.sqrMagnitude, 0f, 100f, 0f, 1.5f));

	}

	protected void AnimatorAttack() {



	}

	protected void AnimatorDeath() {



	}

	/* ----------------------------------------------------------------------------- */

}
