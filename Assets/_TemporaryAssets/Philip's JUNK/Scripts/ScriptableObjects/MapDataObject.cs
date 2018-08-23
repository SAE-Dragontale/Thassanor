/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Philip Ong
   File:			MapDataObject.cs
   Version:			0.0.1
   Description: 	MapData scriptable object containig 
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New MapData Object", menuName = "MapData")]
public class MapDataObject : ScriptableObject
{
    public int typingDifficulty;
    public int _itSeed;
    public int _columns;
    public int _rows;
    public int _waterSize;
    public int _townSpread;
    public int _maxTownCount;
}
