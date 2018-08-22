/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Philip Ong
   File:			LevelControlsUI.cs
   Version:			0.0.0
   Description: 	
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControlsUI : MonoBehaviour {

    public Dragontale.Thassanor thassanor;

    //inspector components
    public InputField seedField;
    public Dropdown mapSizeDropDown;
    public InputField waterSizeField;
    public InputField townSpreadField;
    public InputField maxTownCountField;
    public Dropdown typingDifficultyDropDown;

    public List<GameObject> playerInfoList;
    public LobbyPlayer localPlayer;

    //Level options data
    //public int _itSeed;
    //public float _columns;
    //public float _rows;
    //public int _waterSize;
    //public int _townSpread;
    //public int _maxTownCount;
    //public int typingDifficulty;

    // Use this for initialization
    void Start()
    {
        thassanor = FindObjectOfType<Dragontale.Thassanor>();
        thassanor.GetComponent<MapData>()._columns = 5;
        thassanor.GetComponent<MapData>()._rows = 5;
        thassanor.GetComponent<MapData>()._waterSize = 4;
        thassanor.GetComponent<MapData>()._townSpread = 2;
        thassanor.GetComponent<MapData>().typingDifficulty = 1;
        thassanor.GetComponent<MapData>()._itSeed = 123123;
        thassanor.GetComponent<MapData>()._maxTownCount = 4;

        playerInfoList.AddRange(GameObject.FindGameObjectsWithTag("PlayerInfo"));
        foreach(GameObject player in playerInfoList)
        {
            if(player.GetComponent<LobbyPlayer>().nameInput.interactable == true)
            {
                localPlayer = player.GetComponent<LobbyPlayer>();
            }
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SeedInputField(string seed)
    {
        //_itSeed = int.Parse(seed);
        thassanor.GetComponent<MapData>()._itSeed = int.Parse(seed);
        
    }

    //sets the amount of tiles the size of the map will be depending on option selected
    public void MapSize(int mapSize)
    {
        if(mapSize == 0)
        {
            thassanor.GetComponent<MapData>()._columns = 5;
            thassanor.GetComponent<MapData>()._rows = 5;
        }
        else if(mapSize == 1)
        {
            thassanor.GetComponent<MapData>()._columns = 10;
            thassanor.GetComponent<MapData>()._rows = 10;
        }
        else if(mapSize == 2)
        {
            thassanor.GetComponent<MapData>()._columns = 15;
            thassanor.GetComponent<MapData>()._rows = 15;
        }
    }

    public void WaterSize(string waterSize)
    {
        //_waterSize = int.Parse(waterSize);
        //_waterSize = Mathf.Clamp(_waterSize, 4, 12);
        thassanor.GetComponent<MapData>()._waterSize = Mathf.Clamp(int.Parse(waterSize), 4, 12);
        waterSizeField.text = thassanor.GetComponent<MapData>()._waterSize.ToString();
    }

    public void TownSpread(string townSpread)
    {
        //_townSpread = int.Parse(townSpread);
        //_townSpread = Mathf.Clamp(_townSpread, 2, 12);
        thassanor.GetComponent<MapData>()._townSpread = Mathf.Clamp(int.Parse(townSpread), 2, 12);
        townSpreadField.text = thassanor.GetComponent<MapData>()._townSpread.ToString();
    }

    public void MaxTownCount(string maxTownCount)
    {
        //_maxTownCount = int.Parse(maxTownCount);
        //_maxTownCount = Mathf.Clamp(_maxTownCount, 1, Mathf.RoundToInt(_columns * 0.2f));
        thassanor.GetComponent<MapData>()._maxTownCount = Mathf.Clamp(int.Parse(maxTownCount), 1, Mathf.RoundToInt(thassanor.GetComponent<MapData>()._columns * 0.2f));
        maxTownCountField.text = thassanor.GetComponent<MapData>()._maxTownCount.ToString();
    }

    public void TypingDifficulty(int index)
    {
        //typingDifficulty = Index;
        thassanor.GetComponent<MapData>().typingDifficulty = index;
        //localPlayer.typingDifficulty = index;
        //localPlayer.CmdTypingDifficultyChanged(index);
    }


}
