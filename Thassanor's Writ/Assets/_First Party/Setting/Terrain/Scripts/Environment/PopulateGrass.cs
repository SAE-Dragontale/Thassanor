/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			PopulateGrass.cs
   Version:			0.1.1
   Description: 	
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

public class PopulateGrass : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private MeshRenderer _mr;   // We need our Mesh Renderer to determine the area of the space we have to work with when spawning grass.

	[SerializeField] private Sprite[] _sprites;     // The various grass sprites that we can choose between!

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[SerializeField] private int _itAmountOfGrass;	// How many of said prefabs do we want to spawn?

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	private void Awake() {

		_mr = GetComponentInParent<MeshRenderer>();

	}

	// Called before class calls or functions.
	private void Start() {

		// We're taking the Mesh Renderer's "dimensions" in a Vector 3 so we can position our stuff evenly across it.
		Vector3 v3Size = _mr.bounds.size;

		// We want to position our grass across the Mesh Renderer.
		for (int it = _itAmountOfGrass; _itAmountOfGrass > 0; _itAmountOfGrass--) {

			// Find our our position within the bounds of our Mesh Renderer (surface) and instantiate some grass!
			Vector3 v3Place = _mr.bounds.center + new Vector3(Random.Range(-v3Size.x/2,v3Size.x/2), 0f, Random.Range(-v3Size.z / 2, v3Size.z / 2));

			// Instantiate and set the parent of the GameObject.
			GameObject grass = new GameObject("Grass");
			grass.transform.parent = transform;
			grass.transform.position = v3Place;

			// Randomise the sprite that we're giving the blade of grass!
			grass.AddComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, _sprites.Length - 1)];

		}

		// We no longer require this script. Delete the evidence, Mr Burns.
		Destroy(this);

	}

	/* ----------------------------------------------------------------------------- */
	
}
