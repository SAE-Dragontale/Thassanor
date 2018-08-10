/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			PopulateGrass.cs
   Version:			0.0.0
   Description: 	
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections.Generic;
using UnityEngine;

public class PopulateGrass : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private MeshRenderer _mr;	// We need our Mesh Renderer to determine the area of the space we have to work with when spawning grass.

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[SerializeField] private GameObject[] _agoGrass;	// The various grass prefabs that we can choose to spawn.
	[SerializeField] private int _itAmountOfGrass;		// How many of said prefabs do we want to spawn?


	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	private void Awake() {

		_mr = GetComponent<MeshRenderer>();

	}

	// Called before class calls or functions.
	private void Start() {

		Vector3 v3Size = _mr.bounds.extents;

		for (int it = _itAmountOfGrass; _itAmountOfGrass > 0; _itAmountOfGrass--) {

			// Find our our position within the bounds of our Mesh Renderer (surface) and instantiate some grass!
			Vector3 v3Place = _mr.bounds.center + new Vector3(Random.Range(-v3Size.x/2,v3Size.x/2), Random.Range(-v3Size.y / 2, v3Size.y / 2), 0f);
			Instantiate(_agoGrass[Random.Range(0, _agoGrass.Length - 1)], v3Place, Quaternion.identity);

		}

		// We no longer require this script. Delete the evidence, Mr Burns.
		Destroy(this);

	}

	/* ----------------------------------------------------------------------------- */
	
}
