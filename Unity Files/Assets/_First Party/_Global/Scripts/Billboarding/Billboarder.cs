/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Billboarder.cs
   Version:			0.2.2
   Description: 	A simple script to automatically rotate sprites to face the player camera.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

public class Billboarder : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	protected Transform _trCamera;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start()
	protected void Awake() {

		//_trCamera = _trCamera?.GetComponent<Transform>() ?? Camera.main.GetComponent<Transform>();
		_trCamera = Camera.main.GetComponent<Transform>();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Update is called once per frame
	protected void FixedUpdate () {

		Billboard(transform);
		
	}

	// Called whenevger we need to billboard something.
	protected void Billboard(Transform transformToBillboard) {

		// We're simply asking the sprite to look towards the camera.
		transformToBillboard.LookAt(transform.position + _trCamera.transform.rotation * Vector3.forward, _trCamera.transform.rotation * Vector3.up);

	}

}