/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			AxisButtonsDrawer.cs
   Version:			0.0.0
   Description: 	Used to extend the AxisButtons Struct and make it more easily viewable in the Unity Editor. 
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(AxisButtons))]
public class AxisButtonsDrawer : PropertyDrawer {

	// Overriding the default function. We're telling Unity to use ours, not its own.
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent content) {

		// Announce that we're starting to draw the property.		
		EditorGUI.BeginProperty(position, content, property);

		// Display Properties
		int itIndent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		// First we define the positions of everything.
		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), content);

		GUIContent gcPositive = new GUIContent("+");
		Rect rtLabelPositive = new Rect (position.x + 00, position.y, 15, position.height);
		Rect rtFieldPositive = new Rect (position.x + 20, position.y, 50, position.height);

		GUIContent gcNegative = new GUIContent("-");
		Rect rtLabelNegative = new Rect (position.x + 75, position.y, 15, position.height);
		Rect rtFieldNegative = new Rect (position.x + 95, position.y, 50, position.height);

		// Now we actually draw what we want to display.
		
		EditorGUI.LabelField (rtLabelPositive, gcPositive);
		EditorGUI.PropertyField (rtFieldPositive, property.FindPropertyRelative("_kcPositive"), GUIContent.none);

		EditorGUI.LabelField (rtLabelNegative, gcNegative);
		EditorGUI.PropertyField (rtFieldNegative, property.FindPropertyRelative("_kcNegative"), GUIContent.none);

		// Remove Display Properties
		EditorGUI.indentLevel = itIndent;

		// Announce that we've finished drawing the property.
		EditorGUI.EndProperty();

	}

}
