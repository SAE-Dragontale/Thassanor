/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			KeyboardHotkeysEditor.cs
   Version:			0.1.0
   Description: 	Increases readability of the KeyboardBindings Scriptable Object in the same fashion as KeyboardTrackerEditor.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(KeyboardHotkeys))]
public class KeyboardHotkeysEditor : Editor {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private KeyboardHotkeys _keyboardBindings;

	private int _intKAxis; // Desired KeyAxis Array length.
	private int _intKCode; // Desired KeyCode Array length.	

	private void OnEnable() {

		// Target ourselves.
		_keyboardBindings = target as KeyboardHotkeys;

		// Correctly initialise our array length variables.
		_intKAxis = _keyboardBindings._arrayKeyAxis?.Length ?? 10;
		_intKCode = _keyboardBindings._arrayKeyCode?.Length ?? 10;

		_keyboardBindings.RefreshKeyAxis(_intKAxis);
		_keyboardBindings.RefreshKeyCode(_intKCode);

		// Debug.Log($"Actual Axis length is {_keyboardBindings._arrayKeyAxis.Length} and my length is {_intKAxis}");
		// Debug.Log($"Actual Code length is {_keyboardBindings._arrayKeyCode.Length} and my length is {_intKCode}");

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Window Interface
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

    public override void OnInspectorGUI() {

		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Button Axes
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		GUILayout.BeginVertical("box");

		/* ----------------------------------------------------------------------------- */
		// Header and Array Length Definition.

		GUILayout.BeginHorizontal("box");
		EditorGUI.BeginChangeCheck();

		_intKAxis = EditorGUILayout.IntField(_intKAxis, GUILayout.Width(55));
		GUILayout.Space(30);

		// Finally, if changes have occured, we need to refresh the counterpart script's input variables.
		if (EditorGUI.EndChangeCheck()) {

			_keyboardBindings.RefreshKeyAxis(_intKAxis);
			_intKAxis = _keyboardBindings._arrayKeyAxis.Length;

		}

		EditorGUILayout.LabelField("Axes Keybindings", EditorStyles.boldLabel, GUILayout.Width(90), GUILayout.ExpandWidth(true));
		GUILayout.EndHorizontal();

		/* ----------------------------------------------------------------------------- */
		// Display any existing array elements.

		if (_keyboardBindings._arrayKeyAxis.Length > 0) {

			for (int it = 0; it < _keyboardBindings._arrayKeyAxis.Length; it++) {

				GUILayout.BeginHorizontal();
				EditorGUILayout.LabelField($"Axis #{it.ToString()}", GUILayout.Width(50));

				// The Positive KeyCode.
				GUILayout.Space(15);
				EditorGUILayout.LabelField("+", GUILayout.Width(15));
				_keyboardBindings._arrayKeyAxis[it].positive = (KeyCode)EditorGUILayout.EnumPopup(_keyboardBindings._arrayKeyAxis[it].positive, GUILayout.Width(50), GUILayout.ExpandWidth(true));
			
				// The Negative KeyCode.
				GUILayout.Space(15);
				EditorGUILayout.LabelField("-", GUILayout.Width(15));
				_keyboardBindings._arrayKeyAxis[it].negative = (KeyCode)EditorGUILayout.EnumPopup(_keyboardBindings._arrayKeyAxis[it].negative, GUILayout.Width(50), GUILayout.ExpandWidth(true));

				GUILayout.EndHorizontal();

			}

		}

		GUILayout.EndVertical();

		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Button Booleans
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		GUILayout.BeginVertical("box");

		/* ----------------------------------------------------------------------------- */
		// Header and Array Length Definition.

		GUILayout.BeginHorizontal("box");
		EditorGUI.BeginChangeCheck();

		_intKCode = EditorGUILayout.IntField(_intKCode, GUILayout.Width(55));
		GUILayout.Space(30);

		// Finally, if changes have occured, we need to refresh the counterpart script's input variables.
		if (EditorGUI.EndChangeCheck()) {

			_keyboardBindings.RefreshKeyCode(_intKCode);
			_intKCode = _keyboardBindings._arrayKeyCode.Length;

		}

		EditorGUILayout.LabelField("Button Keybindings", EditorStyles.boldLabel, GUILayout.Width(90), GUILayout.ExpandWidth(true));
		GUILayout.EndHorizontal();

		/* ----------------------------------------------------------------------------- */
		// Display any existing array elements.

		if (_keyboardBindings._arrayKeyCode.Length > 0) {

			for (int it = 0; it < _keyboardBindings._arrayKeyCode.Length; it++) {

				GUILayout.BeginHorizontal();

				// The Button's Keycode.
				EditorGUILayout.LabelField("Button #" + it.ToString(), GUILayout.Width(90));
				_keyboardBindings._arrayKeyCode[it] = (KeyCode)EditorGUILayout.EnumPopup(_keyboardBindings._arrayKeyCode[it], GUILayout.Width(50), GUILayout.ExpandWidth(true));

				GUILayout.EndHorizontal();

			}

		}

		GUILayout.EndVertical();

		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Misc Functions & Information
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		/* ----------------------------------------------------------------------------- */
		// Resets the keybindings back to their default settings.

		if (GUILayout.Button("Reset Keybindings")) {
			if (EditorUtility.DisplayDialog("Confirmation", "Are you sure you want to reset the keybindings?", "Yes", "Cancel")) {

				_keyboardBindings.Reset();
				_intKAxis = _keyboardBindings._arrayKeyAxis.Length;
				_intKCode = _keyboardBindings._arrayKeyCode.Length;

			}
		}

		/* ----------------------------------------------------------------------------- */
		// If no inputs are defined, something is obviously wrong.

		if (_keyboardBindings._arrayKeyAxis?.Length + _keyboardBindings._arrayKeyCode?.Length < 1)
			EditorGUILayout.HelpBox("In order to add new keyboard inputs, you'll need to change the length of the arrays above.", MessageType.Info);

		/* ----------------------------------------------------------------------------- */
		// Author tag and creditation.

		EditorGUILayout.LabelField("Created by Hayden Reeve", EditorStyles.centeredGreyMiniLabel, GUILayout.ExpandWidth(true));
		GUILayout.Space(1);

	}

}
