using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerSpawn : MonoBehaviour {

    float _actionTimer;
    public float _spawnTimer;

    public Unit[] _peasants;
    [Space]

    public PeasantGroup _peasantGroup;


    private void Start()
    {
    }

    private void Update()
    {       

        _actionTimer = _actionTimer + Time.deltaTime;
        if (_actionTimer >= _spawnTimer)
        {
            //reset action timer, set one shot bool to true because idle has 'play once' elements, and set back to idle 

            _peasantGroup.AddUnit(1);
            
            _actionTimer = 0f;
        }
    }

	
}
