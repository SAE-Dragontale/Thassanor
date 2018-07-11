/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			InputTranslator.cs
   Version:			0.7.1
   Description: 	Translates the input provided by Tracker.cs Scripts into actual game functions that are located on the player object.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

// We send information to:
[RequireComponent(typeof(CharSpells))]
[RequireComponent(typeof(CharControls))]

public class InputTranslator : NetworkBehaviour {

    /* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

    // The scripts that control the player's movement, spellcasting, and visual functions. and basic control functionality.
    private CharControls _scControl;
    private CharSpells _scSpell;

    /* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// PlayerState Containers.
    private enum PlayerState {Idle, Spellcasting, Paused, Disabled};
    [SyncVar] private PlayerState _enPlayerState;
    [SyncVar] private PlayerState _enLastState;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public void Awake() {

        _scControl = GetComponent<CharControls>();
        _scSpell = GetComponent<CharSpells>();

        _enPlayerState = PlayerState.Idle;
        _enLastState = _enPlayerState;

    }

	public void Start() {

		SetCursorTo(false);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Command] public void CmdSyncStruct(RawDataInput inputData, bool didIhaveAuthority) {
		Debug.Log($"I am {netId} and I have authority [{didIhaveAuthority}]");
		RpcSyncStruct(inputData);
	}

	[ClientRpc] public void RpcSyncStruct(RawDataInput inputData) {
		TranslateInput(inputData);
	}

	// A call that recieves inputs from the associated scripts. Any Inputs we're recieving are presented here and then translated into function-calls.
	public void TranslateInput(RawDataInput rdi) {

		// Check the current player state and execute arguments based on it.
		switch (_enPlayerState) {

			/* ----------------------------------------------------------------------------- */
			case (PlayerState.Idle):

				// Pressing Escape
				if (rdi._ablButtons[0]) {

					// #TODO: Implement pause menu here.
					// This should be done through additive loading, and called as a function in another script from this class.

					// Save our old state, set our new state, and halt movement.
					_enLastState = _enPlayerState;

					_enPlayerState = PlayerState.Paused;
					SetCursorTo(true);

					_scControl.TrajectoryHalt();

				}

				// Pressing Enter
				else if (rdi._ablButtons[1]) {

					// #TODO: Implement spellcasting trigger.
					// Communicate with CharSpells.cs and begin the 'Spellcasting Phase' from this point.
					_scSpell.TypeStatus(true);

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

					// #TODO: Implement spellcasting 'Abort'.
					// We want to do a little more than just shunt the PlayerState back to Idle. Call function here that represents the same command later.
					_scSpell.TypeStatus(false, true);

					// Begin the sequence to exit SpellCasting mode. This should be fast when aborting.
					StartCoroutine( AnimationLock(PlayerState.Idle, rdi, 0f) );

				}

				// Pressing Enter
				else if (rdi._ablButtons[1]) {

					// #TODO: Implement spellcasting 'Cast'.
					// We don't want to instantly transition here. Include an Animation Lock.
					_scSpell.TypeStatus(false);

					// Begin the sequence to exit SpellCasting mode. This should be animation-dependant.
					StartCoroutine( AnimationLock(PlayerState.Idle, rdi, 0.5f) );

				}

				break;

			/* ----------------------------------------------------------------------------- */
			case (PlayerState.Paused):

				// Pressing Escape
				if (rdi._ablButtons[0]) {

					// #TODO: Implement ability to dismiss pause menu here.
					// Same as the sister function above, except we also want to be able to deload the pause menu. Function should be called from here.

					// Load the state that the player was in before this function was called.
					_enPlayerState = _enLastState;
					SetCursorTo(false);

				}
				
				break;
		}
	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// When we need to assign the player's cursor to either active or inactive within the hierarchy.
	private void SetCursorTo(bool isCursor) {

		Cursor.visible = isCursor;
		Cursor.lockState = isCursor ? CursorLockMode.Confined : CursorLockMode.Locked;

	}

	// A small helper function to include an animation lock to State Switching in some circumstances.
	private IEnumerator AnimationLock(PlayerState _enNewState, RawDataInput rdi, float flWait) {

		// Make sure we can't further issue commands while we're locked into something.
		_enPlayerState = PlayerState.Disabled;

		yield return new WaitForSeconds(flWait);
		
		// Change to the newly issued state and correct movement to the last pressed Axis-Keys.
		_enPlayerState = _enNewState;
		_scControl.TrajectoryChange(rdi._aflAxes);

	}

}



 