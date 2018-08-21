/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			InputTranslator.cs
   Version:			0.8.5
   Description: 	Translates the input provided by Tracker.cs Scripts into actual game functions that are located on the player object.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using UnityEngine.Networking;

// We send information to:
[RequireComponent(typeof(CharSpells),typeof(CharControls),typeof(CharAudio))]
public class InputTranslator : NetworkBehaviour {

    /* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

    // The scripts that control the player's movement, spellcasting, and visual functions. and basic control functionality.
    private CharControls _charControls;
    private CharSpells _charSpells;
	private CharAudio _charAudio;
	private CharVisuals _charVisuals;

    /* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// PlayerState Containers.
    private enum PlayerState {Idle, Spellcasting, Paused, Disabled};
    [SyncVar] private PlayerState _playerState;
    //[SyncVar] private PlayerState _lastState;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Declared before Start().
	private void Awake() {

		if (_charControls == null)
			GetReferences();

        _playerState = PlayerState.Idle;
        //_lastState = _playerState;

    }

	// Initialise our player components.
	private void GetReferences() {

		_charControls = GetComponent<CharControls>();
		_charSpells = GetComponent<CharSpells>();
		_charAudio = GetComponent<CharAudio>();
		_charVisuals = GetComponent<CharVisuals>();

	}

	// Declared before Update().
	private void Start() {

		SetCursorTo(false);

	}

	// This function will trigger when we gain a connection to the server.
	public override void OnStartServer() {

		// Make sure we've initialised.
		if (_charControls == null)
			GetReferences();

		// Load Game Settings.
		MapData mapData = FindObjectOfType<MapData>();
		_charSpells._difficulty = mapData.typingDifficulty;

		// Load Player Settings.
		PlayerData playerData = FindObjectOfType<PlayerData>();
		_charVisuals._NecromancerStyle = playerData.playerCharacter;
		_charSpells._SpellLoadout = playerData.playerSpells;	
		
		GetComponent<KeyboardTracker>()._Keybindings = playerData.playerHotkeys;

	}

	// This function will trigger with a client gains authority over this object.
	public override void OnStartAuthority() {

		// The player who owns this script becomes the target of their input and their camera.
		GetComponent<DeviceTracker>().enabled = true;

		// We also want to declare that we are the primary target of the aesthetic elements in the game.
		_charVisuals.ResetCamera();
		_charVisuals.ResetFocals();
		_charAudio._local = true;

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	/* ----------------------------------------------------------------------------- */
	// Networking Player Input.

	[Command] public void CmdSyncStruct(RawDataInput inputData) => RpcSyncStruct(inputData);
	[ClientRpc] public void RpcSyncStruct(RawDataInput inputData) => TranslateInput(inputData);

	/* ----------------------------------------------------------------------------- */
	// Translating that input into game functions.

	// A call that recieves inputs from the associated scripts. Any Inputs we're recieving are presented here and then translated into function-calls.
	public void TranslateInput(RawDataInput rdi) {

		// Check the current player state and execute arguments based on it.
		switch (_playerState) {

			/* ----------------------------------------------------------------------------- */
			case (PlayerState.Idle):

				/*// Pressing "Pause"
				if (rdi._ablKeys[0]) {

					// #TODO: Implement pause menu here.
					// This should be done through additive loading, and called as a function in another script from this class.

					// Save our old state, set our new state, and halt movement.
					_lastState = _playerState;

					_playerState = PlayerState.Paused;
					SetCursorTo(true);

					_charControls.TrajectoryChange();

				} else */

				// Pressing "Spellcast"
				if (rdi._ablKeys[1]) {

					// Communicate with CharSpells.cs and begin the 'Spellcasting Phase' from this point.
					_charSpells.TypeStatus(true);

					// Set our state to spellcasting and halt movement.
					_playerState = PlayerState.Spellcasting;
					_charControls.TrajectoryChange();

				}

				// If no commands are being pressed, process movement commands
				else {
					_charControls.TrajectoryChange(rdi._aflAxis);
				}
				
				break;

			/* ----------------------------------------------------------------------------- */
			case (PlayerState.Spellcasting):

				// Pressing Escape
				if (rdi._ablKeys[0]) {
					
					_charSpells.TypeStatus(false, true);
					_playerState = PlayerState.Idle;

				}

				// Pressing Enter
				else if (rdi._ablKeys[1]) {

					_charSpells.TypeStatus(false);
					_playerState = PlayerState.Idle;

				}

				break;

			/* ----------------------------------------------------------------------------- */
			/*case (PlayerState.Paused):

				// Pressing Escape
				if (rdi._ablKeys[0]) {

					// #TODO: Implement ability to dismiss pause menu here.
					// Same as the sister function above, except we also want to be able to deload the pause menu. Function should be called from here.

					// Load the state that the player was in before this function was called.
					_playerState = _lastState;
					SetCursorTo(false);

				}
				
				break;*/
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

	/*// A small helper function to include an animation lock to State Switching in some circumstances.
	private IEnumerator AnimationLock(PlayerState updatedState, RawDataInput rdi, float flWait) {

		// Make sure we can't further issue commands while we're locked into something.
		_playerState = PlayerState.Disabled;

		yield return new WaitForSeconds(flWait);
		
		// Change to the newly issued state and correct movement to the last pressed Axis-Keys.
		_playerState = updatedState;
		_charControls.TrajectoryChange(rdi._aflAxis);

	}*/

}