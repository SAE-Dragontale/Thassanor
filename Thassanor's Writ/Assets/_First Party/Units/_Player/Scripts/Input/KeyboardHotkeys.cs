/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			KeyboardHotkeys.cs
   Version:			0.1.0
   Description: 	A scriptable object used to contain the player's keyboard inputs and save them in an efficient manner.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

[CreateAssetMenu(fileName = "New Hotkey Preset", menuName = "Technical/Hotkeys")]
public class KeyboardHotkeys : ScriptableObject {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
			
	public KeyCode[] _arrayKeyCode;
	public KeyAxis[] _arrayKeyAxis;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		System Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Class Constructor
	public KeyboardHotkeys(bool shouldBeDefault = true) {

		if (shouldBeDefault)
			Reset();

	}

	// Called by Unity when resetting elements back to default.
	public void Reset() {

		Debug.Log("I'm resetting my keybindings.");

		_arrayKeyAxis = new KeyAxis[2];
		_arrayKeyCode = new KeyCode[3];

		try {

			_arrayKeyAxis[0].positive = KeyCode.W;
			_arrayKeyAxis[0].negative = KeyCode.S;

			_arrayKeyAxis[1].positive = KeyCode.D;
			_arrayKeyAxis[1].negative = KeyCode.A;

			_arrayKeyCode[0] = KeyCode.Escape;
			_arrayKeyCode[1] = KeyCode.Return;
			_arrayKeyCode[2] = KeyCode.Space;

		} catch {

			Debug.LogError("There aren't enough kebindings assigned in the InputTranslator to properly reset.");

		}

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Function Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Redefines the length of our current KeyAxis array to the new defined length.
	public void RefreshKeyAxis(int newLength) {

		KeyAxis[] aabAxisButtonsNew = new KeyAxis[newLength];

		if (_arrayKeyAxis != null) {
			for (int it = 0; it < Mathf.Min(aabAxisButtonsNew.Length, _arrayKeyAxis.Length); it++) {
				aabAxisButtonsNew[it] = _arrayKeyAxis[it];
			}
		}

		_arrayKeyAxis = aabAxisButtonsNew;

	}

	// Redefines the length of our current KeyCode array to the new defined length.
	public void RefreshKeyCode(int newLength) {

		// Assign temporary Arrays to store data according to new length governance.
		KeyCode[] akcBoolButtonNew = new KeyCode[newLength];		

		// Load our old data into the new array, either culling elements or expanding the maximum number of elements.
		if (_arrayKeyCode != null) {
			for (int it = 0; it < Mathf.Min(akcBoolButtonNew.Length, _arrayKeyCode.Length); it++) {
				akcBoolButtonNew[it] = _arrayKeyCode[it];
			}
		}

		// Finally, assign our original variables with the new array lengths, containing our old array data.
		_arrayKeyCode = akcBoolButtonNew;
		
	}

}

/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
	Our custom Data Structs.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

[System.Serializable]
public struct KeyAxis {

	public KeyCode positive;
	public KeyCode negative;

}

/* ----------------------------------------------------------------------------- */