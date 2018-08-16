/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			NetworkedPlayer.cs
   Version:			0.1.4
   Description: 	
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayer : NetworkBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Space] [Header("Player References")]
	public GameObject _playerPrefab;
	public GameObject _player;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before class calls or functions.
	public override void OnStartAuthority() {

		CmdCreatePlayer();      // SERVER: Spawn our player.
		
	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Networking Commands
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// In order to properly instantiate our player, we need to call it through a Networked method.
	[Command] private void CmdCreatePlayer() {

		// We're taking our instantiated player and we're asking the server to spawn it for us under our authority.
		_player = Instantiate(_playerPrefab);
		NetworkServer.SpawnWithClientAuthority(_player, connectionToClient);

		// Then we're quickly going to assign ourselves under the ActiveNetworkedPlayers group to keep the hierarchy tidy.
		transform.parent = GameObject.Find("ActiveNetworkedPlayers")?.transform;
		transform.name = $"Player {netId}";

		// Then we're going to do the same for our player object.
		_player.transform.parent = transform;
		_player.name = _player.GetComponent<CharVisuals>()._necromancerStyle._unitName;

	}

	/* ----------------------------------------------------------------------------- */

}
