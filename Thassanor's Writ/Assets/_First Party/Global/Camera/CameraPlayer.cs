/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CameraPlayer.cs
   Version:			0.0.2
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
	[SerializeField] private Vector3 _v3CameraOffset;	// Considered Offset. Added before LookAt().

	public Vector3 _v3PlayerOffset;		// Additional Offset for programmatical purposes. This is added to _v3Offset.
	public Vector3 _v3PlayerPanning;	// Flat offset. Added last, after LookAt() for a panning effect.

	// The speed at which the camera snaps to the target its viewing.
	[SerializeField] private float _flCameraSnap;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void Start() {

		// Just culling null entities from the camera transform list.
		_ltrCameraFocus.RemoveAll(Transform => Transform == null);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Update is called once per frame
	private void FixedUpdate () {

		_v3Desired = new Vector3(0, 0, 0);

		ValidatedTransforms();
		OffsetDesired();

		transform.position = Vector3.Lerp(transform.position, _v3Desired, Time.deltaTime * _flCameraSnap);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Function Helpers
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// This quickly tries to validate all the transforms in the list, and if there are null values, it removes them.
	private void ValidatedTransforms() {

		for (int it = _ltrCameraFocus.Count -1; it > -1; it--) {

			if (_ltrCameraFocus[it] == null) {
				_ltrCameraFocus.RemoveAt(it);

			} else {
				_v3Desired += _ltrCameraFocus[it].position;

			}

		}

		if (_ltrCameraFocus.Count != 0)
			_v3Desired = _v3Desired / _ltrCameraFocus.Count;

	}

	// Apply all our required Offset Values to the Camera Position here.
	private void OffsetDesired() {

		// Player panning offset.
		_v3Desired += _v3PlayerPanning;

		// Look towards the player object.
		transform.LookAt(_v3Desired);

		// Apply any further offset values.
		_v3Desired += _v3CameraOffset + _v3PlayerOffset;

	}

}
