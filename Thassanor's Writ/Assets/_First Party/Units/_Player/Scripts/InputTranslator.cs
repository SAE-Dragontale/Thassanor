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

	// The Player Inputs we need to track.
	[Tooltip("The number of Player-Axes we need to monitor. Each Axis is two buttons, or a joystick between two values.")]
	[Range(0,10)] public int _itAxesCount;

	[Tooltip("The number of Player-Buttons we need to monitor. These are checked if they are pressed.")]
	[Range(0,10)] public int _itButtonCount;

	// If the PlayerInput Paramaters change, we need to update the KeyboardTracker.cs script to reflect those changes.
	public void RefreshKeybindingParams() {

		KeyboardTracker scKeyboardTracker = GetComponent<KeyboardTracker>();
		if (scKeyboardTracker != null) {
			scKeyboardTracker.Refresh();
		}

	}

	// A call that recieves inputs from the associated scripts. Any Inputs we're recieving are presented here and then translated into function-calls.
	public void TranslateInput(InputData input) {

        Debug.LogAssertion("Testing");
		Debug.Log(string.Format("Movement: Vertical [{0}], Horizontal [{1}]",input._aflAxes[0],input._aflAxes[1]));
		Debug.Log(string.Format("Buttons Pressed: Escape [{0}], Enter [{1}].",input._ablButtons[0],input._ablButtons[1]));
		
	}



	// Use this for initialization
	private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
	}
}

