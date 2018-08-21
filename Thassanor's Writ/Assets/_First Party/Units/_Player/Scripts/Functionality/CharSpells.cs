/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharSpells.cs
   Version:			0.6.0
   Description: 	Controls all functions related to the Typing Elements within the game.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[RequireComponent(typeof(CharVisuals))]
public class CharSpells : NetworkBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private Transform _typingComponent;		// This is the base root we'll be working with for the Input System.

	private TMP_InputField _inputField;     // This is the player's Input Field.
	private TMP_Text[] _comparedText;		// The locations to display the loadout strings.
	

	private CharVisuals _charVisuals;       // The Visual Controller script for the character.
	private CharAudio _charAudio;			// The Audio Controller script for the character.

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// The list of spells in our loadout. These should generally be assigned at runtime, however if they aren't, then the defaults are assigned in the inspector.
	[SerializeField] private Spell[] _spellLoadout;
	[SerializeField] private string[] _spellPhrases;

	private List<string> _comparedString;   // The strings beloning to the spell phrases, sorted into closest match.

	public Spell[] _SpellLoadout {
		set {
			//_spellLoadout = value;
			InitialiseSpells();
		}
	}

	// How difficult the strings are deemed to be.
	[HideInInspector] public int _difficulty;

	// Casting Prediction and Current Modifier.
	[SerializeField] private string _closestMatch;
	[SerializeField] private int _stringDifference;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Run before Start()
	private void Awake() {

		// Grab the script project root and it's associated references.
		_typingComponent = transform.Find("PlayerCanvas");

		_inputField = _typingComponent.GetComponentInChildren<TMP_InputField>();
		_comparedText = _typingComponent.GetComponentsInChildren<TMP_Text>();

		_charVisuals = GetComponent<CharVisuals>();
		_charAudio = GetComponent<CharAudio>();

	}

	// Run at the start of an object's lifetime.
	private void Start() => TypeStatus(toggleOn: false, cancelCast: true);

	private void InitialiseSpells() {

		// Aquire Spell Settings from Phil's Implementation.
		int difficulty = 1;

		// Take out the corresponding "casting string" from each spell in the player's loadout and store it in something easier to manage later on.
		_spellPhrases = new string[_spellLoadout.Length];

		for (int i = 0; i < _spellLoadout.Length; i++)
			_spellPhrases[i] = _spellLoadout[i]._everyDifficultyPhrase[difficulty];

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called when we're starting to type. Contains all activation code and executes any runtime requirements.
	public void TypeStatus(bool toggleOn, bool cancelCast = false) {

		_charVisuals.PostProcessSpelling(toggleOn);

		ShowInputField(toggleOn);
		CallCameraZoom(toggleOn);
		CallAnimationCasting(toggleOn);

		if (toggleOn)
			return;

		if (cancelCast)
			return;

		CastSpell();

	}



	/* ----------------------------------------------------------------------------- */
	// Spellcasting Predictions

	// The Prediction Model that we're using to match the player's currently entered text to the closest matching spell in their loadout.
	// Identify which spell we're trying to cast by comparing our current string to the string for each equipped spell.

	public void PredictSpell() {

		if (!hasAuthority)
			return;
				
		EvaluateTyping();
		VisualiseComparisons();

	}

	
	// Here we evaluate the player's accuracy to what they intended to cast and modify casting time based on this.
	private void EvaluateTyping() {

		_stringDifference = 999;

		for (int i = 0; i < _spellPhrases.Length; i++) {

			string comparison = _spellPhrases?[i] ?? null;

			if (comparison == null)
				return;

			int oneDifference = Dragontale.StringFable.Compare(_inputField.text, comparison);

			if (oneDifference < _stringDifference) {

				_stringDifference = oneDifference;
				_closestMatch = comparison;

				_comparedString.Insert(0, comparison);				

			} else {

				_comparedString.Add(comparison);

			}

		}
		
	}

	/* ----------------------------------------------------------------------------- */
	// Spellcasting Actions

	// Choosing, and then casting the currently selected spell.
	private void CastSpell() {

		// We don't evaluate anything here, as we've been evaluating through each keypress of the player's input.
		// Therefore, we already have all the information we need about the correct spell. We simply need to assign it.

		// Assign spell targets.


		// Call any functions related to the spell's specific function.
		_charAudio.AudioSummonWarrior();

		// Finalise casting interface and move back to normal controls.
		TypeStatus(false, true);

	}

	/* ----------------------------------------------------------------------------- */
	// Input Field Calls

	// We're using this to toggle the User Interface that allows the player to begin typing. Calling ShowInputField is also implicetly calling FocusInputField.
	public void ShowInputField(bool toggleOn = true) {

		// We're doing this when true to wipe any old text that may still be stored from the last spellcast.
		if (toggleOn)
			_inputField.text = "";

		if (!hasAuthority)
			return;

		_typingComponent.gameObject.SetActive(toggleOn);
		_inputField.interactable = toggleOn;

		FocusInputField(toggleOn);

	}

	// We're using the existing Events within the InputField object to force focus onto the object until it's not needed anymore.
	public void FocusInputField(bool toggleOn = true) {

		if (!hasAuthority)
			return;

		if (toggleOn) {
			_inputField.ActivateInputField();
			_inputField.MoveTextEnd(false);

		} else {
			_inputField.DeactivateInputField();

		}

	}

	/* ----------------------------------------------------------------------------- */
	// String Comparison Calls

	private void VisualiseComparisons() {



	}

	/* ----------------------------------------------------------------------------- */
	// Visual Calls.

	private void CallCameraZoom(bool toggleOn) {

		if (!hasAuthority)
			return;

		_charVisuals.CharacterZoom(toggleOn);

	}

	private void CallAnimationCasting(bool toggleOn) {

		_charVisuals.AnimCasting(toggleOn);

	}

}