/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			UnitSpawner.cs
   Version:			0.0.0
   Description: 	Simply spawns units to a UnitGroup over time.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using UnityEngine;

public class UnitSpawner : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[SerializeField] private UnitGroup _spawnToThis;
	[SerializeField] private UnitStyle[] _everyUnitStyle;

	[SerializeField] private float _incrementInterval;
	[SerializeField] private float _healthPerIncrement;
	[SerializeField] private float _startingHealth;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void Awake() => _spawnToThis._Anchor = transform;
	private void Start() {

		// Define our UnitGroup Style
		_spawnToThis._UnitStyle = _everyUnitStyle[Random.Range(0, _everyUnitStyle.Length - 1)];

		// Define any Units that are pre-spawned.
		_spawnToThis.ChangeHealth(_startingHealth);

		// Begin spawning over time.
		StartCoroutine("SpawnOverTime");

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Functions
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private void SpawnUnits(float health) => _spawnToThis.ChangeHealth(health);

	private IEnumerator SpawnOverTime() {

		yield return new WaitForSeconds(_incrementInterval);
		SpawnUnits(_healthPerIncrement);

	}

	/* ----------------------------------------------------------------------------- */
	
}
