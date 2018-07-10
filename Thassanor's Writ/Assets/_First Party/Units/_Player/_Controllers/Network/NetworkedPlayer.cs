/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			NetworkedPlayer.cs
   Version:			0.1.0
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
	private void Start () {

		// First of all, if we are not the local player, we don't want this script to run. So we're going to destroy it.
		if (!isLocalPlayer)
			return;

		// Now that we've confirmed we are the player, we can start running our Basic Player Setup.
		CmdCreatePlayer();

	}

	// In order to properly instantiate our player, we need to call it through a Networked method.
	// [Command] private GameObject CmdCreatePlayer() {
	[Command] private void CmdCreatePlayer() {

		// First, we need to instantiate our player and create a reference to it so we can network sync it.
		GameObject player = Instantiate(_playerPrefab);
		NetworkServer.SpawnWithClientAuthority(player, connectionToClient);

		// Then the server executes any customisation and organisation to all the player computers.
		RpcCreatePlayer(player);

	}

	[ClientRpc] private void RpcCreatePlayer(GameObject player) {

		// We're just quickly going to assign ourselves under the ActiveNetworkedPlayers group to keep the hierarchy tidy.
		transform.parent = GameObject.Find("ActiveNetworkedPlayers")?.transform;
		transform.name = $"Player {playerControllerId}";

		// Then we're going to do the same for our player object.
		player.transform.parent = transform;
		player.name = player.GetComponent<CharVisuals>()._necromancerStyle._stUnitName;

		// We also want to attach our camera to our primary gameobject.
		Camera.main.GetComponent<CameraPlayer>()._ltrCameraFocus.Add(transform.GetChild(0));

	}

	/* ----------------------------------------------------------------------------- */
	
}
