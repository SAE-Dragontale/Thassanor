// Script: BoardGeneration.cs
// Date Updated: 15/06/2018
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
	public GameObject[] _outerWallTiles;  

	[Space]
	[Header("Board Components")]
	public GameObject _boardHolder;                           // GameObject that acts as a container for all other tiles.
	public List<GameObject> _tileList = new List<GameObject>();
	public List<GameObject> _wallList = new List<GameObject>();
	public List<GameObject> _propList = new List<GameObject>();
    public List<GameObject> _waterList = new List<GameObject>();
	private TileType[][] _tiles;                               // A jagged array of tile types representing the board, like a grid.


	[Space]
	[Header("UI Components")]
	public Text _txtBoardSize;
	public Text _txtWaterCount;
	public Text _txtTownCount;


	private void Start ()
	{
        _simplexNoise = new OpenSimplexNoise(_itSeed);
		_playerRef = GameObject.FindGameObjectWithTag ("Player");
		// Create the board holder.
		_boardHolder = new GameObject("BoardHolder");

		SetupTilesArray ();
	
		InstantiateTiles ();
		InstantiateOuterWalls (); 			

		_txtBoardSize.text = "Board Size: " + _columns + " x " + _rows + " = " + _rows*_columns;		
		_txtTownCount.text = "Towns: " + _curTownCount;	
		_txtWaterCount.text = "Individual Water Tiles: " + _waterList.Count;
		
	}

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

	//Function to create a tile
	bool hasInstantiated = false;
	void InstantiateTiles ()
	{		
		//the amount of tiles which spreads the towns apart
		int townSpreadCur = 0;
		//keeps this a constant size compared to the grid/map size
		_townSpread = Mathf.Max(_columns,_rows) * _townSpread;


		for (int x = 0; x < _tiles.Length; x++)
		{
			for (int z = 0; z < _tiles[x].Length; z++)
            { 
				hasInstantiated = false;
                //THIS WILL RETURN -1 TO 1
				_fltPerlinValue = (float)_simplexNoise.Evaluate((double)(x * .5f), (double)(z * 0.5f));
                //_fltPerlinValue = Mathf.PerlinNoise(_itSeed * sampleX * 0.005f, _itSeed / sampleY * 0.005f);

				//---------------------------------------------------------------------------------------------------------------------------------------------------//
				//say at half the rows have generated, stop, and duplicate tiles but in reverse to mirror the two halves
				//---------------------------------------------------------------------------------------------------------------------------------------------------//

				//creates the floor for the whole grid
				InstantiateFromArray (_floorTiles, x, z);				

				//here to say if we're not on an outer edge tile & instantiate on top of the existing tile
				if (x != 0 || z != 0 || x != _tiles.Length-1 || z != _tiles.Length-1) 
				{
					//perlin value for water
					if (_fltPerlinValue < _waterDensity) 
					{
						InstantiateWater (_waterTiles, x, z);
						
					}

					//perlin value for towns to spawn
					if (_fltPerlinValue < .4f && _fltPerlinValue > .35f && townSpreadCur == _townSpread) 
					{
                        if (_curTownCount != _MaxTownCount)
                        {
                            InstantiateTown(_townTiles, x, z);
							townSpreadCur = 0;
                        }
                    } 
				} 

				//adds 1 to the townspread step per tile
				if(townSpreadCur < _townSpread)
				{
				townSpreadCur++;		

				}
			}
		}

		//_waterList.RemoveAll(GameObject => GameObject != null);
		//_waterList.RemoveAll(GameObject => GameObject == GameObject.exists);

	}

	//creates the border
	void InstantiateOuterWalls ()
	{
		//Array Elements - 8
		// 0 - Bottom
		// 1 - Left
		// 2 - Top
		// 3 - Right
		// 4 - Bottom Left Corner
		// 5 - Bottom Right Corner
		// 6 - Top Left Corner
		// 7 - Top Right Corner

		// The outer walls are one unit left, right, up and down from the board.
		float leftEdgeX = -1f;
		float rightEdgeX = _columns;
		float bottomEdgeZ = -1f;
		float topEdgeZ = _rows;

		// Instantiate both vertical walls (one on each side).
		InstantiateVerticalOuterWall (leftEdgeX, bottomEdgeZ, topEdgeZ);
		InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeZ, topEdgeZ);

		// Instantiate both horizontal walls, these are one in left and right from the outer walls.
		InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeZ);
		InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeZ);
	}

	//creates the border on the left and right hand sides
	void InstantiateVerticalOuterWall (float xCoord, float startingZ, float endingZ)
	{
		
		// Start the loop at the starting value for Y.
		float currentZ = startingZ;

		// While the value for Y is less than the end value...
		while (currentZ <= endingZ)
		{
			// ... instantiate an outer wall tile at the x coordinate and the current y coordinate.
			InstantiateWallsFromArray(_outerWallTiles, xCoord, currentZ);

			currentZ++;
		}
	}

	//creates the border on the top and bottom sides
	void InstantiateHorizontalOuterWall (float startingX, float endingX, float zCoord)
	{
		// Start the loop at the starting value for X.
		float currentX = startingX;

		// While the value for X is less than the end value...
		while (currentX <= endingX)
		{
			// ... instantiate an outer wall tile at the y coordinate and the current x coordinate.
			InstantiateWallsFromArray (_outerWallTiles, currentX, zCoord);

			currentX++;
		}
	}


	GameObject tileInstance;
	void InstantiateWallsFromArray (GameObject[] prefabs, float xCoord, float zCoord)
	{
		// The position to be instantiated at is based on the coordinates.
		Vector3 position = new Vector3(xCoord, 0f , zCoord);

		//checks left wall
		if (xCoord == -1) 
		{
			//this checks the left wall, and says at the top and botttom wall piece, generate a corner tile
			if (zCoord == _tiles[0].Length || zCoord == -1) 
			{			
				if (zCoord == _tiles.Length) 
				{
					tileInstance = Instantiate (prefabs [6], position, Quaternion.identity) as GameObject;
				}	
				if (zCoord == -1) 
				{
					tileInstance = Instantiate (prefabs [4], position, Quaternion.identity) as GameObject;
				}
			} 
			else 
			{
				tileInstance = Instantiate (prefabs [1], position, Quaternion.identity) as GameObject;
			}
		} 
		//checks right wall
		else if (xCoord == _tiles.Length) 
		{
			//this checks the right wall, and says at the top and botttom wall piece, generate a corner tile
			if (zCoord == _tiles[0].Length || zCoord == -1) 
			{			
				if (zCoord == _tiles.Length) 
				{
					//create corner tile at length
					tileInstance = Instantiate (prefabs [7], position, Quaternion.identity) as GameObject;
				}	
				if (zCoord == -1) 
				{
					//create corner tile at start
					tileInstance = Instantiate (prefabs [5], position, Quaternion.identity) as GameObject;
				}
			} 
			else 
			{
				tileInstance = Instantiate (prefabs [3], position, Quaternion.identity) as GameObject;

			}
		}
		
		//checks front wall
		if (zCoord == -1) 
		{
			//this stops tiles from generating at the corners of the front line of walls
			if (xCoord == _tiles.Length || xCoord == -1) 
			{		
			}  
			else 
			{
				tileInstance = Instantiate (prefabs [0], position, Quaternion.identity) as GameObject;
			}
		} 
		//checks back wall with the second element in the jagged array
		else if (zCoord == _tiles[0].Length) 
		{
			//this stops tiles from generating at the corners of the back line of walls
			if (xCoord == _tiles.Length || xCoord == -1) 
			{			
				if (xCoord == _tiles.Length) 
				{
					//create corner tile at length
					tileInstance = Instantiate (prefabs [7], position, Quaternion.identity) as GameObject;
				}	
				if (xCoord == -1) 
				{
					//create corner tile at start
					tileInstance = Instantiate (prefabs [6], position, Quaternion.identity) as GameObject;
				}
			} 
			else 
			{
				tileInstance = Instantiate (prefabs [2], position, Quaternion.identity) as GameObject;

			}
		}
		
		tileInstance.name = "Wall _x-" + xCoord + " _z-" + zCoord;
		_wallList.Add (tileInstance);

		// Set the tile's parent to the board holder.
		tileInstance.transform.parent = _boardHolder.transform;
	}


	void InstantiateFromArray (GameObject[] prefabs, float xCoord, float zCoord)
	{

		//sets the player position to the first tile
		if (xCoord == 0 && zCoord == 0) 
		{
			_playerRef.transform.position = new Vector3(0,.6f,0f);
		}

		// Create a random index for the instantiated tile.
		int randomIndex = Random.Range(0, prefabs.Length);

		Vector3 position = new Vector3(xCoord, 0f, zCoord);        
		// Create an instance of the prefab from the random index of the array.
		GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

		//gives the tiles grid positions
		tileInstance.name = "Tile _x-" + xCoord + " _z-" + zCoord;
		//adds the tile generated into a list for a reference to each of them
		_tileList.Add (tileInstance);
		// Set the tile's parent to the board holder.
		tileInstance.transform.parent = _boardHolder.transform;
		//tileInstance.transform.rotation = Quaternion.Euler(new Vector3(_isometricAngle.x, _isometricAngle.y, _isometricAngle.z));

	}

	void InstantiateTown (GameObject[] prefabs, float xCoord, float zCoord)
	{
		// Create a random index for the instantiated tile.
		int randomIndex = Random.Range(0, prefabs.Length);

		Vector3 position = new Vector3(xCoord, 0.03f, zCoord);
		// Create an instance of the prefab from the random index of the array.
		GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

		//towncount is to keep track of how many towns have been created
		Vector3 newPos = tileInstance.transform.position;
		newPos = newPos + new Vector3(0,1,0);
		tileInstance.transform.position = newPos;
		tileInstance.name = "Town_#" + _curTownCount;
		_curTownCount++;

		//adds the tile generated into a list for a reference to each of them
		_propList.Add (tileInstance);
		// Set the tile's parent to the board holder.
		tileInstance.transform.parent = _boardHolder.transform;
		//tileInstance.transform.rotation = Quaternion.Euler(new Vector3(_isometricAngle.x, _isometricAngle.y, _isometricAngle.z));

	}

	private int waterNo = 0;	
	
    void InstantiateWater (GameObject[] prefabs, float xCoord, float zCoord)
	{
		Vector3 position = new Vector3(xCoord,.02f, zCoord);

		//loop to grow the pool
		for (int it = 0; it < _waterSize; it++)
        {
            GameObject tileInstance = Instantiate(prefabs[0], position, Quaternion.identity) as GameObject;
			tileInstance.name = "Water_#" + waterNo;

            //add this position to a list to check against
            _waterList.Add(tileInstance);

            //set the position with random adjustment in a direction - change to make it less random or have no randomness for the same seed
            position = position + new Vector3(Random.Range(-1,2),0,Random.Range(-1,2));
			//Debug.Log( position = position + new Vector3(Mathf.RoundToInt(_fltPerlinValue),0,Mathf.RoundToInt(_fltPerlinValue)));

            //Do a check to make sure water doesnt overlap itself or other objects
		

            foreach (GameObject tile in _waterList) 
			{
				
				if (position == tile.transform.position)
                {
					Destroy(tileInstance);
                } 
				else
                {
                    tileInstance.transform.parent = _boardHolder.transform;
                }
            }
			waterNo++;
        }
			
    }

}
