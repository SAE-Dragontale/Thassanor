/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			KeyboardTracker.cs
   Version:			0.3.2
   Description: 	Inheriting from DeviceTracker.cs, this script extends functionality to track the player's Keyboard specifically.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

public class KeyboardTracker : DeviceTracker {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Button Storage
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Tooltip("The buttons our Player currently has bound for toggle-able commands.")]
	public KeyCode[] _akcBoolButtons;

	[Tooltip("The buttons our Player currently has bound for axis-based movement.")]
	public AxisButtons[] _aabAxisButtons;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		System Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Awake() in Inspector
	private void Reset() {

		_inputTranslator = GetComponent<InputTranslator>();

        _akcBoolButtons = new KeyCode[_itButtonCount];
        _aabAxisButtons = new AxisButtons[_itAxesCount];

        // Development Functionality: Assign 'baseline' keycodes for development purposes.
        DefaultKeybindings();

    }

    // Alternative to Reset() that doesn't clear away our keybindings.
    public void Refresh() {

        _inputTranslator = GetComponent<InputTranslator>();

        // Assign temporary Arrays to store data according to new length governance.
        KeyCode[] akcBoolButtonNew = new KeyCode[_itButtonCount];
        AxisButtons[] aabAxisButtonsNew = new AxisButtons[_itAxesCount];

        // Load our old data into the new array, either culling elements or expanding the maximum number of elements.
        if (_akcBoolButtons != null) {
            for (int it = 0; it < Mathf.Min(akcBoolButtonNew.Length, _akcBoolButtons.Length); it++) {
                akcBoolButtonNew[it] = _akcBoolButtons[it];
            }
        }

        if (_aabAxisButtons != null) {
            for (int it = 0; it < Mathf.Min(aabAxisButtonsNew.Length, _aabAxisButtons.Length); it++) {
                aabAxisButtonsNew[it] = _aabAxisButtons[it];
            }
        }

        // Finally, assign our original variables with the new array lengths, containing our old array data.
        _akcBoolButtons = akcBoolButtonNew;
        _aabAxisButtons = aabAxisButtonsNew;

    }

	// A quick hardcoded function to set the default keybindings for development purposes.
	public void DefaultKeybindings() {

		try {
			_aabAxisButtons[0]._kcPositive = KeyCode.W;
			_aabAxisButtons[0]._kcNegative = KeyCode.S;

			_aabAxisButtons[1]._kcPositive = KeyCode.A;
			_aabAxisButtons[1]._kcNegative = KeyCode.D;

			_akcBoolButtons[0] = KeyCode.Escape;
			_akcBoolButtons[1] = KeyCode.Return;
		} catch {
			Debug.LogError("There aren't enough kebindings assigned in the InputTranslator to properly reset.");
		}
		
	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Main Program
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	private void Awake() {
		_inputTranslator = GetComponent<InputTranslator>();
		_inputData = new RawDataInput (_itAxesCount, _itAxesCount);
	}
	
	// Called once per frame.
	private void Update () {

		// Check each Boolean-Button to see if they have been pressed or not.
		for (int it = 0; it < _akcBoolButtons.Length; it++) {
			
			if (_inputData._ablButtons[it] != Input.GetKey(_akcBoolButtons[it])) {
				_inputData._ablButtons[it] = Input.GetKey(_akcBoolButtons[it]);
			 	_hasNewData = true;
			}

		}

		// Check each Axis-Button combination to see if either key in the axis has been pressed and return a float.
		for (int it = 0; it < _aabAxisButtons.Length; it++) {
			
			float flAxisReturn = 0f;

			if (Input.GetKey (_aabAxisButtons[it]._kcPositive)) {
				flAxisReturn += 1f;
			}

			if (Input.GetKey (_aabAxisButtons[it]._kcNegative)) {
				flAxisReturn -= 1f;
			}

			if (flAxisReturn != _inputData._aflAxes[it]) {
				_inputData._aflAxes[it] = flAxisReturn;
				_hasNewData = true;
			}

		}

		// If we have new Data, we need to act upon it, so we send it to the Input Translator to be processed.
		CheckForNewData();

	}

}

/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
	Our custom Data Structs.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

[System.Serializable]
public struct AxisButtons {
	public KeyCode _kcPositive;
	public KeyCode _kcNegative;	
}