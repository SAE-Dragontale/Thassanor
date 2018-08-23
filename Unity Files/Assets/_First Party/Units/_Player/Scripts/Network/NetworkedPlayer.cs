/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   Best Dude Award:	Karl Pytte
   File:			NetworkedPlayer.cs
   Version:			0.2.0
   Description: 	
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkedPlayer : NetworkBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Space]
	[Header("Player References")]
	public GameObject _playerPrefab;
	public GameObject _player;

	[Space]
	[Header("Unit References")]
	public GameObject _unitPrefab;
	public GameObject[] _everyGroup = new GameObject[2];
	public UnitStyle[] _everyStyle = new UnitStyle[2];

	private NetworkManager netManager;

	private int playerCount;

    private bool isStart;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void Start() {
		netManager = FindObjectOfType<NetworkManager>();
        isStart = true;
        //if (isServer)
        //{
        //    CmdCreatePlayer();
        //}
    }

    IEnumerator WaitForReady()
    {
        while (!connectionToClient.isReady)
        {
            yield return new WaitForSeconds(0.25f);
        }
        CmdCreatePlayer();
    }

    // Called before class calls or functions.
    public override void OnStartAuthority() {


        CmdCreatePlayer();

		for (int i = 0; i < _everyGroup.Length; i++)
			CmdCreateUnitGroup(_everyStyle[i], i);

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Networking Commands
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	/* ----------------------------------------------------------------------------- */
	// Player

	private void Update() {
		SendPlayerData();
        if (isStart)
        {
            CmdCreatePlayer();

            for (int i = 0; i < _everyGroup.Length; i++)
                CmdCreateUnitGroup(_everyStyle[i], i);

            isStart = false;
        }
	}

	void SendPlayerData() {
		if (netManager.numPlayers != playerCount) {
			playerCount = netManager.numPlayers;
			RpcAssignPlayer(_player);
			for (int i = 0; i < _everyGroup.Length; i++) {
				RpcAssignUnits(_everyGroup[i], i);
			}
		}
	}

	// In order to properly instantiate our player, we need to call it through a Networked method.
	[Command]
	private void CmdCreatePlayer() {
        if (connectionToClient.isReady)
        {
            Debug.Log("Assign player");
            // We're taking our instantiated player and we're asking the server to spawn it for us under our authority.
            _player = Instantiate(_playerPrefab);
            NetworkServer.SpawnWithClientAuthority(_player, connectionToClient);

            //AssignPlayer(_player);
            RpcAssignPlayer(_player);
        }
        else
        {
            StartCoroutine(WaitForReady());
        }
        if (_player) {
			return;
		}
		

	}

	[Command] private void CmdNewGameObject(GameObject thingToSpawn) {



	}

	// private void RpcAssignPlayer() => AssignPlayer(_player);
	[ClientRpc]
	private void RpcAssignPlayer(GameObject myPlayer) {
	
		
		_player = myPlayer;
		
		/*
		GameObject[] players = GameObject.FindGameObjectsWithTag("NetworkedPlayerObj");
		Debug.Log("players " + players.Length);

		for (int i = 0; i < players.Length; i++) {
			if (players[i].GetComponent<NetworkIdentity>().hasAuthority) {
				_player = players[i];
				Debug.Log("player " + _player.name);
				break;
			}
		}*/

		// Then we're quickly going to assign ourselves under the ActiveNetworkedPlayers group to keep the hierarchy tidy.
		transform.parent = GameObject.Find("ActiveNetworkedPlayers").transform;
		transform.name = $"Player netId{netId}";

		// Then we're going to do the same for our player object.
		if (_player)
		_player.transform.parent = transform;
		//Invoke("AssignPlayerTransform", 0.2f);

	}

	void AssignPlayerTransform() {
		
	}

	/* ----------------------------------------------------------------------------- */
	// UnitGroup

	[Command]
	private void CmdCreateUnitGroup(UnitStyle unitStyle, int i) {
		_everyGroup[i] = Instantiate(_unitPrefab);
		NetworkServer.SpawnWithClientAuthority(_everyGroup[i], connectionToClient);

		//AssignUnits(i);
		RpcAssignUnits(_everyGroup[i],i);

	}

	/*
	[Command] private void CmdCreateUnitGroup(GameObject unitGroup, UnitStyle unitStyle, int i) {

		_everyGroup[i] = Instantiate(unitGroup);
		NetworkServer.SpawnWithClientAuthority(_everyGroup[i], connectionToClient);

		AssignUnits(i);
		RpcAssignUnits(i);

	}*/


	[ClientRpc]
	private void RpcAssignUnits(GameObject eGroup, int i) {
		_everyGroup[i] = eGroup;

		if (!_everyGroup[i]) {
			return;
		}

		//_everyGroup[i].transform.parent = transform;
		_everyGroup[i].transform.parent = transform;
		_everyGroup[i].transform.name = $"{_everyStyle[i].name} netId{netId}";

		UnitGroup unit = _everyGroup[i].GetComponent<UnitGroup>();
		unit._UnitStyle = _everyStyle[i];
		unit._Anchor = _player.transform;

		GetComponentInChildren<CharSpells>()._minions[i] = _everyGroup[i].GetComponent<UnitGroup>();

	}
	/*
	[ClientRpc]
	private void RpcAssignUnits(int i) {

		_everyGroup[i].transform.parent = transform;
		_everyGroup[i].transform.name = $"{_everyStyle[i].name} netId{netId}";

		UnitGroup unit = _everyGroup[i].GetComponent<UnitGroup>();
		unit._UnitStyle = _everyStyle[i];
		unit._Anchor = _player.transform;

		_player.GetComponentInChildren<CharSpells>()._minions[i] = _everyGroup[i].GetComponent<UnitGroup>();

	}*/

	/* ----------------------------------------------------------------------------- */

}
