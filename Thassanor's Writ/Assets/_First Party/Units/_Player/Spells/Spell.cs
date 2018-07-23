/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Spell.cs
   Version:			0.1.0
   Description: 	The base inheritance framework for player spells. This class contains any global calls or variables needed to reference by the loadouts.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ScriptableObject {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Tooltip("The name of the spell.")]
	public string _stSpellName;

	[Tooltip("The mana cost of the spell.")]
	public float _flManaCost;

	[Space] [Tooltip("The phrase used to invoke the spell, starting from the easiest difficulty at 0, to the hardest at 3.")]
	public string[] _astSpellPhrase = new string[4];

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public abstract void CastSpell();

	/* ----------------------------------------------------------------------------- */
	
}
