/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			KeyboardBindings.cs
   Version:			0.0.0
   Description: 	A scriptable object used to contain the player's keyboard inputs and save them in an efficient manner.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

[CreateAssetMenu(fileName = "New Keybinding Preset", menuName = "Technical/Keybindings")]
public class KeyboardBindings : ScriptableObject {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public int _itButtonCount;
	public KeyCode[] _akcBoolButtons;

	public int _itAxesCount;
	public AxisButtons[] _aabAxisButtons;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Editor Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Awake() in Inspector
	public void Reset() {

		DefaultKeybindings();

	}

	// A quick hardcoded function to set the default keybindings for development purposes.
	public void DefaultKeybindings() {

		_itAxesCount = 2;
		_itButtonCount = 3;

		_aabAxisButtons = new AxisButtons[_itAxesCount];
		_akcBoolButtons = new KeyCode[_itButtonCount];

		try {

			_aabAxisButtons[0]._kcPositive = KeyCode.W;
			_aabAxisButtons[0]._kcNegative = KeyCode.S;

			_aabAxisButtons[1]._kcPositive = KeyCode.A;
			_aabAxisButtons[1]._kcNegative = KeyCode.D;

			_akcBoolButtons[0] = KeyCode.Escape;
			_akcBoolButtons[1] = KeyCode.Return;
			_akcBoolButtons[2] = KeyCode.Space;

			Debug.Log("Done");

		} catch {

			Debug.LogError("There aren't enough kebindings assigned in the InputTranslator to properly reset.");

		}

	}

	// Alternative to Reset() that doesn't clear away our keybindings.
	public void Refresh() {

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

	/* ----------------------------------------------------------------------------- */

}
