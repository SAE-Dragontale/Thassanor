/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Toolkit.cs
   Version:			0.0.0
   Description: 	Just some useful functions that I needed. Descriptions are labled within.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

// You're going to have to access these functions either "using Dragontale" or preferably "Dragontale.Math" (etc).
namespace Dragontale {

	// Just some custom caluclations that aren't supported in Unity Mathf for some reason.
	static class Math {

		// Takes in an input, expected min/max of that input, and then remaps that value to a desired min/max.
		public static float Remap(float input, float inputMin, float inputMax, float outPutMin, float outPutMax) => outPutMin + (input - inputMin) * (outPutMax - outPutMin) / (inputMax - inputMin);

	}

}