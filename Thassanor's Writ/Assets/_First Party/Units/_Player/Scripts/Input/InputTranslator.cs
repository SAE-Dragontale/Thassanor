/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			InputTranslator.cs
   Version:			0.3.0
   Description: 	Translates the input provided by Tracker.cs Scripts into actual game functions that are located on the player object.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We send information to:
[RequireComponent(typeof(CharSpells))]
[RequireComponent(typeof(CharControls))]

public class InputTranslator : MonoBehaviour {

    /* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

    // The scripts that control the player's movement, spellcasting, and visual functions. and basic control functionality.
    private CharControls _scControl;
    private CharSpells _scSpell;

    /* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */



    /* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

    public void Awake() {

        _scControl = GetComponent<CharControls>();
        _scSpell = GetComponent<CharSpells>();

    }

    /* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// A call that recieves inputs from the associated scripts. Any Inputs we're recieving are presented here and then translated into function-calls.
	public void TranslateInput(RawDataInput rdi) {

        Debug.Log("--------------------");
        Debug.Log($"Movement: Vertical [{rdi._aflAxes[0]}], Horizontal [{rdi._aflAxes[1]}]");
		Debug.Log($"Buttons Pressed: Escape [{rdi._ablButtons[0]}], Enter [{rdi._ablButtons[1]}].");
        
        _scControl.TrajectoryChange(rdi._aflAxes);
		
	}

}