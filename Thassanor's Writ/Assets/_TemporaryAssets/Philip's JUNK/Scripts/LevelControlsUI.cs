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

    public GameObject thassanor;

    //inspector components
    public InputField seedField;
    //public Dropdown mapSizeDropDown;
    public InputField waterSizeField;
    public InputField townSpreadField;
    public InputField maxTownCountField;
    //public Dropdown typingDifficultyDropDown;

    //Level options data
    public int _itSeed;
    public float _columns;
    public float _rows;
    public int _waterSize;
    public int _townSpread;
    public int _maxTownCount;
    public int typingDifficulty;

    // Use this for initialization
    void Start()
    {
        thassanor = GameObject.Find("[Thassanor]");
        seedField.onValueChanged.AddListener(SeedInputField);
        thassanor.GetComponent<MapData>()._columns = 20;
        thassanor.GetComponent<MapData>()._rows = 20;
        thassanor.GetComponent<MapData>()._waterSize = 4;
        thassanor.GetComponent<MapData>()._townSpread = 2;
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
            thassanor.GetComponent<MapData>()._columns = 20;
            thassanor.GetComponent<MapData>()._rows = 20;
            _columns = 20f;
            _rows = 20f;

        }
        else if(mapSize == 1)
        {
            thassanor.GetComponent<MapData>()._columns = 40;
            thassanor.GetComponent<MapData>()._rows = 40;
            _columns = 40f;
            _rows = 40f;
        }
        else if(mapSize == 2)
        {
            thassanor.GetComponent<MapData>()._columns = 80;
            thassanor.GetComponent<MapData>()._rows = 80;
            _columns = 80f;
            _rows = 80f;
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
        thassanor.GetComponent<MapData>()._maxTownCount = Mathf.Clamp(int.Parse(maxTownCount), 1, Mathf.RoundToInt(_columns * 0.2f));
        maxTownCountField.text = thassanor.GetComponent<MapData>()._maxTownCount.ToString();
    }

    public void TypingDifficulty(int Index)
    {
        //typingDifficulty = Index;
        thassanor.GetComponent<MapData>().typingDifficulty = Index;
    }


}
