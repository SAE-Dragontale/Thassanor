using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class TestNetworkLobby : LobbyHook {

    //when player loads, takes lobbyPlayer(player info) and gameplayer (gameinfo), info is passed and synced from lobbyPlayer to gamePlayer 
public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        SetupLocalPlayer localPlayer = gamePlayer.GetComponent<SetupLocalPlayer>();

        localPlayer.pname = lobby.playerName;
        localPlayer.playerColor = lobby.playerColor;
    }
}
