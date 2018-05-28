/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharControls.cs
   Version:			0.0.0
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

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Speed Variables
	[SerializeField] private float _flSpeedMax;
	[SerializeField] private float _flSpeedIncrease;
	private float _flSpeedCurrent;
	private bool[] _isMoving = new bool[2];
	
	// Direction Variable
	private Vector3 _v3Trajectory = new Vector3 (0f,0f,0f);

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public void Awake() {

		// References
		_rb = GetComponent<Rigidbody>();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	public void TrajectoryChange(float[] flDirection) {

		// Apply Horizontal and Vertical Movement.
		//_v3Trajectory = new Vector3 (0f, _v3Trajectory.y, 0f);
		_v3Trajectory = new Vector3 (flDirection[1] * _flSpeedMax, _v3Trajectory.y, flDirection[0] * _flSpeedMax);


	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Update is called once per frame.
	private void Update () {
		
	}

	// Fixed Update is called once per fixed frame.
	private void FixedUpdate() {

		// Calculate 

		// Apply new Vector3 to velocity.
		_rb.velocity = _v3Trajectory;

	}

}