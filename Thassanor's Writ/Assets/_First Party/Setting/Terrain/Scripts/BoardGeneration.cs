// Script: BoardGeneration.cs
// Author & Contributors: Eric Cox
// Purpose: The creation and design details of the maps for the game. Water/Towns/Props/Size. etc.  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.Networking;



public class BoardGeneration : MonoBehaviour {
	
	// The type of tile that will be laid in a specific position.
	public enum TileType
	{
		Wall, Floor,
	}


    [Header("Grid Components")]	
    public int _itSeed;                                         //grid seed for generation
    OpenSimplexNoise _simplexNoise;								//reference to simplex noise script to get grid style
    float _fltPerlinValue;

	[Space]

    [Tooltip("Width.")]
    public int _columns = 12;                                   // The number of columns on the board (how wide it will be).
    [Tooltip("Height. This number also cannot be odd.")]
	public int _rows = 12;                                      // The number of rows on the board (how tall it will be).

	[Space]

    readonly float _waterAmount = -0.3f;
    [Range(4, 12)]
    public int _waterSize;

	[Space]

    [Range(2, 12)]
    public int _townSpread;
	public int _maxTownCount = 4;	
	[SerializeField] private int _curTownCount = 0;	

	[Space]
	
	public GameObject[] _floorTiles;                            // An array of floor tile prefabs.
	public GameObject[] _townTiles;        
	public GameObject[] _waterTiles;       

/*
	[Space]
	[Header("UI Components")]
	public Text _txtBoardSize;
	public Text _txtWaterCount;
	public Text _txtTownCount;
*/

	[Space]
	[Header("Board Components")]
	public GameObject _p1Spawn;       
	public GameObject _p2Spawn;       
	[Space]

    public GameObject _navMeshTile;
	
	[Space]

	public List<GameObject> _tileList = new List<GameObject>();
	public List<GameObject> _propList = new List<GameObject>();
    public List<GameObject> _waterList = new List<GameObject>();
	public TileType[][] _tiles;                               // A jagged array of tile types representing the board, like a grid.

	[HideInInspector] public MapData _mapData;
	[HideInInspector] public BorderGeneration _borderGenRef;
	[HideInInspector] public GameObject _tileInstance;

    [Space]
    [Header("Neatness")]
    private Transform _tileFolder;

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//

    //public SingletonPass _singletonRef;
    void Awake()
	{
        //read from singleton
        if (_mapData = GameObject.FindObjectOfType<MapData>())
        {
            _itSeed = _mapData._itSeed;
            _columns = _mapData._columns;
            _rows = _mapData._rows;
            _waterSize = _mapData._waterSize;
            _townSpread = _mapData._townSpread;
            _maxTownCount = _mapData._maxTownCount;

        }

		
	}

	private void Start ()
	{

        _tileFolder = new GameObject("Tiles").transform;
        _tileFolder.parent = transform;

        _borderGenRef = gameObject.GetComponent<BorderGeneration>();

        _simplexNoise = new OpenSimplexNoise(_itSeed);


        //_p1Spawn = GameObject.Find("P1 Spawner");
        //_p2Spawn = GameObject.Find("P2 Spawner");


               
		SetupTilesArray ();
	
		InstantiateTiles ();
		_borderGenRef.StartBorderGen(); 			
		
		StartCoroutine(DelayedStart());

       // Debug.Log("Rows_" + _tiles[0].Length);
       // Debug.Log("Columns" + _tiles.Length);
    }

	public IEnumerator DelayedStart()
    {
        	
		//generates the nav surface for the board
		_navMeshTile.GetComponent<NavMeshSurface>().BuildNavMesh();

        yield return new WaitForSeconds(1f);
        for (int i = 0; i < _tileList.Count; i++)
        {
            Destroy(_tileList[i].GetComponent<MeshCollider>());
        }


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
        //keeps this a constant size compared to the grid/map size, this is the number of steps in tiles it takes until another town can spawn

        //avoids the town spread being so high on small grid sizes, that towns just dont spawn
        if (_townSpread > _columns * 2 || _townSpread > _rows * 2)
        {
            _townSpread = Mathf.Max(_columns, _rows);
        }

        //stops there from being an over abundance of towns 
        if (_maxTownCount > _columns / 2 || _maxTownCount > _rows / 2)
        {
            _maxTownCount = Mathf.Max(_columns, _rows) / 2;
        }

		if(_townSpread % 2 == 0)
		{_townSpread -= 1;}
				
		for (int z = 0; z < _tiles[0].Length; z++)
		{	
			//random half
			if(z < _rows/2)
			{
				for (int x = 0; x < _tiles.Length; x++)
				{ 
					//THIS WILL RETURN -1 TO 1
					_fltPerlinValue = (float)_simplexNoise.Evaluate((double)(x * .5f), (double)(z * 0.5f));
				

					//here to say if we're not on an outer edge tile & instantiate on top of the existing tile
					if (x != 0 || z != 0 || x != _tiles.Length-1 || z != _tiles[0].Length-1) 
					{
                        
						//the actual spawning of the grass tiles, this is here too so theres a grass tile under any water or town
						InstantiateFromArray(_floorTiles,x,z);	
						mirrorTileList.Add(floorTileInstance);
						mirrorListCount++;
                        
						//perlin value for water
						//if (_fltPerlinValue < _waterAmount) 
						//{
						//	InstantiateWater (_waterTiles, x, z);									
						//}
						
						//perlin value for towns to spawn
						if (_fltPerlinValue < .3f && _fltPerlinValue > -.3f) 
						{
							if (_curTownCount != _maxTownCount && townSpreadCur == _townSpread)
							{
								InstantiateTown(_townTiles, x, z);
								townSpreadCur = 0;
							}
						} 						
					}
					else
					{
						//the actual spawning of the grass tiles
						InstantiateFromArray(_floorTiles,x,z);	
						mirrorTileList.Add(floorTileInstance);
						mirrorListCount++;	
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
				//for (int x = _tiles.Length - 1; x >= 0; x--) //this results in a vertical flip, so x town will be to the right of one player, and to the left for the other
				for (int x = 0; x < _tiles.Length; x++)//  this results in a vertical and horizontal flip, so x town will be to the right of both players
				{
					Vector3 position = new Vector3(x * 10, 0f, z * 10);    

					floorTileInstance = Instantiate(mirrorTileList[mirrorListCount - 1], position, Quaternion.identity, _tileFolder) as GameObject;
					floorTileInstance.name = mirrorTileList[mirrorListCount - 1].name;
					_tileList.Add (floorTileInstance);

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
	//sets the player positions appropriately to their gbo's, when the center bottom tile is creating. 
		if (xCoord == (_columns/2) && zCoord == 0) 
		{
			_p1Spawn.transform.position = new Vector3(xCoord * 10,.6f,zCoord);
			_p2Spawn.transform.position = new Vector3(xCoord * 10,.6f,(_tiles[0].Length * 10) - 10);

		}

		//Mulitpplies the x and z coords to match the distance required to seperate grid tiles
        xCoord = xCoord * 10f;
        zCoord = zCoord * 10f;

		Vector3 position = new Vector3(xCoord, 0f, zCoord);    
		
		int index = 0; //if the number of possible grass tiles is 1, then set index to 0 so it wont try spawn soemthing that doesnt exist
		if(prefabs.Length > 1)
		{
            //if there's more than 1 possible grass tile to spawn from, other tiles hhave 25% chance to spawn ...
            //... it chooses one tile at random from the others and sets that as the index
            if (_fltPerlinValue > .57f && _fltPerlinValue < .6f)
            {
                index = 2;

            }
            else if (_fltPerlinValue > 0f && _fltPerlinValue < .5f)
            {
                index = 1;
            }
            else
            {
                index = 0;
            }
			
		}			

		floorTileInstance = Instantiate(prefabs[index], position, Quaternion.identity, _tileFolder) as GameObject;
        floorTileInstance.AddComponent<MeshCollider>();
		floorTileInstance.name = "Tile X: " + (xCoord /10) + " | Z: " + (zCoord/10);


        _tileList.Add(floorTileInstance);

	}

 //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------//
	void InstantiateTown (GameObject[] prefabs, float xCoord, float zCoord)
	{
        xCoord = xCoord * 10;
        zCoord = zCoord * 10;

		// Create a random index for the instantiated tile.
		int randomIndex = Random.Range(0, prefabs.Length);

		Vector3 position = new Vector3(xCoord, 0, zCoord);
		// Create an instance of the prefab from the random index of the array.
		GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.identity) as GameObject;

		Vector3 newPos = tileInstance.transform.position;
		newPos = newPos + new Vector3(0,0.3f,0);
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
		Vector3 position = new Vector3(xCoord * 10,0.05f, zCoord * 10);
		
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
					Destroy(tileInstance);
                } 
				else
                {
					//if water is out of bounds, destroy
					if (tileInstance.transform.position.x < -5 || tileInstance.transform.position.z < -5 || tileInstance.transform.position.x > (_tiles.Length * 10)-1 || tileInstance.transform.position.z > (_tiles[0].Length * 10)-1) 
					{						
						Destroy(tileInstance);
					}
                }
            }
			waterNo++;
        }
			
    }

}
