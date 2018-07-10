/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CameraPlayer.cs
   Version:			0.0.1
   Description: 	The player-follow camera. Has all basic functions to smoothly follow the player.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : CameraBase {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[SerializeField] public List<Transform> _ltrCameraFocus;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// The Camera Offset(s) being applied to the position.
	[SerializeField] private Vector3 _v3CameraLocation; // Considered Offset. Added before LookAt().
	[HideInInspector] public Vector3 _v3Offset; // Additional Offset for programmatical purposes. This is added to _v3Offset.

	[HideInInspector] public Vector3 _v3Panning; // Flat offset. Added last, after LookAt() for a panning effect.

	// The speed at which the camera snaps to the target its viewing.
	[SerializeField] private float _flCameraSnap;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void Start() {

		_ltrCameraFocus.RemoveAll(vector => vector == null);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Foobar

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Update is called once per frame
	private void FixedUpdate () {

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

		// As long as there are elements within the array.
		if (itNullCount != _ltrCameraFocus.Count)
			v3Desired = v3Desired / (_ltrCameraFocus.Count - itNullCount);

		// First, we need to lerp the panning of the camera, to allow us to offset it from the target transform.
		v3Desired += _v3Panning;
		transform.LookAt(v3Desired);

		// Now, after we've panned and aimed our camera at the transform, we can offset and lerp to our camera rotation.
		v3Desired += _v3CameraLocation + _v3Offset;
		transform.position = Vector3.Lerp(transform.position, v3Desired, Time.deltaTime * _flCameraSnap);

	}

}
