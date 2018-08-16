/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			MusicManager.cs
   Version:			0.1.1
   Description: 	For managing all audio components within the game. All audio should be called from this script.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioManager : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	EventInstance mainTheme;
	ParameterInstance mainThemeHealth;
	ParameterInstance mainThemeIntensity;
	ParameterInstance mainThemeSpecialEvent;
	ParameterInstance mainThemeMenu;
	ParameterInstance mainThemeCredits;
	ParameterInstance mainThemeGameplay;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[SerializeField] [Range(0, 4)] int gameIntensity;
	[SerializeField] [Range(0, 100)] float playerHealth;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	private void Awake() {

		mainTheme = FMODUnity.RuntimeManager.CreateInstance("event:/Soundtrack/SOUNDTRACK");
		mainTheme.getParameterByIndex(0, out mainThemeHealth);
		mainTheme.getParameterByIndex(1, out mainThemeIntensity);
		mainTheme.getParameterByIndex(2, out mainThemeSpecialEvent);
		mainTheme.getParameterByIndex(3, out mainThemeMenu);
		mainTheme.getParameterByIndex(4, out mainThemeCredits);
		mainTheme.getParameterByIndex(5, out mainThemeGameplay);


	}

	// Called before class calls or functions.
	private void Start () {

		mainTheme.start();
		mainThemeGameplay.setValue(1);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	// Update is called once per frame.
	private void Update () {
		
	}

	/* ----------------------------------------------------------------------------- */
	
}
