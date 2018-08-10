using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerSpawn : MonoBehaviour {

	public GameObject[] _villagers;
	[Space]
	public int _villagersToSpawn = 1;
	[Space]
	public bool _randomNumOfVillagers = false;

	[Space]
	public List<GameObject> _villagerCount = new List<GameObject>();

	[Space]
	public float _power = 0f;

	// Use this for initialization
	void Start () 
	{
		if(_randomNumOfVillagers == false)
		{
			for (int it = 0; it < _villagersToSpawn; it++)
			{			
				GameObject newVillager;
				newVillager = Instantiate(_villagers[Random.Range(0,_villagers.Length)], transform.position, Quaternion.identity, transform) as GameObject;

				Vector3 newpos = new Vector3(transform.position.x,0.07f,transform.position.z - .5f);
				//newpos.transform.position = transform.position;
				newVillager.transform.position = newpos;
				_villagerCount.Add(newVillager);
			}
		}
		else //spawn random number of villagers 
		{
			for (int it = 0; it < Random.Range(1,6); it++)
			{			
				GameObject newVillager;
				newVillager = Instantiate(_villagers[it], transform.position, Quaternion.identity, transform) as GameObject;

				Vector3 newpos = new Vector3(transform.position.x,0.3f,transform.position.z - .5f);
				//newpos.transform.position = transform.position;
				newVillager.transform.position = newpos;
				_villagerCount.Add(newVillager);
			}
			
		}
	}

	public void AddPower(float addPower)
	{
		_power = _power + addPower;
		//after all villagers are risen, over time add power here or to a global controller or the necromancer.	
	}
	
}
