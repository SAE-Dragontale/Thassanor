/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Billboarder.cs
   Version:			0.2.0
   Description: 	A simple script to automatically rotate sprites to face the player camera.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

public class Billboarder : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[SerializeField] private Transform _trCamera;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void Awake() {

		//_trCamera = _trCamera?.GetComponent<Transform>() ?? Camera.main.GetComponent<Transform>();
		_trCamera = Camera.main.GetComponent<Transform>();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Update is called once per frame
	private void FixedUpdate () {

		// We're simply asking the sprite to look towards the camera.
		// NOTE: I do not know why you need the (Vector3.forward * 180f). It does not work without it. ~Vector3(0,0,15) at least is needed, as Vector3.forward/back/up/down do literally nothing.

		transform.LookAt(_trCamera.position + _trCamera.transform.rotation * (Vector3.forward * 180f), _trCamera.transform.rotation * Vector3.up);	
		
	}

}