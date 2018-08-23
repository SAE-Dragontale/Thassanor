/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			SplitChildrenToZ.cs
   Version:			0.1.0
   Description: 	A really simple placeholder script to manipulate sprite-children-components into a faux 3D effect.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

public class SplitChildrenToZ : MonoBehaviour {

	[SerializeField] private float _flSplitY = 0.05f;
	[SerializeField] private float _flSplitZ = 0.1f;

	// Called before class calls or functions.
	private void Start () {

		ShittySplitter();

	}

	[InspectButton("Split")]
	public void ShittySplitter() {

		var children = GetComponentsInChildren<Transform>();

		for (int it = 0; it < children.Length; it++) {
			children[it].position = transform.TransformPoint(0f, it * _flSplitY, it * _flSplitZ *-1);
		}

	}

	/* ----------------------------------------------------------------------------- */
	
}