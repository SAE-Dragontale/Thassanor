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

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private bool _isActive = false;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Ran before Start()
	private void Awake() {

		// Grab the script project root and it's associated references.
		_trTypingComponent = transform.Find("Visual/PlayerUI");
		_inputField = _trTypingComponent.GetComponentInChildren<TMP_InputField>();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	// Called when we're starting to type. Contains all activation code and executes any runtime requirements.
	public void TypeStatus(bool shouldActivate) {

		// Firstly, we need to make sure we've got a safeguard in case the player clicks away from the textbox.
		_isActive = shouldActivate;

		// Just quickly toggle each component of the typing field.
		_trTypingComponent.gameObject.SetActive(shouldActivate);
		_inputField.interactable = shouldActivate;

		if (!shouldActivate) {

			// TODO: Spell Stuff.

		}

	}

}