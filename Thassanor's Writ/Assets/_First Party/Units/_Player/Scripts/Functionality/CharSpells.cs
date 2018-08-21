/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharSpells.cs
   Version:			0.8.0
   Description: 	Controls all functions related to the Typing Elements within the game.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[RequireComponent(typeof(CharVisuals))]
public class CharSpells : NetworkBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private Transform _typingComponent;		// This is the base root we'll be working with for the Input System.

	private TMP_InputField _inputField;			// This is the player's Input Field.
	private TextMeshProUGUI[] _textDisplays;    // The locations to display the loadout strings.
	private TextMeshProUGUI _accuracyDisplay;	// Where we present the user with their typing accuracy.
	
	private CharVisuals _charVisuals;	// The Visual Controller script for the character.
	private CharAudio _charAudio;		// The Audio Controller script for the character.

	[SerializeField] private Material _toggleOn;	// The shader preset for the closest matching string.
	[SerializeField] private Material _toggleOff;	// The shader preset for the other one.

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// The list of spells in our loadout. These should generally be assigned at runtime, however if they aren't, then the defaults are assigned in the inspector.
	[SerializeField] private Spell[] _spellLoadout;

	public Spell[] _SpellLoadout {
		set { /*_spellLoadout = value;*/ InitialiseSpells(); }
	}

	[HideInInspector] public int _difficulty;		// What is the current difficulty level set by the lobby?
	[SerializeField] private int _differenceToFade;	// How much padding do we need between one prediction and the other before we grey one of them out.

	private string[] _spellPhrases;	// The total list of all of our spell phrases, taken from spell loadout.
	private int _smallestLength;	// The length of the smallest string from the above.

	[SerializeField] private string _closestMatch;		// The current closest matching string compared to what we are typing.
	[SerializeField] private int _distanceToClosest;	// How far in Levenshtein units are we to our current target.


	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Run before Start()
	private void Awake() {

		// Grab the script project root and it's associated references.
		_typingComponent = transform.Find("PlayerCanvas");

		_inputField = _typingComponent.GetComponentInChildren<TMP_InputField>();

		_textDisplays = new TextMeshProUGUI[2];
		_textDisplays[0] = _typingComponent.Find("Prediction1").GetComponent<TextMeshProUGUI>();
		_textDisplays[1] = _typingComponent.Find("Prediction2").GetComponent<TextMeshProUGUI>();

		_accuracyDisplay = _typingComponent.Find("Accuracy").GetComponent<TextMeshProUGUI>();

		_charVisuals = GetComponent<CharVisuals>();
		_charAudio = GetComponent<CharAudio>();

	}

	// Run at the start of an object's lifetime.
	private void Start() => TypeStatus(toggleOn: false, cancelCast: true);

	private void InitialiseSpells() {

		// Take out the corresponding "casting string" from each spell in the player's loadout and store it in something easier to manage later on.
		_spellPhrases = new string[_spellLoadout.Length];

		for (int i = 0; i < _spellLoadout.Length; i++)
			_spellPhrases[i] = _spellLoadout[i]._everyDifficultyPhrase[_difficulty];

		EqualiseTyping();
		
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

	}

	private void EqualiseTyping() => _smallestLength = Mathf.Min(_spellPhrases[0].Length, _spellPhrases[1].Length);
		
	// Here we evaluate the player's accuracy to what they intended to cast and modify casting time based on this.
	private void EvaluateTyping() {

		int lowestDifference = 999;				// The current lowest Levenshtein difference between our string and the target strings.
		int[] differenceBetween = new int[2];	// The player's input compared to each target string in levenshtein distance.

		_textDisplays[0].fontMaterial = _toggleOn;
		_textDisplays[1].fontMaterial = _toggleOn;

		for (int i = 0; i < _spellPhrases.Length; i++) {

			string comparison = _spellPhrases?[i].Substring(0, _smallestLength) ?? null;

			if (comparison == null)
				return;

			string typing = _difficulty > 0 ? _inputField.text : _inputField.text.ToLower();
			differenceBetween[i] = Dragontale.StringFable.Compare(typing, comparison);

			if (differenceBetween[i] < lowestDifference) {

				_closestMatch = comparison;
				lowestDifference = differenceBetween[i];

				_distanceToClosest = lowestDifference;

			}
		
		}

		_textDisplays[0].fontMaterial = _toggleOn;
		_textDisplays[1].fontMaterial = _toggleOn;

		if (differenceBetween[0] + _differenceToFade < differenceBetween[1])
			_textDisplays[1].fontMaterial = _toggleOff;

		else if (differenceBetween[1] + _differenceToFade < differenceBetween[0])
			_textDisplays[0].fontMaterial = _toggleOff;

		_accuracyDisplay.text = $"{Mathf.CeilToInt(TypingAccuracy()).ToString()}%";

	}

	// Grade our current typing attempt.
	private float TypingAccuracy() {

		return Mathf.Clamp(Dragontale.MathFable.Remap(_distanceToClosest, _closestMatch.Length, 0, 0, 100), 0, 100);

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
		if (toggleOn) {

			_inputField.text = "";

			for (int i = 0; i < _textDisplays.Length; i++) {
				_textDisplays[i].text = _spellPhrases[i];
			}

		}

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