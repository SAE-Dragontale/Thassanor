// Script: BoardGeneration.cs
// Date Updated: 26/06/2018
// Author & Contributors: Eric Cox
// Purpose: The creation and design details of the maps for the game. Water/Towns/Props/Size. etc.  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoardGeneration : MonoBehaviour {
	
	// The type of tile that will be laid in a specific position.
	public enum TileType
	{
		Wall, Floor,
	}

	[Space]

	//player reference
	public GameObject _playerRef;

    [Header("Grid Components")]	
    public int _itSeed;                                         //grid seed for generation
    OpenSimplexNoise _simplexNoise;								//reference to simplex noise script to get grid style
    float _fltPerlinValue;

	[Space]

    [Tooltip("Width.")]
    public int _columns = 12;                                   // The number of columns on the board (how wide it will be).
    [Tooltip("Height.")]
	public int _rows = 12;                                      // The number of rows on the board (how tall it will be).

	[Space]

    [Range(-.5f,.5f)]
    public float _waterDensity;
    [Range(1, 9)]
    public int _waterSize;

	[Space]

    [Tooltip("This value is multiplied by the column size or row size, whichever is biggest. This keeps size & spread dynamic.")]
    [Range(2, 9)]
    public int _townSpread;
	public int _MaxTownCount = 4;	
	[SerializeField] private int _curTownCount = 0;	

	[Space]
	
	public GameObject[] _floorTiles;                            // An array of floor tile prefabs.
	public GameObject[] _townTiles;        
	public GameObject[] _waterTiles;       

	[Space]
	[Header("Board Components")]
	public GameObject _boardHolder;                           // GameObject that acts as a container for all other tiles.
	public List<GameObject> _tileList = new List<GameObject>();
	public List<GameObject> _propList = new List<GameObject>();
    public List<GameObject> _waterList = new List<GameObject>();
	public TileType[][] _tiles;                               // A jagged array of tile types representing the board, like a grid.


	[Space]
	[Header("UI Components")]
	public Text _txtBoardSize;
	public Text _txtWaterCount;
	public Text _txtTownCount;


	[Space]
	[Header("GameObject References")]
	public BorderGeneration _borderGenRef;

	public GameObject _tileInstance;
		
 //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	void Awake()
	{
		_playerRef = GameObject.FindGameObjectWithTag ("Player");
		_playerRef.SetActive(true);

	}


	private void Start ()
	{
		_borderGenRef = gameObject.GetComponent<BorderGeneration>();

        _simplexNoise = new OpenSimplexNoise(_itSeed);
		// Create the board holder.
		_boardHolder = new GameObject("BoardHolder");

		SetupTilesArray ();
	
		InstantiateTiles ();
		_borderGenRef.InstantiateOuterWalls (); 			

		_txtBoardSize.text = "Columns: " + _columns + " | Rows: " + _rows + " | Ground Tiles: " + _tileList.Count;		
		_txtTownCount.text = "Towns: " + _curTownCount;	
		_txtWaterCount.text = "Water Tiles: " + _waterList.Count;
				
		StartCoroutine(DelayedStart());

	}

	public IEnumerator DelayedStart()
	{
		yield return new WaitForSeconds(.7f);
		_playerRef.SetActive(true);
	}
 //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	//Function to set length of grid directions
	void SetupTilesArray ()
	{
		// Set the tiles jagged array to the correct width.
		_tiles = new TileType[_columns][];

		// Go through all the tile arrays...
		for (int i = 0; i < _tiles.Length; i++)
		{
			// and set each tile array is the correct height.
			_tiles[i] = new TileType[_rows];
		}
	}

 //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

	List<GameObject> mirrorTileList = new List<GameObject>();
	int mirrorListCount;
	//Function to create a tile
	void InstantiateTiles ()
	{		
		//the amount of tiles which spreads the towns apart
		int townSpreadCur = 0;
		//keeps this a constant size compared to the grid/map size
		_townSpread = Mathf.Max(_columns,_rows) * _townSpread;
				
		for (int z = 0; z < _tiles[0].Length; z++)
		{	
			//random half
			if(z < _rows/2)
			{
				for (int x = 0; x < _tiles.Length; x++)
				{ 
					//THIS WILL RETURN -1 TO 1
					_fltPerlinValue = (float)_simplexNoise.Evaluate((double)(x * .5f), (double)(z * 0.5f));
				
					InstantiateFromArray(_floorTiles,x,z);	
					mirrorTileList.Add(floorTileInstance);
					mirrorListCount++;	

					//here to say if we're not on an outer edge tile & instantiate on top of the existing tile
					if (x != 0 || z != 0 || x != _tiles.Length-1 || z != _tiles[0].Length-1) 
					{
						//perlin value for water
						if (_fltPerlinValue < _waterDensity) 
						{
							InstantiateWater (_waterTiles, x, z);									
						}
						
						//perlin value for towns to spawn
						if (_fltPerlinValue < .3f && _fltPerlinValue > -.2f && townSpreadCur == _townSpread) 
						{
							if (_curTownCount != _MaxTownCount)
							{
								InstantiateTown(_townTiles, x, z);
								townSpreadCur = 0;
							}
						} 						
					} 					
					//this is the check(s) for 'steps' between components
					if(townSpreadCur < _townSpread)
					{
						townSpreadCur++;
					}
				}
			}
			else 		//non random half
			{				
				//for (int x = 0; x < _tiles.Length; x++)  this results in a vertical and horizontal flip, so x town will be to the right of both players
				for (int x = _tiles.Length - 1; x >= 0; x--) //this results in a vertical flip, so x town will be to the right of one player, and to the left for the other
				{ 						
					Vector3 position = new Vector3(x, 0f, z);    

					floorTileInstance = Instantiate(mirrorTileList[mirrorListCount - 1], position, Quaternion.identity) as GameObject;
					floorTileInstance.name = mirrorTileList[mirrorListCount - 1].name;
					_tileList.Add (floorTileInstance);
					floorTileInstance.transform.parent = _boardHolder.transform;

					mirrorListCount--;					
					
					
					//this is the check(s) for 'steps' between components
					if(townSpreadCur < _townSpread)
					{
						townSpreadCur++;
					}
				}

			}
		}
		
	}	

 //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
    
	GameObject floorTileInstance;
	void InstantiateFromArray (GameObject[] prefabs, float xCoord, float zCoord)
	{		
		//sets the player position to the first tile
		if (xCoord == (_columns/2) && zCoord == 0) 
		{
			_playerRef.transform.position = new Vector3(_columns/2,.6f,0f);
		}

		// Create a random index for the instantiated tile.
		int randomIndex = Random.Range(0, prefabs.Length);
		Vector3 position = new Vector3(xCoord, 0f, zCoord);    
		int index = Mathf.RoundToInt(_fltPerlinValue);
		if(index < 0)
		{index = 0;}


		floorTileInstance = Instantiate(prefabs[index], position, Quaternion.identity) as GameObject;
		floorTileInstance.name = "Tile _x-" + xCoord + " _z-" + zCoord;
		_tileList.Add (floorTileInstance);	
		floorTileInstance.transform.parent = _boardHolder.transform;

	}

 //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	void InstantiateTown (GameObject[] prefabs, float xCoord, float zCoord)
	{
		// Create a random index for the instantiated tile.
		int randomIndex = Random.Range(0, prefabs.Length);

		Vector3 position = new Vector3(xCoord, 0, zCoord);
		// Create an instance of the prefab from the random index of the array.
		GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

		Vector3 newPos = tileInstance.transform.position;
		newPos = newPos + new Vector3(0,.95f,0);
		tileInstance.transform.position = newPos;
		tileInstance.name = "Town_#" + _curTownCount;
		_curTownCount++;
		//adds the tile generated into a list for a reference to each of them
		_propList.Add (tileInstance);

		RaycastHit hit;
		if (Physics.Raycast(tileInstance.transform.position,-Vector3.up, out hit) && hit.transform.tag == "Ground")
		{
			tileInstance.transform.parent = hit.transform;
		}		
		
	}

 //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	private int waterNo = 0;		
    void InstantiateWater (GameObject[] prefabs, float xCoord, float zCoord)
	{
		Vector3 position = new Vector3(xCoord,0.05f, zCoord);
		
		//loop to grow the pool
		for (int it = 0; it < _waterSize; it++)
        {
            GameObject tileInstance = Instantiate(prefabs[0], position, Quaternion.identity) as GameObject;
			tileInstance.name = "Water_#" + waterNo;
            _waterList.Add(tileInstance);

            //set the position with random adjustment in a direction - change to make it less random or have no randomness for the same seed
            position = position + new Vector3(Random.Range(-1,2),0,Random.Range(-1,2));		

			RaycastHit hit;
			if (Physics.Raycast(tileInstance.transform.position,-Vector3.up, out hit) && hit.transform.tag == "Ground")
			{			
				tileInstance.transform.parent = hit.transform;
			}
			else
			{
				Destroy(tileInstance);
			}

            //check to make sure water doesnt overlap itself or other objects
            foreach (GameObject tile in _waterList) 
			{				
				if (position == tile.transform.position)
                {
					//destroys overlapping tile and instantiates ground tile in place
					Destroy(tileInstance);
					InstantiateFromArray (_floorTiles, xCoord, zCoord);
                } 
				else
                {
					//if water is out of bounds, destroy
					if (tileInstance.transform.position.x < 0 || tileInstance.transform.position.z < 0 || tileInstance.transform.position.x > _tiles.Length-1 || tileInstance.transform.position.z > _tiles[0].Length-1) 
					{						
						Destroy(tileInstance);
					}
                }
            }
			waterNo++;
        }
			
    }

}
