using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VillagerSpawn : MonoBehaviour {

    [SerializeField] float _spawnRate;

	[SerializeField] GameObject _peasant;
	[SerializeField] GameObject _peasantGroup;
	[SerializeField] private UnitGroup _thisUnitGroup;
	[SerializeField] private UnitStyle[] _everyUnitStyle;


    private void Start()
    {
		_thisUnitGroup = _peasantGroup.GetComponent<UnitGroup>();

		_thisUnitGroup._UnitStyle = _everyUnitStyle[Random.Range(0, _everyUnitStyle.Length - 1)];
		
		StartCoroutine(SpawnOverTime());
    }

    
	private IEnumerator SpawnOverTime() {

		
		while (true)
		{
			yield return new WaitForSeconds(_spawnRate);
           // _thisUnitGroup.AddUnit(1);
		   Instantiate(_peasant,transform.position,Quaternion.identity,_peasantGroup.transform);
		}
	}

	
}
