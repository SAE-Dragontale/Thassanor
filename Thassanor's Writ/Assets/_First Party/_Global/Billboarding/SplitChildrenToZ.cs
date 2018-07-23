/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			SplitChildrenToZ.cs
   Version:			0.0.0
   Description: 	A really simple placeholder script to manipulate sprite-children-components into a faux 3D effect.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

public class SplitChildrenToZ : MonoBehaviour {

	// Called before class calls or functions.
	private void Start () {
		
		float i = 0f;
		foreach (Transform tr in GetComponentInChildren<Transform>()) {
			tr.position = new Vector3(tr.position.x, tr.position.y + (i*0.05f), tr.position.z - (i*0.1f));
			i++;
		}

	}

	/* ----------------------------------------------------------------------------- */
	
}
