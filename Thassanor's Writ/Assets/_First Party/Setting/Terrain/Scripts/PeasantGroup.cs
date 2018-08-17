using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantGroup : UnitGroup {

    bool _oneShotActionActive = false;
    Vector3 _patrolDir;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    protected override void BehaviourLoopPassive()
    {
        //sets this to only occur once in patrol state
        if (_oneShotActionActive == true)
        {
            for (int i = 0; i < _everyUnit.Length; i++)
            {
                //chooses a random patrol direction out of 4 directions
                float patrolDirX = Random.Range(-1f, 1f);
                float patrolDirZ = Random.Range(-1f, 1f);

                _patrolDir = new Vector3(patrolDirX, 0, patrolDirZ);
                MoveUnit(i, _patrolDir);

            }

            _oneShotActionActive = false;
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
