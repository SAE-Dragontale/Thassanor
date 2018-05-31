/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			KeyboardTrackerEditor.cs
   Version:			0.1.3
   Description: 	A quick UnityEditor Script to increase readability of the KeyBoard Tracker Script. Handles Input Configuration.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(KeyboardTracker))]
public class KeyboardTrackerEditor : Editor {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private KeyboardTracker keyboardTracker;

	private void OnEnable() {
		keyboardTracker = target as KeyboardTracker;
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
		keyboardTracker._itAxesCount = EditorGUILayout.IntSlider(keyboardTracker._itAxesCount,0,10);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("# of Buttons", GUILayout.Width(90));
		keyboardTracker._itButtonCount = EditorGUILayout.IntSlider(keyboardTracker._itButtonCount,0,10);
		GUILayout.EndHorizontal();

		if (keyboardTracker._itAxesCount < 1 && keyboardTracker._itButtonCount < 1) {
			EditorGUILayout.HelpBox("If your player character does not need any inputs then you do not need this script.", MessageType.Info);
		}

		GUILayout.EndVertical();

		// Finally, if changes have occured, we need to refresh the counterpart script's input variables.
		if (EditorGUI.EndChangeCheck()) {
			keyboardTracker.Refresh();
		}

		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Button Axes
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		if (keyboardTracker._itAxesCount > 0) {

			//GUILayout.Space(5);
			GUILayout.BeginVertical("box");

			GUILayout.BeginHorizontal("box");
			EditorGUILayout.LabelField("Axes Keybindings", EditorStyles.boldLabel);
			GUILayout.EndHorizontal();

			if (keyboardTracker._aabAxisButtons.Length < 1) {
				EditorGUILayout.HelpBox("The Input Manager is not calling for any Axis to be defined.", MessageType.Info);

			} else {
				for (int it = 0; it < keyboardTracker._aabAxisButtons.Length; it++) {

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
					keyboardTracker._aabAxisButtons[it]._kcPositive = (KeyCode)EditorGUILayout.EnumPopup(keyboardTracker._aabAxisButtons[it]._kcPositive, GUILayout.Width(50), GUILayout.ExpandWidth(true));

					GUILayout.Space(15);
					EditorGUILayout.LabelField("-", GUILayout.Width(15));
					keyboardTracker._aabAxisButtons[it]._kcNegative = (KeyCode)EditorGUILayout.EnumPopup(keyboardTracker._aabAxisButtons[it]._kcNegative, GUILayout.Width(50), GUILayout.ExpandWidth(true));

					GUILayout.EndHorizontal();

				}
			}

			GUILayout.EndVertical();
		
		}

		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Button Booleans
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		if (keyboardTracker._itButtonCount > 0) {

			//GUILayout.Space(5);
			GUILayout.BeginVertical("box");

			GUILayout.BeginHorizontal("box");
			EditorGUILayout.LabelField("Button Keybindings", EditorStyles.boldLabel);
			GUILayout.EndHorizontal();

			if (keyboardTracker._akcBoolButtons.Length < 1) {
				EditorGUILayout.HelpBox("The Input Manager is not calling for any Axis to be defined.", MessageType.Info);
			} else {
				for (int it = 0; it < keyboardTracker._akcBoolButtons.Length; it++) {

					GUILayout.BeginHorizontal();

					EditorGUILayout.LabelField("Button " + it.ToString(), GUILayout.Width(90));
					keyboardTracker._akcBoolButtons[it] = (KeyCode)EditorGUILayout.EnumPopup(keyboardTracker._akcBoolButtons[it], GUILayout.Width(50), GUILayout.ExpandWidth(true));

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
				keyboardTracker.DefaultKeybindings();
			}
		}

		EditorGUILayout.LabelField("Created by Hayden Reeve", EditorStyles.centeredGreyMiniLabel, GUILayout.ExpandWidth(true));
		GUILayout.Space(1);

	}

}
