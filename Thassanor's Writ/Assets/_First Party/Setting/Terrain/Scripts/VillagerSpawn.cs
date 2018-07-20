using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerSpawn : MonoBehaviour {

	public GameObject[] _villagers;
	public List<GameObject> _villagerCount = new List<GameObject>();

	public float _power = 0f;

	// Use this for initialization
	void Start () 
	{
		for (int it = 0; it < _villagers.Length; it++)
		{			
			GameObject newVillager;
			newVillager = Instantiate(_villagers[it], transform.position, Quaternion.identity, transform) as GameObject;

			Vector3 newpos = new Vector3(transform.position.x,0.3f,transform.position.z - .5f);
			//newpos.transform.position = transform.position;
			newVillager.transform.position = newpos;
			_villagerCount.Add(newVillager);
		}
	}

	public void AddPower(float addPower)
	{
		_power = _power + addPower;
		//after all villagers are risen, over time add power here or to a global controller or the necromancer.	
	}
	
}
