/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharControls.cs
   Version:			0.3.0
   Description: 	Recieves movement input and controls the player's position.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

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
	
	public void TrajectoryChange(float[] flDirection = null) {

		// Any null answers should be considered "Halt" commands
		if (flDirection == null)
			_v3Trajectory = new Vector3(0f, _v3Trajectory.y, 0f);

		// Otherwise, consider the input as normal and add the values to each direction as needed.
		else
			_v3Trajectory = new Vector3 (flDirection[1] * _flMovementSpeed, _v3Trajectory.y, flDirection[0] * _flMovementSpeed);

		// Then, since we have a new command, we send that variable to the Animator to adjust the visuals.
		_charVisuals.AnimMovement(flDirection);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Fixed Update is called once per fixed frame.
	private void FixedUpdate() {

		EricMapDebug();

		// Apply new Vector3 to velocity.
		_rb.velocity += _v3Trajectory.normalized;

	}

	private void EricMapDebug() {

		RaycastHit hit;
		if (Physics.Raycast(transform.position, -Vector3.up, out hit) && hit.transform.tag == "Water") {
			Debug.Log("Water");
		}

	}

}