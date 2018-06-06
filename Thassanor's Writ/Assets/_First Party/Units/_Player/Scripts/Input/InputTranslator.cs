/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			InputTranslator.cs
   Version:			0.4.0
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

    private enum PlayerState {Idle, Spellcasting, Paused};
    private PlayerState _enPlayerState;
    private PlayerState _enLastState;

    /* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

    public void Awake() {

        _scControl = GetComponent<CharControls>();
        _scSpell = GetComponent<CharSpells>();

        _enPlayerState = PlayerState.Idle;
        _enLastState = _enPlayerState;

    }

    /* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

    // A call that recieves inputs from the associated scripts. Any Inputs we're recieving are presented here and then translated into function-calls.
    public void TranslateInput(RawDataInput rdi) {

        // Some simple debugging that was originally useful.
        Debug.Log("--------------------");
        Debug.Log($"Movement: Vertical [{rdi._aflAxes[0]}], Horizontal [{rdi._aflAxes[1]}]");
        Debug.Log($"Buttons Pressed: Escape [{rdi._ablButtons[0]}], Enter [{rdi._ablButtons[1]}].");

		// Check the current player state and execute arguments based on it.
		switch (_enPlayerState) {

			/* ----------------------------------------------------------------------------- */
			case (PlayerState.Idle):

				// Pressing Escape
				if (rdi._ablButtons[0]) {
					_enLastState = _enPlayerState;
					_enPlayerState = PlayerState.Paused;
				}

				// Pressing Enter
				else if (rdi._ablButtons[1]) {

				}

				// If no commands are being pressed, process movement commands
				else {
					_scControl.TrajectoryChange(rdi._aflAxes);
				}
					
				break;

			/* ----------------------------------------------------------------------------- */
			case (PlayerState.Spellcasting):

				// Pressing Escape
				if (rdi._ablButtons[0]) {
					_enPlayerState = PlayerState.Idle;
					// TODO: We want to do a little more than just shunt the PlayerState back to Idle. Call function here that represents the same command later.
				}

				// Pressing Enter
				else if (rdi._ablButtons[1]) {

				}

				// If no commands are being pressed, process movement commands
				else {
					
				}

				break;

			/* ----------------------------------------------------------------------------- */
			case (PlayerState.Paused):

				// Pressing Escape
				if (rdi._ablButtons[0]) {
					_enPlayerState = _enLastState;
				}

				// Pressing Enter
				else if (rdi._ablButtons[1]) {

				}

				// If no commands are being pressed, process movement commands
				else {

				}
				
				break;

		}
		
	}

}
 
 