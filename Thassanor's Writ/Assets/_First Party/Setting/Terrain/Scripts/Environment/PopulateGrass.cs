/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			PopulateGrass.cs
   Version:			0.2.0
   Description: 	
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

public class PopulateGrass : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	private MeshRenderer _mr;	// We need our Mesh Renderer to determine the area of the space we have to work with when spawning grass.

	[SerializeField] private Sprite[] _grassSprites;    // The various grass sprites that we can choose between!

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Range(0,1000)]
	[SerializeField] private int amountOfGrass; // How many of said prefabs do we want to spawn?

	private Vector3 _boundary;	// The boundary of our MeshRenderer. This is where we are allowed to spawn.

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Instantation
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	// Called before Start().
	private void Awake() {

		_mr = GetComponent<MeshRenderer>();

	}

	// Called before class calls or functions.
	private void Start() {

		// We're taking the Mesh Renderer's "dimensions" in a Vector 3 so we can position our stuff evenly across it.
		_boundary = _mr.bounds.extents;
		
		// Spawn random visual-only elements that belong to this object.
		SpawnGrass();

		// We no longer require this script. Delete the evidence, Mr Burns.
		Destroy(this);

	}

	// Find our our position within the bounds of our Mesh Renderer.
	private Vector3 PlaceWithinBoundary() => _mr.bounds.center + new Vector3(Random.Range(-_boundary.x, _boundary.x), 0f, Random.Range(-_boundary.z, _boundary.z));

	private void SpawnGrass() {

		for (int i = amountOfGrass; i > 0; i--) {

			// Instantiate and set the parent of the GameObject.
			GameObject grass = new GameObject("Grass");
			grass.transform.parent = transform;
			grass.transform.position = PlaceWithinBoundary();

			// Randomise the sprite that we're giving the blade of grass!
			grass.AddComponent<SpriteRenderer>().sprite = _grassSprites[Random.Range(0, _grassSprites.Length - 1)];

		}

	}

	/* ----------------------------------------------------------------------------- */
	
}
