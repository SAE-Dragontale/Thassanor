/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			InputTranslator.cs
   Version:			0.1.0
   Description: 	Translates the input provided by Tracker.cs Scripts into actual game functions that are located on the player object.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTranslator : MonoBehaviour {

	[Range(0,10)] public int _itAxesCount;
	[Range(0,10)] public int _itButtonCount;

	public void TranslateInput(InputData input) {

		Debug.Log(string.Format("Movement: Vertical [{0}], Horizontal [{1}]",input._aflAxes[0],input._aflAxes[1]));
		Debug.Log(string.Format("Buttons Pressed: Escape [{0}], Enter [{1}].",input._ablButtons[0],input._ablButtons[1]));
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

