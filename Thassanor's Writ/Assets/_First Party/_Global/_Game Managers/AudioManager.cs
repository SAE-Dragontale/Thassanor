/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			MusicManager.cs
   Version:			0.2.0
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

	[SerializeField] [Range(0, 4)] int _gameIntensity;
	[SerializeField] [Range(0, 100)] float _playerHealth;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	private void Awake() {

		mainTheme = RuntimeManager.CreateInstance("event:/Soundtrack/SOUNDTRACK");
		mainTheme.getParameterByIndex(0, out mainThemeHealth);
		mainTheme.getParameterByIndex(1, out mainThemeIntensity);
		mainTheme.getParameterByIndex(2, out mainThemeSpecialEvent);
		mainTheme.getParameterByIndex(3, out mainThemeMenu);
		mainTheme.getParameterByIndex(4, out mainThemeCredits);
		mainTheme.getParameterByIndex(5, out mainThemeGameplay);



	}

	// Called before class calls or functions.
	private void Start () {

		// Initialise the music automatically.
		BeginMusic();

		ResetMusicTo();
		MusicToGameplay();

		HealthParam(100f);
		IntensityParam(1f);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Music Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public void BeginMusic() => mainTheme.start();
	public void StopMusic() => mainTheme.stop(STOP_MODE.ALLOWFADEOUT);

	public void HealthParam(float health) => mainThemeHealth.setValue(health);
	public void IntensityParam(float intensity) => mainThemeIntensity.setValue(intensity);

	public void MusicToMenu() => mainThemeMenu.setValue(1f);
	public void MusicToGameplay() => mainThemeGameplay.setValue(1f);
	public void MusicToCredits() => mainThemeCredits.setValue(1f);
	public void MusicToSpecial() => mainThemeSpecialEvent.setValue(1f);

	public void ResetMusicTo() {
				
		mainThemeMenu.setValue(0f);
		mainThemeGameplay.setValue(0f);
		mainThemeCredits.setValue(0f);
		mainThemeSpecialEvent.setValue(0f);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		SFX Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */



	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Update is called once per frame.
	private void Update () {

		HealthParam(_playerHealth);
		IntensityParam(_gameIntensity);

	}

	/* ----------------------------------------------------------------------------- */
	
}
