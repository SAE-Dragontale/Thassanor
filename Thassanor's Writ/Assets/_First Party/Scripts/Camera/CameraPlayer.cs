/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CameraPlayer.cs
   Version:			0.0.0
   Description: 	The player-follow camera. Has all basic functions to smoothly follow the player.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : CameraBase {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[SerializeField] List<Transform> _ltrCameraFocus;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// The Camera Offset being applied to the position.
	[SerializeField] private Vector3 _v3CameraOffset;

	// The speed at which the camera snaps to the target its viewing.
	[SerializeField] private float _flCameraSnap;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Foobar

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	// Foobar

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Update is called once per frame
	void FixedUpdate () {

		Vector3 v3Desired = new Vector3 (0,0,0);

		// Find the MidPoint of all listed variables.
		int itNullCount = 0;

		foreach (Transform tr in _ltrCameraFocus) {
			if (tr != null) {
				v3Desired += tr.position;
			} else {
				itNullCount++;
			}
		}

		v3Desired = v3Desired / (_ltrCameraFocus.Count -itNullCount);

		// Now, we can have the camera point towards, and look at, the player. We do this before adding offset.
		transform.LookAt (v3Desired);

		// Then, add any current Camera Offsets (Camera Shake, Offset, etc.)
		v3Desired += _v3CameraOffset;

		// Finally, lerp to the desired position.
		transform.position = Vector3.Lerp (transform.position, v3Desired, Time.deltaTime * _flCameraSnap);
		

	}
}
