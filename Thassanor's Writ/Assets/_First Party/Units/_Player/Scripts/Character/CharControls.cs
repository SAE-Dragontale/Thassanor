/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharControls.cs
   Version:			0.1.0
   Description: 	Recieves movement input and controls the player's position.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharVisuals))]
public class CharControls : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private Rigidbody _rb;
	private CharVisuals _charVisuals;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Movement Variables
	[SerializeField] private float _flMovementSpeed;
	private Vector3 _v3Trajectory = new Vector3 (0f,0f,0f);

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public void Awake() {

		// References
		_rb = GetComponent<Rigidbody>();
		_charVisuals = GetComponent<CharVisuals>();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	public void TrajectoryChange(float[] flDirection) {

		// First, we align our new trajectory with the direction.
		_v3Trajectory = new Vector3 (flDirection[1] * _flMovementSpeed, _v3Trajectory.y, flDirection[0] * _flMovementSpeed);

		// Then, since we have a new command, we send that variable to the Animator to adjust the visuals.
		_charVisuals.AnimMovement(flDirection);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Update is called once per frame.
	private void Update () {
		
	}

	// Fixed Update is called once per fixed frame.
	private void FixedUpdate() {

		// Apply new Vector3 to velocity.
		_rb.velocity += _v3Trajectory.normalized;

	}

}