using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGeneration : MonoBehaviour {
	
	// The type of tile that will be laid in a specific position.
	public enum TileType
	{
		Wall, Floor,
	}

	[Space]
	// An array of outer wall tile prefabs.
	public GameObject _playerRef;

	[Header("Grid Components")]
	public int _columns = 12;                                 // The number of columns on the board (how wide it will be).
	public int _rows = 12;                                    // The number of rows on the board (how tall it will be).
	public GameObject[] _floorTiles;                           // An array of floor tile prefabs.
	public GameObject[] _townTiles;        
	public GameObject[] _waterTiles;                            
	public GameObject[] _outerWallTiles;  

	[Space]
	[Header("Board Components")]
	[SerializeField] private int _MaxTownCount = 4;	
	[SerializeField] private int _curTownCount = 0;	
	public GameObject _boardHolder;                           // GameObject that acts as a container for all other tiles.
	public List<GameObject> _tileList = new List<GameObject>();
	private TileType[][] _tiles;                               // A jagged array of tile types representing the board, like a grid.



	private void Start ()
	{
		_playerRef = GameObject.FindGameObjectWithTag ("Player");
		// Create the board holder.
		_boardHolder = new GameObject("BoardHolder");

		SetupTilesArray ();
	
		InstantiateTiles ();
		InstantiateOuterWalls ();
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
	void InstantiateTiles ()
	{		
		for (int x = 0; x < _tiles.Length; x++)
		{
			for (int z = 0; z < _tiles[x].Length; z++)
			{	
				
				//creates the floor for the whole grid
				InstantiateFromArray (_floorTiles, x, z);

				//here to say if we're on an outer edge tile, instantiate nothing on top of the existing tile instead a town or etc
				if (x != 0 || z != 0 || x != _tiles.Length-1 || z != _tiles.Length-1) 
				{
					// 1% chance to spawn water tile on top of normal
					if (Random.value <= .01f) 
					{
						InstantiateWater (_waterTiles, x, z);
					}

					// 3% chance to spawn town tile on top of normal
					if (Random.value <= .03f && _curTownCount != _MaxTownCount) 
					{		
						InstantiateTown (_townTiles, x, z);
					} 
				} 
			}
		}
	}

	//creates the border
	void InstantiateOuterWalls ()
	{
		// The outer walls are one unit left, right, up and down from the board.
		float leftEdgeX = -1f;
		float rightEdgeX = _columns + 0f;
		float bottomEdgeZ = -1f;
		float topEdgeZ = _rows + 0f;

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

		// Create an instance of the prefab from the random index of the array.
		if (xCoord == -1) 
		{

			//this checks the left wall, and says at the top and botttom wall piece, generate a corner tile
			if (zCoord == _tiles.Length || zCoord == -1) 
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
		else if (xCoord == _tiles.Length) 
		{
			//this checks the right wall, and says at the top and botttom wall piece, generate a corner tile
			if (zCoord == _tiles.Length || zCoord == -1) 
			{			
				if (zCoord == _tiles.Length) 
				{
					tileInstance = Instantiate (prefabs [7], position, Quaternion.identity) as GameObject;
				}	
				if (zCoord == -1) 
				{
					tileInstance = Instantiate (prefabs [5], position, Quaternion.identity) as GameObject;
				}
			} 
			else 
			{
				tileInstance = Instantiate (prefabs [3], position, Quaternion.identity) as GameObject;

			}
		}
		if (zCoord == -1) 
		{
			//this stops tiles from generating at the two ends of the front line of walls
			if (xCoord == _tiles.Length || xCoord == -1) 
			{		
			}  
			else 
			{
				tileInstance = Instantiate (prefabs [0], position, Quaternion.identity) as GameObject;
			}
		} 
		else if (zCoord == _tiles.Length) 
		{
			//this stops tiles from generating at the two ends of the back line of walls
			if (xCoord == _tiles.Length || xCoord == -1) 
			{	
			} 
			else 
			{
				tileInstance = Instantiate (prefabs [2], position, Quaternion.identity) as GameObject;
			}
		}

		tileInstance.name = "Wall _x-" + xCoord + " _z-" + zCoord;
		_tileList.Add (tileInstance);

		// Set the tile's parent to the board holder.
		tileInstance.transform.parent = _boardHolder.transform;
	}


	void InstantiateFromArray (GameObject[] prefabs, float xCoord, float zCoord)
	{
		if (xCoord == 0 && zCoord == 0) 
		{
			//	_playerRef.transform.position = new Vector3(0,1f,0f);
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
		tileInstance.transform.position = newPos;

		tileInstance.name = "Town_#" + _curTownCount;
		_curTownCount++;

		//adds the tile generated into a list for a reference to each of them
		_tileList.Add (tileInstance);
		// Set the tile's parent to the board holder.
		tileInstance.transform.parent = _boardHolder.transform;
		//tileInstance.transform.rotation = Quaternion.Euler(new Vector3(_isometricAngle.x, _isometricAngle.y, _isometricAngle.z));

	}

	private int waterNo = 0;
    public List<GameObject> listWaterTiles = new List<GameObject>();
    void InstantiateWater (GameObject[] prefabs, float xCoord, float zCoord)
	{

		//random index is for sprite to display - this wont use multiple water sprites, might not matter though
		//int randomIndex = Random.Range(0, prefabs.Length);
		Vector3 position = new Vector3(xCoord,.02f, zCoord);

		for (int it = 0; it < 4; it++)
        {
            //instantiate a tile because it's position is not being used, and therefore is unique
            //instantiate a new tile of water at newpos
            GameObject tileInstance = Instantiate(prefabs[0], position, Quaternion.identity) as GameObject;

            //add this position to a list to check against
            listWaterTiles.Add(tileInstance);

            //set the position with random adjustment in a direction
            position = position + new Vector3(Random.Range(-1,1),0,Random.Range(-1,1));

            //Do a check to make sure water doesnt overlap itself or other objects
            foreach (GameObject tile in listWaterTiles) 
			{
                //if the position i want to use as a new tile's position, exsists in this pool of water~
				if (position == tile.transform.position)
                {
                    Debug.Log("Remove additional tile at: " + position);
                    //remove the new game object that overlaps
                    Destroy(tileInstance);
                    waterNo--;
                } 
				else
                {
                    //name the tiles to keep track of them
                    tileInstance.name = "Water_#" + waterNo;
                    waterNo++;

                    //adds the tile to the universal tile list
                    _tileList.Add(tileInstance);
                    tileInstance.transform.parent = _boardHolder.transform;

                }
            }
        }

    }
}
