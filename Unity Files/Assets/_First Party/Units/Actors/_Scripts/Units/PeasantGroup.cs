/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Erix Cox
   Contributors:	Hayden Reeve
   File:			PeasantGroup.cs
   Version:			0.2.1
   Description: 	Inheritance structure that allows peasants to move freely around the village's range.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PeasantGroup : UnitGroup {

    Vector3 _patrolDir;
    float _timer;

	[Header("Peasant Settings")]
	[Space] [SerializeField] float _moveRange = 5f; // How much a unit can move (UP TO) per timer loop.
	[SerializeField] float _moveOften = 3f;

    protected override void BehaviourLoopPassive()
    {
        _timer += Time.deltaTime;

		if (_timer >= _moveOften)
			VillagerMove();

		if (_forcePositionUpdate)
			VillagerMove();

    }

	protected void VillagerMove() 
	{

		for (int i = 0; i < _everyUnit.Length; i++) {

			//chooses a random patrol direction out of 4 directions
			float movementX = Random.Range(-_moveRange, _moveRange);
			float movementZ = Random.Range(-_moveRange, _moveRange);

			MoveUnit(i, _anchor.TransformPoint(movementX, 0, movementZ));

		}

		// Reset activation variables.
		_timer = 0f;
		_forcePositionUpdate = false;

	}

	// Method specifically for killing summonable-entities. We pass information to make turning the undead easier to deal with.
	public Vector3[] MurderedVillagerLocation(int summonCost, float accuracyModifier) {

		int arrayLength = Mathf.Max(Mathf.CeilToInt(summonCost * accuracyModifier),0);

		var someList = _everyUnit.ToList();

		List<Vector3> vectorList = new List<Vector3>();

		for (int i = Mathf.Min(arrayLength,_everyUnit.Length); i > 0; i--) {
			vectorList.Add(_everyUnit[i].transform.position);
		}

		MinusUnit(Mathf.Min(arrayLength, _everyUnit.Length));
		return vectorList.ToArray();
		
	}

	//_unitStyle;           // The type of unit that we contain.
	//_unitTemplate;        // The basic unit template.
	//_everyUnit;           // The units within the group.


	//  code to get units to move 
	//set health
	//BehaviourLoopPassive
	//NeedToUpdatePosition
	//PositionVariance
	//MoveUnit

}