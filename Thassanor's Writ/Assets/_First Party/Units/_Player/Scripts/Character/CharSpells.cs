/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			CharSpells.cs
   Version:			0.1.1
   Description: 	Controls all functions related to the Typing Elements within the game.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharVisuals))]
public class CharSpells : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// This is the base root we'll be working with. From here on out we can reference _tr[etc] rather than transform.
	private Transform _trTypingComponent;

	// These are the sub-components that we're gathering from the root dir above.
	private TMP_InputField _inputField;

	// The list of spells in our loadout. These should generally be assigned at runtime, however if they aren't, then the defaults are assigned in the inspector.
	[SerializeField] private Spell[] _spellLoadout;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Run before Start()
	private void Awake() {

		// Grab the script project root and it's associated references.
		_trTypingComponent = transform.Find("Visual/PlayerUI");
		_inputField = _trTypingComponent.GetComponentInChildren<TMP_InputField>();

	}

	// Run at the start of an object's lifetime.
	private void Start() {

		// #TODO: Properly assign spells here from loadout.

		// Just as a failsafe, we want to make sure we're always starting with the input field disabled.
		TypeStatus(false, true);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called when we're starting to type. Contains all activation code and executes any runtime requirements.
	public void TypeStatus(bool shouldShow, bool wasCancelled = false) {

		// Just quickly toggle each component of the typing field.
		_trTypingComponent.gameObject.SetActive(shouldShow);
		_inputField.interactable = shouldShow;

		// Then either disable the field or force focus onto the field.
		if (!wasCancelled) {
			if (!shouldShow) {

				_inputField.DeactivateInputField();
				CastSpell();

			} else {

				_inputField.text = "";
				FocusCursor();

			}
		}

	}

	/* ----------------------------------------------------------------------------- */
	// Update Functions

	// We're using the existing Events within the InputField object to force focus onto the object until it's not needed anymore.
	public void FocusCursor() {

		_inputField.ActivateInputField();
		_inputField.MoveTextEnd(false);

	}

	// The Prediction Model that we're using to match the player's currently entered text to the closest matching spell in their loadout.
	public void PredictSpell() {

		// #TODO: Predict the spell the player is typing and display it dynamically.

	}

	/* ----------------------------------------------------------------------------- */
	// Finalising Functions

	// Choosing, and then casting the currently selected spell.
	private void CastSpell() {

		// #TODO: Cast the closest matching spell.

	}

	// Here we evaluate the player's accuracy to what they intended to cast and modify casting time based on this.
	private void CastEvaluation() {

		// #TODO: Evaluate the validity of the player's input, and then grade the spell based on it.

	}

}