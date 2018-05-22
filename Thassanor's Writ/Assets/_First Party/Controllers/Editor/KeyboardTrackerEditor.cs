/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			KeyboardTrackerEditor.cs
   Version:			0.0.0
   Description: 	A quick UnityEditor Script to increase readability of the KeyBoard Tracker Script. Handles Input Configuration.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(KeyboardTracker))]
public class KeyboardTrackerEditor : Editor {

    public override void OnInspectorGUI() {

		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Initialisation
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		// "target" is the current OnInspectorGUI, so in this case we're making a reference to "this", or "gameobject."
		KeyboardTracker keyboardTracker = target as KeyboardTracker;

		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Button Axes
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		GUILayout.Space(10);
		GUILayout.BeginVertical("box");

		GUILayout.BeginHorizontal("box");
		EditorGUILayout.LabelField("Axes Keybindings", EditorStyles.boldLabel);
		GUILayout.EndHorizontal();

		if (keyboardTracker._asuButtonsAxis.Length < 1) {
			EditorGUILayout.HelpBox("The Input Manager is not calling for any Axis to be defined.", MessageType.Info);

		} else {
			for (int it = 0; it < keyboardTracker._asuButtonsAxis.Length; it++) {

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
				keyboardTracker._asuButtonsAxis[it]._kcPositive = (KeyCode)EditorGUILayout.EnumPopup(keyboardTracker._asuButtonsAxis[it]._kcPositive, GUILayout.Width(50), GUILayout.ExpandWidth(true));

				GUILayout.Space(15);
				EditorGUILayout.LabelField("-", GUILayout.Width(15));
				keyboardTracker._asuButtonsAxis[it]._kcNegative = (KeyCode)EditorGUILayout.EnumPopup(keyboardTracker._asuButtonsAxis[it]._kcNegative, GUILayout.Width(50), GUILayout.ExpandWidth(true));

				GUILayout.EndHorizontal();

			}
		}

		GUILayout.EndVertical();


		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Button Booleans
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		GUILayout.Space(5);
		GUILayout.BeginVertical("box");

		GUILayout.BeginHorizontal("box");
		EditorGUILayout.LabelField("Button Keybindings", EditorStyles.boldLabel);
		GUILayout.EndHorizontal();

		if (keyboardTracker._akcButtonsBoolean.Length < 1) {
			EditorGUILayout.HelpBox("The Input Manager is not calling for any Axis to be defined.", MessageType.Info);
		} else {
			for (int it = 0; it < keyboardTracker._akcButtonsBoolean.Length; it++) {

				GUILayout.BeginHorizontal();

				EditorGUILayout.LabelField("Button " + it.ToString(), GUILayout.Width(90));
				keyboardTracker._akcButtonsBoolean[it] = (KeyCode)EditorGUILayout.EnumPopup(keyboardTracker._akcButtonsBoolean[it], GUILayout.Width(50), GUILayout.ExpandWidth(true));

				GUILayout.EndHorizontal();

			}
		}

		GUILayout.EndVertical();

		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Elseworthy Information
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		GUILayout.Space(5);

		// A quick "Gotcha" if you fuck something up and need to reset. Does not automatically configure InputSettings and may throw an error.
		if (GUILayout.Button("Reset Keybindings")) {
			if (EditorUtility.DisplayDialog("Confirmation", "Are you sure you want to reset the keybindings?", "Yes", "Cancel")) {
				keyboardTracker.DefaultKeybindings();
			}
		}

		//GUILayout.Space(5);

		EditorGUILayout.LabelField("Created by Hayden Reeve", EditorStyles.centeredGreyMiniLabel, GUILayout.ExpandWidth(true));

		GUILayout.Space(5);

	}

}
