/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			InputTranslatorEditor.cs
   Version:			0.0.0
   Description: 	Automatically updates the Keyboard Tracker Script with changed variables on Editor.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InputTranslator))]
public class InputTranslatorEditor : Editor {

	public override void OnInspectorGUI() {

		// "target" is the current OnInspectorGUI, so in this case we're making a reference to "this", or "gameobject."
		InputTranslator inputTranslator = target as InputTranslator;

		EditorGUI.BeginChangeCheck();

		base.OnInspectorGUI();

		if (EditorGUI.EndChangeCheck()) {
			inputTranslator.RefreshKeybindingParams();
		}


	}

}
