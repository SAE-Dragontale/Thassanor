using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderGeneration : MonoBehaviour {

	public BoardGeneration _boardGeneratorRef;
    [HideInInspector] public int _columnLength;
   [HideInInspector] public int _rowLength;
	public GameObject[] _outerWallTiles;  
	public List<GameObject> _wallList = new List<GameObject>();

    private Transform _borderFolder;

    private void Awake() 
	{
		_boardGeneratorRef = gameObject.GetComponent<BoardGeneration>();
	}

    private void Start()
    {
        _borderFolder = new GameObject("Borders").transform;
        _borderFolder.parent = transform;

        _rowLength = _boardGeneratorRef._rows * 10;
        _columnLength = _boardGeneratorRef._columns * 10;

    }


    //creates the border
    public void InstantiateOuterWalls ()
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
		float leftEdgeX = -5f;
		float rightEdgeX = _columnLength;
		float bottomEdgeZ = -5f;
		float topEdgeZ = _rowLength;

		// Instantiate both vertical walls (one on each side).
		InstantiateVerticalOuterWall (leftEdgeX, bottomEdgeZ, topEdgeZ);
		InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeZ, topEdgeZ);

		// Instantiate both horizontal walls, these are one in left and right from the outer walls.
		InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, bottomEdgeZ);
		InstantiateHorizontalOuterWall(leftEdgeX + 1f, rightEdgeX - 1f, topEdgeZ);
	}

	//creates the border on the left and right hand sides
	public void InstantiateVerticalOuterWall (float xCoord, float startingZ, float endingZ)
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
	public void InstantiateHorizontalOuterWall (float startingX, float endingX, float zCoord)
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


	public void InstantiateWallsFromArray (GameObject[] prefabs, float xCoord, float zCoord)
	{
		// The position to be instantiated at is based on the coordinates.
		Vector3 position = new Vector3(xCoord*10, 0.01f , zCoord*10);

		//checks left wall
		if (xCoord == -5) 
		{
			//this checks the left wall, and says at the top and botttom wall piece, generate a corner tile
			if (zCoord == _rowLength *10 || zCoord == -5) 
			{			
				if (zCoord == _columnLength * 10- 1) 
				{
					_boardGeneratorRef._tileInstance = Instantiate (prefabs [6], position, Quaternion.identity) as GameObject;
				}	
				if (zCoord == -5) 
				{
					_boardGeneratorRef._tileInstance = Instantiate (prefabs [4], position, Quaternion.identity) as GameObject;
				}
			} 
			else 
			{
				_boardGeneratorRef._tileInstance = Instantiate (prefabs [1], position, Quaternion.identity) as GameObject;
			}
		} 
		//checks right wall
		else if (xCoord == _columnLength*10) 
		{
			//this checks the right wall, and says at the top and botttom wall piece, generate a corner tile
			if (zCoord == _rowLength*10 || zCoord == -5) 
			{			
				if (zCoord == _columnLength*10 -1) 
				{
					//create corner tile at length
					_boardGeneratorRef._tileInstance = Instantiate (prefabs [7], position, Quaternion.identity) as GameObject;
				}	
				if (zCoord == -5) 
				{
					//create corner tile at start
					_boardGeneratorRef._tileInstance = Instantiate (prefabs [5], position, Quaternion.identity) as GameObject;
				}
			} 
			else 
			{
				_boardGeneratorRef._tileInstance = Instantiate (prefabs [3], position, Quaternion.identity) as GameObject;

			}
		}
		
		//checks front wall
		if (zCoord == -5) 
		{
			//this stops tiles from generating at the corners of the front line of walls
			if (xCoord == _columnLength*10 || xCoord == -5) 
			{		
			}  
			else 
			{
				_boardGeneratorRef._tileInstance = Instantiate (prefabs [0], position, Quaternion.identity) as GameObject;
			}
		} 
		//checks back wall with the second element in the jagged array
		else if (zCoord == _rowLength*10) 
		{
			//this stops tiles from generating at the corners of the back line of walls
			if (xCoord == _columnLength*10 || xCoord == -5) 
			{			
				if (xCoord == _columnLength*10) 
				{
					//create corner tile at length
					_boardGeneratorRef._tileInstance = Instantiate (prefabs [7], position, Quaternion.identity) as GameObject;
				}	
				if (xCoord == -5) 
				{
					//create corner tile at start
					_boardGeneratorRef._tileInstance = Instantiate (prefabs [6], position, Quaternion.identity) as GameObject;
				}
			} 
			else 
			{
				_boardGeneratorRef._tileInstance = Instantiate (prefabs [2], position, Quaternion.identity) as GameObject;

			}
		}
		
		_boardGeneratorRef._tileInstance.name = "Wall _x-" + xCoord + " _z-" + zCoord;
		_wallList.Add (_boardGeneratorRef._tileInstance);

		// Set the tile's parent to the board holder.
		_boardGeneratorRef._tileInstance.transform.parent = _borderFolder;
	}

}
