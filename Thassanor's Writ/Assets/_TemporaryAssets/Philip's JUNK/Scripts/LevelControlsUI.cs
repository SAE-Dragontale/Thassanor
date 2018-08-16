/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Philip Ong
   File:			LevelControlsUI.cs
   Version:			0.0.0
   Description: 	
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelControlsUI : MonoBehaviour {

    //inspector components
    public InputField seedField;
    public Dropdown mapSizeDropDown;
    public InputField waterSizeField;
    public InputField townSpreadField;
    public InputField townCountField;

    public int typingDifficulty;

    public PlayerData playerData;
    public GameObject thassanor;

    //Level options data
    public int _itSeed;
    public int _columns;
    public int _rows;
    public int _waterSize;
    public int _townSpread;
    public int _maxTownCount;

    // Use this for initialization
    void Start () {
        thassanor = GameObject.Find("[Thassanor]");
        seedField.onValueChanged.AddListener(SeedInputField);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SeedInputField(string seed)
    {
        _itSeed = int.Parse(seed);
    }

    //sets the amount of tiles the size of the map will be depending on option selected
    public void MapSize(string mapSize)
    {
        if(mapSize == "Small")
        {
            thassanor.GetComponent<PlayerData>()._columns = 20;
            thassanor.GetComponent<PlayerData>()._rows = 20;

        }
        else if(mapSize == "Medium")
        {
            _columns = 40;
            _rows = 40;
        }
        else
        {
            _columns = 80;
            _rows = 80;
        }
    }

    public void WaterSize(string waterSize)
    {
        _waterSize = int.Parse(waterSize);
    }

    public void TownSpread(string townSpread)
    {
        _townSpread = int.Parse(townSpread);
    }

    public void MaxTownCount(string maxTownCount)
    {
        _maxTownCount = int.Parse(maxTownCount);
    }


}
