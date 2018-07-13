/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			KeyboardBindingsEditor.cs
   Version:			0.0.0
   Description: 	Increases readability of the KeyboardBindings Scriptable Object in the same fashion as KeyboardTrackerEditor.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(KeyboardBindings))]
public class KeyboardBindingsEditor : Editor {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private KeyboardBindings _keyboardBindings;

	private void OnEnable() {
		_keyboardBindings = target as KeyboardBindings;
	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Window Interface
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

    public override void OnInspectorGUI() {

		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Button Axes
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		// First we need to start monitoring for any changes that occur.
		EditorGUI.BeginChangeCheck();

		GUILayout.Space(10);
		GUILayout.BeginVertical("box");

		GUILayout.BeginHorizontal("box");
		EditorGUILayout.LabelField("Required Input(s)", EditorStyles.boldLabel);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("# of Axes", GUILayout.Width(90));
		_keyboardBindings._itAxesCount = EditorGUILayout.IntSlider(_keyboardBindings._itAxesCount,0,10);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("# of Buttons", GUILayout.Width(90));
		_keyboardBindings._itButtonCount = EditorGUILayout.IntSlider(_keyboardBindings._itButtonCount,0,10);
		GUILayout.EndHorizontal();

		if (_keyboardBindings._itAxesCount < 1 && _keyboardBindings._itButtonCount < 1) {
			EditorGUILayout.HelpBox("If your player character does not need any inputs then you do not need this script.", MessageType.Info);
		}

		GUILayout.EndVertical();

		// Finally, if changes have occured, we need to refresh the counterpart script's input variables.
		if (EditorGUI.EndChangeCheck()) {
			_keyboardBindings.Refresh();
		}

		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Button Axes
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		if (_keyboardBindings._itAxesCount > 0) {

			//GUILayout.Space(5);
			GUILayout.BeginVertical("box");

			GUILayout.BeginHorizontal("box");
			EditorGUILayout.LabelField("Axes Keybindings", EditorStyles.boldLabel);
			GUILayout.EndHorizontal();

			if (_keyboardBindings._aabAxisButtons.Length < 1) {
				EditorGUILayout.HelpBox("The Input Manager is not calling for any Axis to be defined.", MessageType.Info);

			} else {
				for (int it = 0; it < _keyboardBindings._aabAxisButtons.Length; it++) {

					// Just a quick bit of code to hardcode titles into certain roles. Deprecated in a later version.
					string stTitle;

					if (it == 0) {
						stTitle = "Vertical";
					} else if (it == 1) {
						stTitle = "Horizontal";
					} else {
						stTitle = "Axis " + it.ToString();
					}

					// Present the title of the Axis, along with the two keycodes that represents its poles.
					GUILayout.BeginHorizontal();
					EditorGUILayout.LabelField(stTitle, GUILayout.Width(90));

					EditorGUILayout.LabelField("+", GUILayout.Width(15));
					_keyboardBindings._aabAxisButtons[it]._kcPositive = (KeyCode)EditorGUILayout.EnumPopup(_keyboardBindings._aabAxisButtons[it]._kcPositive, GUILayout.Width(50), GUILayout.ExpandWidth(true));

					GUILayout.Space(15);
					EditorGUILayout.LabelField("-", GUILayout.Width(15));
					_keyboardBindings._aabAxisButtons[it]._kcNegative = (KeyCode)EditorGUILayout.EnumPopup(_keyboardBindings._aabAxisButtons[it]._kcNegative, GUILayout.Width(50), GUILayout.ExpandWidth(true));

					GUILayout.EndHorizontal();

				}
			}

			GUILayout.EndVertical();
		
		}

		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Button Booleans
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		if (_keyboardBindings._itButtonCount > 0) {

			//GUILayout.Space(5);
			GUILayout.BeginVertical("box");

			GUILayout.BeginHorizontal("box");
			EditorGUILayout.LabelField("Button Keybindings", EditorStyles.boldLabel);
			GUILayout.EndHorizontal();

			if (_keyboardBindings._akcBoolButtons.Length < 1) {
				EditorGUILayout.HelpBox("The Input Manager is not calling for any Axis to be defined.", MessageType.Info);
			} else {
				for (int it = 0; it < _keyboardBindings._akcBoolButtons.Length; it++) {

					GUILayout.BeginHorizontal();

					EditorGUILayout.LabelField("Button " + it.ToString(), GUILayout.Width(90));
					_keyboardBindings._akcBoolButtons[it] = (KeyCode)EditorGUILayout.EnumPopup(_keyboardBindings._akcBoolButtons[it], GUILayout.Width(50), GUILayout.ExpandWidth(true));

					GUILayout.EndHorizontal();

				}
			}

			GUILayout.EndVertical();
		
		}

		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Elseworthy Information
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		//GUILayout.Space(5);

		// A quick "Gotcha" if you fuck something up and need to reset. Does not automatically configure InputSettings and may throw an error.
		if (GUILayout.Button("Reset Keybindings")) {
			if (EditorUtility.DisplayDialog("Confirmation", "Are you sure you want to reset the keybindings?", "Yes", "Cancel")) {
				_keyboardBindings.Reset();
			}
		}

		EditorGUILayout.LabelField("Created by Hayden Reeve", EditorStyles.centeredGreyMiniLabel, GUILayout.ExpandWidth(true));
		GUILayout.Space(1);

	}

}
