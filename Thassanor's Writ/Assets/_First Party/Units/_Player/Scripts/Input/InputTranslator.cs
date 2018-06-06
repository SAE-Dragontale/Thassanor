/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			InputTranslator.cs
   Version:			0.5.2
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

	// Class Settings
	[SerializeField] private float _flAnimationLock;

	// PlayerState Containers.
    private enum PlayerState {Idle, Spellcasting, Paused};
    private PlayerState _enPlayerState;
    private PlayerState _enLastState;

	// Blank Movement Variable.
	private float[] _flStill;

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

					// TODO: Implement pause menu here.
					// This should be done through additive loading, and called as a function in another script from this class.

					// Save our old state, set our new state, and halt movement.
					_enLastState = _enPlayerState;
					_enPlayerState = PlayerState.Paused;
					_scControl.TrajectoryHalt();
				}

				// Pressing Enter
				else if (rdi._ablButtons[1]) {

					// TODO: Implement spellcasting trigger.
					// Communicate with CharSpells.cs and begin the 'Spellcasting Phase' from this point.

					// Set our state to spellcasting and halt movement.
					_enPlayerState = PlayerState.Spellcasting;
					_scControl.TrajectoryHalt();
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

					// TODO: Implement spellcasting 'Abort'.
					// We want to do a little more than just shunt the PlayerState back to Idle. Call function here that represents the same command later.

					// Clean up variables. (this needs to change.)
					_enPlayerState = PlayerState.Idle;
					_scControl.TrajectoryChange(rdi._aflAxes);
				}

				// Pressing Enter
				else if (rdi._ablButtons[1]) {

					// TODO: Implement spellcasting 'Cast'.
					// We don't want to instantly transition here. Include an Animation Lock.

					// Clean up variables. (This needs to change.)
					_enPlayerState = PlayerState.Idle;
					_scControl.TrajectoryChange(rdi._aflAxes);
				}

				break;

			/* ----------------------------------------------------------------------------- */
			case (PlayerState.Paused):

				// Pressing Escape
				if (rdi._ablButtons[0]) {

					// TODO: Implement ability to dismiss pause menu here.
					// Same as the sister function above, except we also want to be able to deload the pause menu. Function should be called from here.

					// Load the state that the player was in before this function was called.
					_enPlayerState = _enLastState;
				}
				
				break;

		}
		
	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// A small helper function to include an animation lock to State Switching in some circumstances.
	private IEnumerator AnimationLock(float flAnimationLock, PlayerState _enNewState) {

		yield return new WaitForSeconds(flAnimationLock);
		
		_enPlayerState = _enNewState;
		_scControl.TrajectoryChange(rdi._aflAxes);
	}

}
 
 