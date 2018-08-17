/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Toolkit.cs
   Version:			0.1.3
   Description: 	Just some useful functions that I needed. Descriptions are labled within.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System;

// You're going to have to access these functions either "using Dragontale" or preferably "Dragontale.Math" (etc).
namespace Dragontale {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Math Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	static class MathFable {

		/* ----------------------------------------------------------------------------- */
		// Takes in an input, expected min/max of that input, and then remaps that value to a desired min/max.

		public static float Remap(float input, float inputMin, float inputMax, float outPutMin, float outPutMax) {

			return outPutMin + (input - inputMin) * (outPutMax - outPutMin) / (inputMax - inputMin);

		}

		/* ----------------------------------------------------------------------------- */
		// Math.Min / Math.Max but for three values.

		public static int Min3(int x, int y, int z)			=> Math.Min(x, Math.Min(y, z));

		public static float Min3(float x, float y, float z) => Math.Min(x, Math.Min(y, z));

		public static int Max3(int x, int y, int z)			=> Math.Max(x, Math.Max(y, z));

		public static float Max3(float x, float y, float z) => Math.Max(x, Math.Max(y, z));

		/* ----------------------------------------------------------------------------- */

	}

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		String Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	static class StringFable {

		/* ----------------------------------------------------------------------------- */
		// Compares two strings and returns the number of modifications that need to be made to have the same string.

		public static int Compare(string statement, string comparison) {

			// Code examples: https://www.dotnetperls.com/levenshtein
			// Explanations: https://people.cs.pitt.edu/~kirk/cs1501/Pruhs/Spring2006/assignments/editdistance/Levenshtein%20Distance.htm

			// Declarations
			int statementLength = statement.Length;
			int comparisonLength = comparison.Length;
			int[,] everyComparison = new int[statementLength + 1, comparisonLength + 1]; // Each possible combination of changes that we could make.

			// Check for zero'd values. If a value is zero, the result must be the length of the compared string.
			if (statementLength == 0)
				return comparisonLength;

			if (comparisonLength == 0)
				return statementLength;

			// Initialise the two dimensional array with default values.
			for (int i = 0; i <= statementLength; everyComparison[i, 0] = i++) {}
			for (int i = 0; i <= comparisonLength; everyComparison[0, i] = i++) {}

			// This produces the following effect:

			// 3 0 0 0
			// 2 0 0 0
			// 1 0 0 0
			// 0 1 2 3 etc. for length of string in each direction.

			// Now, we look at each cell between these initial declarations and assign a value based on the minimum of:
			// a. The cell immediately above plus 1.
			// b. The cell immediately to the left plus 1.
			// c. The cell diagonally above and to the left plus the cost.

			for (int x = 1; x <= statementLength; x++) {
				for (int y = 1; y <= comparisonLength; y++) {

					int cost = (comparison[y - 1] == statement[x - 1]) ? 0 : 1;

					everyComparison[x, y] = MathFable.Min3( everyComparison[x - 1, y] + 1, everyComparison[x, y - 1] + 1, everyComparison[x - 1, y - 1] + cost );
					
				}
			}

			// After we've finished, we want to return our 'Conclusion' of [stringLength, stringLength] as it contains our Russian Gypsy Magic.
			return everyComparison[statementLength, comparisonLength];

		}

		/* ----------------------------------------------------------------------------- */

	}

}