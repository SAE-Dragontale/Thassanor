/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			NetworkedPlayer.cs
   Version:			0.2.0
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

	[Space] [Header("Unit References")]
	public GameObject _unitPrefab;
	public GameObject[] _everyGroup = new GameObject[2];
	public UnitStyle[] _everyStyle = new UnitStyle[2];

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before class calls or functions.
	public override void OnStartAuthority() {

		CmdCreatePlayer();

		for (int i = 0; i < _everyGroup.Length; i++)
			CmdCreateUnitGroup(_unitPrefab, _everyStyle[i], i);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Networking Commands
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	/* ----------------------------------------------------------------------------- */
	// Player

	// In order to properly instantiate our player, we need to call it through a Networked method.
	[Command] private void CmdCreatePlayer() {

		// We're taking our instantiated player and we're asking the server to spawn it for us under our authority.
		_player = Instantiate(_playerPrefab);
		NetworkServer.SpawnWithClientAuthority(_player, connectionToClient);

		AssignPlayer();
		RpcAssignPlayer();

	}

	[ClientRpc] private void RpcAssignPlayer() => AssignPlayer();

	private void AssignPlayer() {

		// Then we're quickly going to assign ourselves under the ActiveNetworkedPlayers group to keep the hierarchy tidy.
		transform.parent = GameObject.Find("ActiveNetworkedPlayers").transform;
		transform.name = $"Player netId{netId}";

		// Then we're going to do the same for our player object.
		_player.transform.parent = transform;

	}

	/* ----------------------------------------------------------------------------- */
	// UnitGroup

	[Command] private void CmdCreateUnitGroup(GameObject unitGroup, UnitStyle unitStyle, int i) {

		_everyGroup[i] = Instantiate(unitGroup);
		NetworkServer.SpawnWithClientAuthority(_everyGroup[i], connectionToClient);

		AssignUnits(i);
		RpcAssignUnits(i);

	}

	[ClientRpc] private void RpcAssignUnits(int i) => AssignUnits(i);

	private void AssignUnits(int i) {

		_everyGroup[i].transform.parent = transform;
		_everyGroup[i].transform.name = $"{_everyStyle[i].name} netId{netId}";

		UnitGroup unit = _everyGroup[i].GetComponent<UnitGroup>();
		unit._UnitStyle = _everyStyle[i];
		unit._Anchor = _player.transform;

		_player.GetComponentInChildren<CharSpells>()._minions[i] = _everyGroup[i].GetComponent<UnitGroup>();

	}

	/* ----------------------------------------------------------------------------- */

}
