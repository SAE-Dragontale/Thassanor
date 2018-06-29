/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			NetworkedPlayer.cs
   Version:			0.0.0
   Description: 	
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using System.Collections.Generic;
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
			Destroy(this);
		
		// Now that we've confirmed we are the player, we can start running our Basic Player Setup.
		CmdCreatePlayer();
		
		// TODO: We really want to be loading our player settings at some point. This should probably be done here, like this:
		// PlayerSetup(CmdCreatePlayer());

		// What this means, is we're going to need another CmdCreatePlayer() to return a script, or GameObject, after we've instantiated.
		// Then, we're taking that information and calling another function to set values on that GameObject, such as the Style and Hotkeys.

	}

	// In order to properly instantiate our player, we need to call it through a Networked method.
	// [Command] private GameObject CmdCreatePlayer() {
	[Command] private void CmdCreatePlayer() {

		// First, we need to instantiate our player and create a reference to it so we can network sync it.
		GameObject player = Instantiate(_playerPrefab);

		// Then, we want to make sure that we are in sole control of our player.
		player.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
		player.transform.parent = GameObject.Find("ActiveNetworkPlayers").transform;

		// TODO: Customise Player with Style.

		// TODO: Customie Player with Loadout.

		// Finally, we can ask the server to spawn the object under our control.
		NetworkServer.Spawn(player);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
	
	// Update is called once per frame.
	private void Update () {
		
	}

	/* ----------------------------------------------------------------------------- */
	
}
