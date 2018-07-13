/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			KeyboardTracker.cs
   Version:			0.5.0
   Description: 	Inheriting from DeviceTracker.cs, this script extends functionality to track the player's Keyboard specifically.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

public class KeyboardTracker : DeviceTracker {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[SerializeField] private KeyboardHotkeys _keybindings;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Update().
	private void Start() {

		LoadKeybindings(_keybindings);
		_inputData = new RawDataInput( _keybindings._arrayKeyAxis.Length, _keybindings._arrayKeyCode.Length );

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Function Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// How we load keybindings from the player's preferences.
	public void LoadKeybindings(KeyboardHotkeys newHotkeys) {

		_keybindings = newHotkeys ?? new KeyboardHotkeys();

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Main Program
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	// Called once per frame.
	private void Update() {

		/* ----------------------------------------------------------------------------- */
		// Check each Axis-Button combination to see if either key in the axis has been pressed and return a float.

		for (int it = 0; it < _keybindings._arrayKeyAxis.Length; it++) {

			float flAxisReturn = 0f;

			if (Input.GetKey(_keybindings._arrayKeyAxis[it].positive)) {
				flAxisReturn += 1f;
				Debug.Log($"Positive detected at {it}.");
			}

			if (Input.GetKey(_keybindings._arrayKeyAxis[it].negative)) {
				flAxisReturn -= 1f;
				Debug.Log($"Negative detected at {it}.");
			}

			if (flAxisReturn != _inputData._aflAxis[it]) {
				_inputData._aflAxis[it] = flAxisReturn;
				_hasNewData = true;
			}

		}

		/* ----------------------------------------------------------------------------- */
		// Check each Boolean-Button to see if they have been pressed or not and return a bool.

		for (int it = 0; it < _keybindings._arrayKeyCode.Length; it++) {

			if (_inputData._ablKeys[it] != Input.GetKey(_keybindings._arrayKeyCode[it])) {
				_inputData._ablKeys[it] = Input.GetKey(_keybindings._arrayKeyCode[it]);
				_hasNewData = true;
			}

		}

		/* ----------------------------------------------------------------------------- */
		// If we have new Data, we need to act upon it, so we send it to the Input Translator to be processed.

		if (_hasNewData)
			SendNewInput();

	}

}