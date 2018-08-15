/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Thassanor.cs
   Version:			0.1.0
   Description: 	
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;
using ByteSprite.Development;

namespace Dragontale {

	public class Thassanor : MonoBehaviour {

		/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
			Instantation
		// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

		// This script will run at the start of every scene. 

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		static void RuntimeInit() {

			// If we have already run in a different scene, we don't want to run again.
			if (FindObjectOfType<Thassanor>() != null)
				return;

			/* ----------------------------------------------------------------------------- */
			// Critical Dependency Object
			GameObject thassanor = new GameObject { name = "[Thassanor]" };
			DontDestroyOnLoad(thassanor);

			thassanor.AddComponent<Thassanor>();
			thassanor.AddComponent<DeveloperConsole>();

			/* ----------------------------------------------------------------------------- */
			// Audio Manager Object
			GameObject audio = new GameObject { name = "[Audio]" };
			DontDestroyOnLoad(audio);

			audio.AddComponent<AudioManager>();
			
		}

		/* ----------------------------------------------------------------------------- */

	}

}
