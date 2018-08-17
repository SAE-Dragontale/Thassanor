using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PeasantGroup : UnitGroup {

    Vector3 _patrolDir;

    protected override void BehaviourLoopPassive()
    {
        //call co rout u noob
        for (int i = 0; i < _everyUnit.Length; i++)
        {
            //chooses a random patrol direction out of 4 directions
            float patrolDirX = Random.Range(-1f, 1f);
            float patrolDirZ = Random.Range(-1f, 1f);

            _patrolDir = new Vector3(patrolDirX, 0, patrolDirZ);
            MoveUnit(i, _patrolDir);

            //Debug.Log("Navmesh clamp");
            //NavMeshHit closestHit;
            //if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas))
            //    _everyUnit[i].gameObject.transform.position = closestHit.position;

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
