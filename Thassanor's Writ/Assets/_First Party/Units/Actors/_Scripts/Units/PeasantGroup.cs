/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Erix Cox
   Contributors:	Hayden Reeve
   File:			PeasantGroup.cs
   Version:			0.1.0
   Description: 	Inheritance structure that allows peasants to move freely around the village's range.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

public class PeasantGroup : UnitGroup {

    Vector3 _patrolDir;
    float _timer;

	[SerializeField] float _movementPerTick = 1f;	// How much a unit can move (UP TO) per timer loop.

    protected override void BehaviourLoopPassive()
    {
        _timer += Time.deltaTime;

        if (_timer >= 3f)
        {
            for (int i = 0; i < _everyUnit.Length; i++)
            {
                //chooses a random patrol direction out of 4 directions
                float patrolDirX = Random.Range(-1f, 1f);
                float patrolDirZ = Random.Range(-1f, 1f);

                _patrolDir = _anchor.TransformPoint(_movementPerTick, 0, _movementPerTick).normalized;

                MoveUnit(i, _patrolDir);

            }
            _timer = 0f;
        }
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