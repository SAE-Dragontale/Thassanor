/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharVisuals.cs
   Version:			0.3.0
   Description: 	Called by player scripts that need to execute visual functions. Should not directly recieve player input.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharVisuals : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Hierarchy Components
	private Animator _an;
	private SpriteRenderer _sr;
	private CameraPlayer _cp;

	[Space] [Header("References")]
	[SerializeField] public NecromancerStyle _necromancerStyle;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Space] [Header("Character Zoom")]
	[SerializeField] private Vector3 _v3PlayerZoom = new Vector3(0,0,0);
	[SerializeField] private Vector3 _v3PlayerZoomPanning = new Vector3(0, 0, 0);

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Reset is called when the user hits the Reset button in the Inspector's context menu or when adding the component the first time.
	private void Reset() {

		// All we really want to do is run the actual Instantiation property.
		Awake();

	}

	// Called before Start().
	private void Awake() {

		// Component Grab.
		_an = GetComponentInChildren<Animator>();
		_sr = GetComponentInChildren<SpriteRenderer>();
		_cp = Camera.main.GetComponent<CameraPlayer>();

		// Assign Style to Character.
		_an.runtimeAnimatorController = _necromancerStyle._animatorController;

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	/* ----------------------------------------------------------------------------- */
	// Animation Controllers

	public void AnimMovement(float[] aflMovement = null) {
	
		// If no value is passed to the function we should substitute {0,0} instead.
		aflMovement = aflMovement ?? new float[2];

		// First we need to determine whether we're running or not.
		_an.SetBool("isRunning", (aflMovement[0] != 0 || aflMovement[1] != 0) );

		// Then we need to determine whether we've changed directions or not.
		if (aflMovement[1] > 0) {
			_sr.flipX = false;
		} else if (aflMovement[1] < 0) {
			_sr.flipX = true;
		}

	}

	/* ----------------------------------------------------------------------------- */
	// Camera Calls

	public void CharacterZoom(bool isZooming) {

		_cp._v3Offset = isZooming ? _v3PlayerZoom : new Vector3 (0,0,0);
		_cp._v3Panning = isZooming ? _v3PlayerZoomPanning : new Vector3(0, 0, 0);

	}

}