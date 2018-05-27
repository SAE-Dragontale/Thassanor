/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			InputTranslatorEditor.cs
   Version:			0.2.0
   Description: 	Automatically updates the Keyboard Tracker Script with changed variables on Editor.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InputTranslator))]
public class InputTranslatorEditor : Editor {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Initialisation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private InputTranslator inputTranslator;

	private void OnEnable() {
		inputTranslator = target as InputTranslator;
	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Window Interface
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public override void OnInspectorGUI() {

		GUILayout.Space(10);

		EditorGUILayout.BeginVertical("box");
		EditorGUILayout.HelpBox("The original script looked like...", MessageType.Info);
		base.OnInspectorGUI();
		EditorGUILayout.EndVertical();

		EditorGUILayout.LabelField("Created by Hayden Reeve", EditorStyles.centeredGreyMiniLabel, GUILayout.ExpandWidth(true));
		GUILayout.Space(1);

	}

}
