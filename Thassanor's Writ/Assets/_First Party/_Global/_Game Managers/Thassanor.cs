/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Thassanor.cs
   Version:			0.0.0
   Description: 	
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

namespace Dragontale {

	public class Thassanor : MonoBehaviour {

		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Instantation
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static void RuntimeInit() {

			// This script will run at the start of every scene. 
			// If we have already run in a different scene, we don't want to run again.
			if (FindObjectOfType<Thassanor>() != null)
				return;

			// Create an empty gameobject to hold this and any other dependencies.
			GameObject thassanor = new GameObject { name = "[Thassanor]" };

			// Add our script dependencies here.
			thassanor.AddComponent<Thassanor>();

			DontDestroyOnLoad(thassanor);

		}

		/* ----------------------------------------------------------------------------- */

	}

}
