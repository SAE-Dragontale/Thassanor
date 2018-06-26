using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderGeneration : MonoBehaviour {

	public BoardGeneration _boardGeneratorRef;                     
	public GameObject[] _outerWallTiles;  
	public List<GameObject> _wallList = new List<GameObject>();

	private void Awake() 
	{
		_boardGeneratorRef = gameObject.GetComponent<BoardGeneration>();
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
		float leftEdgeX = -1f;
		float rightEdgeX = _boardGeneratorRef._columns;
		float bottomEdgeZ = -1f;
		float topEdgeZ = _boardGeneratorRef._rows;

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
		Vector3 position = new Vector3(xCoord, 0f , zCoord);

		//checks left wall
		if (xCoord == -1) 
		{
			//this checks the left wall, and says at the top and botttom wall piece, generate a corner tile
			if (zCoord == _boardGeneratorRef._tiles[0].Length || zCoord == -1) 
			{			
				if (zCoord == _boardGeneratorRef._tiles.Length) 
				{
					_boardGeneratorRef._tileInstance = Instantiate (prefabs [6], position, Quaternion.identity) as GameObject;
				}	
				if (zCoord == -1) 
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
		else if (xCoord == _boardGeneratorRef._tiles.Length) 
		{
			//this checks the right wall, and says at the top and botttom wall piece, generate a corner tile
			if (zCoord == _boardGeneratorRef._tiles[0].Length || zCoord == -1) 
			{			
				if (zCoord == _boardGeneratorRef._tiles.Length) 
				{
					//create corner tile at length
					_boardGeneratorRef._tileInstance = Instantiate (prefabs [7], position, Quaternion.identity) as GameObject;
				}	
				if (zCoord == -1) 
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
		if (zCoord == -1) 
		{
			//this stops tiles from generating at the corners of the front line of walls
			if (xCoord == _boardGeneratorRef._tiles.Length || xCoord == -1) 
			{		
			}  
			else 
			{
				_boardGeneratorRef._tileInstance = Instantiate (prefabs [0], position, Quaternion.identity) as GameObject;
			}
		} 
		//checks back wall with the second element in the jagged array
		else if (zCoord == _boardGeneratorRef._tiles[0].Length) 
		{
			//this stops tiles from generating at the corners of the back line of walls
			if (xCoord == _boardGeneratorRef._tiles.Length || xCoord == -1) 
			{			
				if (xCoord == _boardGeneratorRef._tiles.Length) 
				{
					//create corner tile at length
					_boardGeneratorRef._tileInstance = Instantiate (prefabs [7], position, Quaternion.identity) as GameObject;
				}	
				if (xCoord == -1) 
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
		_boardGeneratorRef._tileInstance.transform.parent = _boardGeneratorRef._boardHolder.transform;
	}

}
