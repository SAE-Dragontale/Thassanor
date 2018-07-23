/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			SpellSummon.cs
   Version:			0.1.0
   Description: 	The basic spell modification that summons the undead. This spell handles killing, and resurrecting neutral actors.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Summon Minion", menuName = "Spells/Summon")]
public class SpellSummon : Spell {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public enum SummonType {Necromancy, Resurrection};

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Tooltip("The number of targets that this spell can target at maximum.")]
	public int _itTargets;

	[Tooltip("The time the spell takes to complete after it has been cast in seconds.")]
	public float _flTimeToComplete;

	[Tooltip("The Summon-Type this spell is. Necromancy kills alive units and resurrects them. Resurrection simply revives previously undead units.")]
	public SummonType _enSummonType;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public override void CastSpell () {

	}

	/* ----------------------------------------------------------------------------- */
	
}
