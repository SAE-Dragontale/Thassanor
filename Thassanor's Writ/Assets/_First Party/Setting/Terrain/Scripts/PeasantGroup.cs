using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantGroup : UnitGroup {

    public float _spawnTimer;
    bool _oneShotActionActive = false;
    Vector3 _patrolDir;

    public int _villagersToSpawn = 1;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        AddUnit(_villagersToSpawn);
    }

    protected override void BehaviourLoopPassive()
    {
        //sets this to only occur once in patrol state
        if (_oneShotActionActive == true)
        {
            _oneShotActionActive = false;

            //chooses a random patrol direction out of 4 directions
            float patrolDirX = Random.Range(-1f, 1f);
            float patrolDirZ = Random.Range(-1f, 1f);

            _patrolDir = new Vector3(patrolDirX, 0, patrolDirZ);
            MoveUnit(0, _patrolDir);
        }

        _spawnTimer = _spawnTimer + Time.deltaTime;
        if (_spawnTimer >= 4f)
        {
            //reset action timer, set one shot bool to true because idle has 'play once' elements, and set back to idle 
            
            AddUnit(_villagersToSpawn);
            _spawnTimer = 0f;
            _oneShotActionActive = true;
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
