/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharVisuals.cs
   Version:			0.4.1
   Description: 	Called by player scripts that need to execute visual functions. Should not directly recieve player input.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

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
	[SerializeField] private NecromancerStyle _necromancerStyle;
	public NecromancerStyle _NecromancerStyle {
		set {
			_necromancerStyle = value;
			LoadStyle();
		}
	}


	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Space] [Header("Character Zoom")]
	[SerializeField] private Vector3 _v3PlayerOffset = new Vector3(0,0,0);
	[SerializeField] private Vector3 _v3PlayerPanning = new Vector3(0, 0, 0);

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void Reset() => GetReferences();
	private void Awake() => GetReferences();

	// Component Grab.
	private void GetReferences() {

		_an = GetComponentInChildren<Animator>();
		_sr = GetComponentInChildren<SpriteRenderer>();
		_cp = Camera.main.GetComponent<CameraPlayer>();

	}

	// Called before Update().
	private void Start() => LoadStyle();

	private void LoadStyle() => _an.runtimeAnimatorController = _necromancerStyle._animatorController;


	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	/* ----------------------------------------------------------------------------- */
	// Animation Controllers

	public void AnimMovement(float[] aflMovement = null) {
	
		// If no value is passed to the function we should substitute {0,0} instead.
		aflMovement = aflMovement ?? new float[2];

		
		// First we need to determine whether we're running or not and change our animator as needed.
		_an.SetBool("isRunning", (aflMovement[0] != 0 || aflMovement[1] != 0) );

		// Here we only want to change our facing direction if we've got implicit movement direction (0<) plugged in.
		if (aflMovement[1] > 0)
			_sr.flipX = false;

		else if (aflMovement[1] < 0) 
			_sr.flipX = true;

	}

	public void AnimCasting(bool isSpelling = true) {

		_an.SetBool("isSpelling", isSpelling);

	}

	/* ----------------------------------------------------------------------------- */
	// Camera Calls

	public void CharacterZoom(bool isZooming) {

		_cp._v3PlayerOffset = isZooming ? _v3PlayerOffset : new Vector3 (0,0,0);
		_cp._v3PlayerPanning = isZooming ? _v3PlayerPanning : new Vector3(0, 0, 0);

	}

}