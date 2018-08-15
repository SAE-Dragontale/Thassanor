/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Toolkit.cs
   Version:			0.0.0
   Description: 	Just some useful functions that I needed. Descriptions are labled within.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

// You're going to have to access these functions either "using Dragontale" or preferably "Dragontale.Math" (etc).
namespace Dragontale {

	static class Math {

		/* ----------------------------------------------------------------------------- */
		// Takes in an input, expected min/max of that input, and then remaps that value to a desired min/max.

		public static float Remap(float input, float inputMin, float inputMax, float outPutMin, float outPutMax) => outPutMin + (input - inputMin) * (outPutMax - outPutMin) / (inputMax - inputMin);

	}

	static class String {

		/* ----------------------------------------------------------------------------- */
		// Compares two strings and returns the number of modifications that need to be made to have the same string.

		public static int Compare(string statement, string comparison) {

			// https://blogs.msdn.microsoft.com/toub/2006/05/05/generic-levenshtein-edit-distance-with-c/
			// https://stackoverflow.com/questions/5859561/getting-the-closest-string-match
			// https://www.dotnetperls.com/levenshtein (Best)

			// Declarations
			int statementLength = statement.Length;
			int comparisonLength = comparison.Length;
			int[,] eachComparison = new int[statementLength + 1, comparisonLength + 1]; // Each possible combination of changes that we could make.

			// Check for zero'd values. If a value is zero, the result must be the length of the compared string.
			if (statementLength == 0)
				return comparisonLength;

			if (comparisonLength == 0)
				return statementLength;

			

			return 0;

		}

		// REFERENCE
		static class LevenshteinDistance {

			public static int Compute(string statement, string comparison) {
				int statementLength = statement.Length;
				int comparisonLength = comparison.Length;
				int[,] eachComparison = new int[statementLength + 1, comparisonLength + 1];

				// Step 1
				if (statementLength == 0) {
					return comparisonLength;
				}

				if (comparisonLength == 0) {
					return statementLength;
				}

				// Step 2
				for (int i = 0; i <= statementLength; eachComparison[i, 0] = i++) {
				}

				for (int j = 0; j <= comparisonLength; eachComparison[0, j] = j++) {
				}

				// Step 3
				for (int i = 1; i <= statementLength; i++) {
					//Step 4
					for (int j = 1; j <= comparisonLength; j++) {
						// Step 5
						int cost = (comparison[j - 1] == statement[i - 1]) ? 0 : 1;

						// Step 6
						eachComparison[i, j] = Math.Min(
							Math.Min(eachComparison[i - 1, j] + 1, eachComparison[i, j - 1] + 1),
							eachComparison[i - 1, j - 1] + cost);
					}
				}
				// Step 7
				return eachComparison[statementLength, comparisonLength];
			}
		}

	}

}