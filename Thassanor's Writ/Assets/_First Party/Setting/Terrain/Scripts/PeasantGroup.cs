using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantGroup : UnitGroup {
    
	public int _villagersToSpawn = 1;

	[Space]
	private List<GameObject> _villagerCount = new List<GameObject>();
    
	// Use this for initialization
	void Start () 
	{
		for (int it = 0; it < _villagersToSpawn; it++)
		{			
			GameObject newVillager;
			newVillager = Instantiate(_unitTemplate, transform.position, Quaternion.identity, transform) as GameObject;

			Vector3 newpos = new Vector3(transform.position.x,0.07f,transform.position.z - .5f);
			//newpos.transform.position = transform.position;
			newVillager.transform.position = newpos;
			_villagerCount.Add(newVillager);
		}
		
	}

    //_unitStyle;           // The type of unit that we contain.
    //_unitTemplate;        // The basic unit template.
    //_everyUnit;           // The units within the group.

    //code to get units to move 
    //set health
    //BehaviourLoopPassive
    //NeedToUpdatePosition
    //PositionVariance
    //MoveUnit


}
