/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			Spell.cs
   Version:			0.0.0
   Description: 	
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Tooltip("The name of the spell.")]
	protected string stSpellName;

	[Tooltip("The mana cost of the spell.")]
	protected float flManaCost;

	[Space] [Tooltip("The phrase used to invoke the spell, starting from the easiest difficulty at 0, to the hardest at 3.")]
	[SerializeField] protected string[] astSpellPhrase = new string[4];

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public abstract void CastSpell();
	
}
