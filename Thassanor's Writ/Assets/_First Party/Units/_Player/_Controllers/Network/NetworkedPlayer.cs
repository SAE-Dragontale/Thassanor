/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			NetworkedPlayer.cs
   Version:			0.1.2
   Description: 	
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayer : NetworkBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public GameObject _playerPrefab;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before class calls or functions.
	public override void OnStartAuthority() {

		CmdCreatePlayer();

	}

	// In order to properly instantiate our player, we need to call it through a Networked method.
	[Command] private void CmdCreatePlayer() {

		// First, we need to instantiate our player and create a reference to it so we can network sync it.
		GameObject player = Instantiate(_playerPrefab);
		NetworkServer.SpawnWithClientAuthority(player, connectionToClient);

		// Then the server executes any customisation and organisation to all the player computers.
		RpcCreatePlayer(player);

	}

	[ClientRpc] private void RpcCreatePlayer(GameObject player) {

		Debug.Log($"I'm executing [RpcCreatePlayer] with {connectionToClient}");

		// We're just quickly going to assign ourselves under the ActiveNetworkedPlayers group to keep the hierarchy tidy.
		transform.parent = GameObject.Find("ActiveNetworkedPlayers")?.transform;
		transform.name = $"Player {connectionToClient}";

		// Then we're going to do the same for our player object.
		player.transform.parent = transform;
		player.name = player.GetComponent<CharVisuals>()._necromancerStyle._stUnitName;

		// We also want to attach our camera to our primary gameobject.
		Camera.main.GetComponent<CameraPlayer>()._ltrCameraFocus.Add(transform.GetChild(0));

	}

	// 
	private void OrganiseExistingPlayers() {



	}

	/* ----------------------------------------------------------------------------- */

}
