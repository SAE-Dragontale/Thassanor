/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			InputTranslator.cs
   Version:			0.3.0
   Description: 	Translates the input provided by Tracker.cs Scripts into actual game functions that are located on the player object.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharVisuals))]
[RequireComponent(typeof(CharSpells))]
[RequireComponent(typeof(CharControls))]
public class InputTranslator : MonoBehaviour {

    /* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

    // The scripts that control the player's movement, spellcasting, and visual functions. and basic control functionality.
    private CharControls _scControl;
    private CharSpells _scSpell;
    private CharVisuals _scVisual;

    /* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */



    /* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

    public void Awake() {

        _scControl = GetComponent<CharControls>();
        _scSpell = GetComponent<CharSpells>();
        _scVisual = GetComponent<CharVisuals>();

    }

    /* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// A call that recieves inputs from the associated scripts. Any Inputs we're recieving are presented here and then translated into function-calls.
	public void TranslateInput(RawDataInput rdi) {

        Debug.LogAssertion("Testing");
        Debug.Log($"Movement: Vertical [{rdi._aflAxes[0]}], Horizontal [{rdi._aflAxes[1]}]");
		Debug.Log($"Buttons Pressed: Escape [input._ablButtons[0]], Enter [{rdi._ablButtons[1]}].");

        
		
	}

}

