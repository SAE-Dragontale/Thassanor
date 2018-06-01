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

    enum PlayerState {Idle, Spellcasting, Paused};
    PlayerState _enPlayerState;
    PlayerState _enLastState;

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
        // Debug.Log("--------------------");
        // Debug.Log($"Movement: Vertical [{rdi._aflAxes[0]}], Horizontal [{rdi._aflAxes[1]}]");
        // Debug.Log($"Buttons Pressed: Escape [{rdi._ablButtons[0]}], Enter [{rdi._ablButtons[1]}].");

        // Pressing the ESC key should come first in our processes.
        if (rdi._ablButtons[0]) {

            switch (_enPlayerState) {

				// If we're idle, pause the game.
				case (PlayerState.Idle):
                    _enLastState = _enPlayerState;
                    _enPlayerState = PlayerState.Paused;
					break;

				// If we're casting a spell, stop casting.
                case (PlayerState.Spellcasting):
                    _enPlayerState = PlayerState.Idle;
					// TODO: We want to do a little more than just shunt the PlayerState back to Idle. Call function here that represents the same command later.
                    break;

				// If we're paused, unpause the game.
                case (PlayerState.Paused):
                    _enPlayerState = _enLastState;
                    break;

            }

        }

		// Now, we should be checking each game state, as different states call for different actions.
        switch (_enPlayerState) {

			// If we're Idle, we want to process in-game commands.
            case (PlayerState.Idle):
                _scControl.TrajectoryChange(rdi._aflAxes);
                break;

			// If we're Spellcasting, we only really want to process final input as the keyboard is used for typing.
            case (PlayerState.Spellcasting):
                break;

        }
		
	}

}