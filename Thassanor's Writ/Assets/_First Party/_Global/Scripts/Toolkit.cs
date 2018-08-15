/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Toolkit.cs
   Version:			0.1.0
   Description: 	Just some useful functions that I needed. Descriptions are labled within.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

// You're going to have to access these functions either "using Dragontale" or preferably "Dragontale.Math" (etc).
namespace Dragontale {

	static class FableMath {

		/* ----------------------------------------------------------------------------- */
		// Takes in an input, expected min/max of that input, and then remaps that value to a desired min/max.

		public static float Remap(float input, float inputMin, float inputMax, float outPutMin, float outPutMax) => outPutMin + (input - inputMin) * (outPutMax - outPutMin) / (inputMax - inputMin);

	}

	static class FableString {

		/* ----------------------------------------------------------------------------- */
		// Compares two strings and returns the number of modifications that need to be made to have the same string.

		public static int Compare(string statement, string comparison) {

			// Tutorial credit to: https://www.dotnetperls.com/levenshtein
			// I am using this function for reference at the moment. I am intending to rewrite this wholly in my own code once I understand it better.

			// Declarations
			int statementLength = statement.Length;
			int comparisonLength = comparison.Length;
			int[,] eachComparison = new int[statementLength + 1, comparisonLength + 1]; // Each possible combination of changes that we could make.

			// Check for zero'd values. If a value is zero, the result must be the length of the compared string.
			if (statementLength == 0)
				return comparisonLength;

			if (comparisonLength == 0)
				return statementLength;

			// COPY-PASTE FOR THE MOMENT
			for (int i = 0; i <= statementLength; eachComparison[i, 0] = i++) {}
			for (int j = 0; j <= comparisonLength; eachComparison[0, j] = j++) {}

			// COPY-PASTE FOR THE MOMENT
			for (int i = 1; i <= statementLength; i++) {
				for (int j = 1; j <= comparisonLength; j++) {
					int cost = (comparison[j - 1] == statement[i - 1]) ? 0 : 1;

					eachComparison[i, j] = System.Math.Min(
						System.Math.Min(eachComparison[i - 1, j] + 1, eachComparison[i, j - 1] + 1),
						eachComparison[i - 1, j - 1] + cost);
				}
			}

			// Return our wonderful confusing function.
			return eachComparison[statementLength, comparisonLength];

		}

	}

}