/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			MusicManager.cs
   Version:			0.3.1
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
	ParameterInstance mainThemeScene;

	EventInstance ambienceMenu;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	private void Awake() {

		AwakeMusic();
		AwakeAmbience();
		
	}

	private void AwakeMusic() {

		mainTheme = RuntimeManager.CreateInstance("event:/Soundtrack/Main");
		mainTheme.getParameter("intensity", out mainThemeIntensity);
		mainTheme.getParameter("health", out mainThemeHealth);
		mainTheme.getParameter("scene", out mainThemeScene);

	}

	private void AwakeAmbience() {

		ambienceMenu = RuntimeManager.CreateInstance("event:/Ambience/menuAmbience");

	}

	// Called before class calls or functions.
	private void Start () {

		// Initialise default music parameters.
		MusicToGameplay();

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
		Ambience Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Menu
	public void BeginMenuAmbience() => ambienceMenu.start();
	public void StopMenuAmbience() => ambienceMenu.stop(STOP_MODE.ALLOWFADEOUT);

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		SFX Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Player Casting
	public void SingleSpellArcher(Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/Gameplay/castArcher", locationFrom);
	public void SingleSpellWarrior(Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/Gameplay/castWarrior", locationFrom);
	public void SingleSpellResurrection(Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/Gameplay/castResurrection", locationFrom);

	// Player Receiving Damage
	public void SingleDamageArcher(Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/Gameplay/charDamageArcher", locationFrom);
	public void SingleDamageWarrior(Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/Gameplay/charDamageWarrior", locationFrom);
	public void SingleCharDeath(Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/Gameplay/charDeath", locationFrom);
	public void SingleCharTyping(Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/Gameplay/charTyping", locationFrom);
	public void SingleCharHurt(Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/Gameplay/charVoiceDamage", locationFrom);

	// Footsteps
	public void SingleFootsteps(Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/Gameplay/footsteps", locationFrom);

	// Skeleton Noises
	public void SingleSkeletonDeath(Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/Gameplay/skeletonDeath", locationFrom);
	public void SingleSkeletonDamage(Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/Gameplay/skeletonDamage", locationFrom);

	// Villager Noises
	public void SingleVillagerDeath(Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/Gameplay/villagerDeathMale", locationFrom);

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		UI Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Menu UI SFX
	public void UIButtonClick (Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/UserInterface/buttonClick", locationFrom);
	public void UIScrollOpen (Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/UserInterface/scrollOpen", locationFrom);
	public void UIScrollClose (Vector3 locationFrom) => RuntimeManager.PlayOneShot("event:/UserInterface/scrollClose", locationFrom);

	/* ----------------------------------------------------------------------------- */

}
