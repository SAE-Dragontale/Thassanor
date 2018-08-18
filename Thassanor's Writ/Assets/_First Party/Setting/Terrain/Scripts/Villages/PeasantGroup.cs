using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PeasantGroup : UnitGroup {

    Vector3 _patrolDir;
    float _timer;



    protected override void BehaviourLoopPassive()
    {
        _timer = _timer + Time.deltaTime;
        if (_timer >= 3f)
        {
            for (int i = 0; i < _everyUnit.Length; i++)
            {
                //chooses a random patrol direction out of 4 directions
                float patrolDirX = Random.Range(-1f, 1f);
                float patrolDirZ = Random.Range(-1f, 1f);

                _patrolDir = new Vector3(patrolDirX, 0, patrolDirZ);
                Debug.Log("Move Unit " + i + " to" + _patrolDir);
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
