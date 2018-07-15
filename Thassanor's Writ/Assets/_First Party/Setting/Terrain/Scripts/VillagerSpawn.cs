using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerSpawn : MonoBehaviour {

	public GameObject[] _villagers;
	public List<GameObject> _villagerCount = new List<GameObject>();

	// Use this for initialization
	void Start () 
	{
		for (int it = 0; it < _villagers.Length; it++)
		{			
			GameObject newVillager;
			newVillager = Instantiate(_villagers[it], transform.position, Quaternion.identity) as GameObject;

			Vector3 newpos = new Vector3(transform.position.x,0.3f,transform.position.z - .5f);
			//newpos.transform.position = transform.position;
			newVillager.transform.position = newpos;
			_villagerCount.Add(newVillager);
		}
	}
	
}
