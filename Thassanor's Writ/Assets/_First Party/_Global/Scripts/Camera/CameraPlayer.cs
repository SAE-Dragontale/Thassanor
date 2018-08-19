/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CameraPlayer.cs
   Version:			0.1.0
   Description: 	The player-follow camera. Has all basic functions to smoothly follow the player.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections.Generic;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine;

public class CameraPlayer : CameraBase {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private Camera _camera;

	[Space] [Header("Camera Focus")]

	[SerializeField] private Transform _primaryFocus;
	[SerializeField] public List<Transform> _everyFocus;

	public Transform _PrimaryFocus {

		set {
			_primaryFocus = value;
			ResetFocus();
		}

	}

	[Space] [Header("Camera Effects")]

	[SerializeField] private PostProcessVolume _postProcessing;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Space] [Header("Camera Offset")]

	// The Camera Offset(s) being applied to the position.
	[SerializeField] private Vector3 _cameraOffset;	// Considered Offset. Added before LookAt().

	public Vector3 _additionalOffset;	// Additional Offset for programmatical purposes. This is added to _v3Offset.
	public Vector3 _focalOffset;	// Flat offset. Added last, after LookAt() for a panning effect.

	// The speed at which the camera snaps to the target its viewing.
	[SerializeField] private float _snapSpeed;
	[SerializeField] private float _rotateSpeed;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void Awake() => _camera = GetComponent<Camera>();

	private void Start() {

		// Just culling null entities from the camera transform list.
		_everyFocus.RemoveAll(Transform => Transform == null);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void ResetFocus() {

		_everyFocus.Clear();
		_everyFocus.Add(_primaryFocus);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Runtime
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Update is called once per frame
	private void FixedUpdate() {

		SetCameraPosition();
		_postProcessing.weight = Mathf.Clamp01(Vector3.Distance(transform.position, desiredPosition)/5f);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Camera Position
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void SetCameraPosition() {

		// Reset our previous position.
		desiredPosition = new Vector3(0, 0, 0);

		// Find the middle of any currently listed focal targets that we've been given.
		FindFocalPoint();
		OffsetFocalPoint();

		// Rotate the camera towards our target and apply a rotational lerp.
		transform.rotation = Quaternion.Slerp(transform.rotation, RotateCamera(), Time.deltaTime * _rotateSpeed);
		
		// Offset the camera's position if we need to and then finally lerp the camera's physical position.
		OffsetCameraPosition();
		transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * _snapSpeed);

	}

	// This quickly tries to validate all the transforms in the list, and if there are null values, it removes them.
	private void FindFocalPoint() {

		for (int i = _everyFocus.Count-1; i >= 0; i--) {

			if (_everyFocus[i] == null) 
				_everyFocus.RemoveAt(i);
			else
				desiredPosition += _everyFocus[i].position;

		}

		if (_everyFocus.Count > 1)
			desiredPosition = desiredPosition / _everyFocus.Count;

	}

	private void OffsetFocalPoint() => desiredPosition += _focalOffset;
	private void OffsetCameraPosition() => desiredPosition += _cameraOffset + _additionalOffset;

	private Quaternion RotateCamera() => Quaternion.LookRotation(desiredPosition - transform.position, transform.up);

}
