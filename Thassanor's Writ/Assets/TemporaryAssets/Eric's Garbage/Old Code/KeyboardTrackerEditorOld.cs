///* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
//   Author: 			Hayden Reeve
//   File:			KeyboardTrackerEditor.cs
//   Version:			0.2.0
//   Description: 	A quick UnityEditor Script to increase readability of the KeyBoard Tracker Script. Handles Input Configuration.
//// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(KeyboardTracker))]
//public class KeyboardTrackerEditor : Editor {

//	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
//		Initialisation
//	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

//	private KeyboardTracker _keyboardTracker;

//	private void OnEnable() {
//		_keyboardTracker = target as KeyboardTracker;
//	}

//	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
//		Window Interface
//	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

//    public override void OnInspectorGUI() {

//		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
//			Button Axes
//		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

//		// First we need to start monitoring for any changes that occur.
//		EditorGUI.BeginChangeCheck();

//		GUILayout.Space(10);
//		GUILayout.BeginVertical("box");

//		GUILayout.BeginHorizontal("box");
//		EditorGUILayout.LabelField("Required Input(s)", EditorStyles.boldLabel);
//		GUILayout.EndHorizontal();

//		GUILayout.BeginHorizontal();
//		EditorGUILayout.LabelField("# of Axes", GUILayout.Width(90));
//		_keyboardTracker._itAxisCount = EditorGUILayout.IntSlider(_keyboardTracker._itAxisCount,0,10);
//		GUILayout.EndHorizontal();

//		GUILayout.BeginHorizontal();
//		EditorGUILayout.LabelField("# of Buttons", GUILayout.Width(90));
//		_keyboardTracker._itKeysCount = EditorGUILayout.IntSlider(_keyboardTracker._itKeysCount,0,10);
//		GUILayout.EndHorizontal();

//		if (_keyboardTracker._itAxisCount < 1 && _keyboardTracker._itKeysCount < 1) {
//			EditorGUILayout.HelpBox("If your player character does not need any inputs then you do not need this script.", MessageType.Info);
//		}

//		GUILayout.EndVertical();

//		// Finally, if changes have occured, we need to refresh the counterpart script's input variables.
//		if (EditorGUI.EndChangeCheck()) {
//			_keyboardTracker.Refresh();
//		}

//		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
//			Button Axes
//		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

//		if (_keyboardTracker._itAxisCount > 0) {

//			//GUILayout.Space(5);
//			GUILayout.BeginVertical("box");

//			GUILayout.BeginHorizontal("box");
//			EditorGUILayout.LabelField("Axes Keybindings", EditorStyles.boldLabel);
//			GUILayout.EndHorizontal();

//			if (_keyboardTracker._aabAxisButtons.Length < 1) {
//				EditorGUILayout.HelpBox("The Input Manager is not calling for any Axis to be defined.", MessageType.Info);

//			} else {
//				for (int it = 0; it < _keyboardTracker._aabAxisButtons.Length; it++) {

//					// Just a quick bit of code to hardcode titles into certain roles. Deprecated in a later version.
//					string stTitle;

//					if (it == 0) {
//						stTitle = "Vertical";
//					} else if (it == 1) {
//						stTitle = "Horizontal";
//					} else {
//						stTitle = "Axis " + it.ToString();
//					}

//					// Present the title of the Axis, along with the two keycodes that represents its poles.
//					GUILayout.BeginHorizontal();
//					EditorGUILayout.LabelField(stTitle, GUILayout.Width(90));

//					EditorGUILayout.LabelField("+", GUILayout.Width(15));
//					_keyboardTracker._aabAxisButtons[it]._kcPositive = (KeyCode)EditorGUILayout.EnumPopup(_keyboardTracker._aabAxisButtons[it]._kcPositive, GUILayout.Width(50), GUILayout.ExpandWidth(true));

//					GUILayout.Space(15);
//					EditorGUILayout.LabelField("-", GUILayout.Width(15));
//					_keyboardTracker._aabAxisButtons[it]._kcNegative = (KeyCode)EditorGUILayout.EnumPopup(_keyboardTracker._aabAxisButtons[it]._kcNegative, GUILayout.Width(50), GUILayout.ExpandWidth(true));

//					GUILayout.EndHorizontal();

//				}
//			}

//			GUILayout.EndVertical();
		
//		}

//		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
//			Button Booleans
//		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

//		if (_keyboardTracker._itKeysCount > 0) {

//			//GUILayout.Space(5);
//			GUILayout.BeginVertical("box");

//			GUILayout.BeginHorizontal("box");
//			EditorGUILayout.LabelField("Button Keybindings", EditorStyles.boldLabel);
//			GUILayout.EndHorizontal();

//			if (_keyboardTracker._akcBoolButtons.Length < 1) {
//				EditorGUILayout.HelpBox("The Input Manager is not calling for any Axis to be defined.", MessageType.Info);
//			} else {
//				for (int it = 0; it < _keyboardTracker._akcBoolButtons.Length; it++) {

//					GUILayout.BeginHorizontal();

//					EditorGUILayout.LabelField("Button " + it.ToString(), GUILayout.Width(90));
//					_keyboardTracker._akcBoolButtons[it] = (KeyCode)EditorGUILayout.EnumPopup(_keyboardTracker._akcBoolButtons[it], GUILayout.Width(50), GUILayout.ExpandWidth(true));

//					GUILayout.EndHorizontal();

//				}
//			}

//			GUILayout.EndVertical();
		
//		}

//		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
//			Elseworthy Information
//		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

//		//GUILayout.Space(5);

//		// A quick "Gotcha" if you fuck something up and need to reset. Does not automatically configure InputSettings and may throw an error.
//		if (GUILayout.Button("Reset Keybindings")) {
//			if (EditorUtility.DisplayDialog("Confirmation", "Are you sure you want to reset the keybindings?", "Yes", "Cancel")) {
//				_keyboardTracker.DefaultKeybindings();
//			}
//		}

//		EditorGUILayout.LabelField("Created by Hayden Reeve", EditorStyles.centeredGreyMiniLabel, GUILayout.ExpandWidth(true));
//		GUILayout.Space(1);

//	}

//}
