/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			MusicManager.cs
   Version:			0.3.0
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
	ParameterInstance mainThemePausing;
	ParameterInstance mainThemeScene;

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

		AwakeMusic();
		AwakeSFX();
		AwakeUI();
		
	}

	private void AwakeMusic() {

		mainTheme = RuntimeManager.CreateInstance("event:/Soundtrack/Main");
		mainTheme.getParameter("intensity", out mainThemeIntensity);
		mainTheme.getParameter("pause", out mainThemePausing);
		mainTheme.getParameter("health", out mainThemeHealth);
		mainTheme.getParameter("scene", out mainThemeScene);

	}

	private void AwakeSFX() {



	}

	private void AwakeUI() {

	}

	// Called before class calls or functions.
	private void Start () {

		// Initialise default music parameters.
		MusicToGameplay();
		_playerHealth = 100f;
		_gameIntensity = 1;

		// Finally, begin the music.
		BeginMusic();		

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Music Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Initialise the music.
	public void BeginMusic() => mainTheme.start();
	public void StopMusic() => mainTheme.stop(STOP_MODE.ALLOWFADEOUT);

	// Parameteres controlling the music.
	public void HealthParam(float health) => mainThemeHealth.setValue(health);
	public void PausedParam(bool paused) => mainThemeScene.setValue(paused ? 0f : 1f);
	public void IntensityParam(float intensity) => mainThemeIntensity.setValue(intensity);

	// Versatile controlling for scenes. Some hardcoded functions for readability and a dynamic function as an emergency.
	public void SceneParam(int scene) => mainThemeScene.setValue(scene);
	public void MusicToMenu() => mainThemeScene.setValue(0f);
	public void MusicToMenuSpecial() => mainThemeScene.setValue(1f);
	public void MusicToGameplay() => mainThemeScene.setValue(2f);
	public void MusicToCredits() => mainThemeScene.setValue(3f);

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
