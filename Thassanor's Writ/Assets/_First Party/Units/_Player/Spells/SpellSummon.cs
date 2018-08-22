/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Hayden Reeve
   File:			SpellSummon.cs
   Version:			0.2.2
   Description: 	The basic spell modification that summons the undead. This spell handles killing, and resurrecting neutral actors.
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Summon Minion", menuName = "Spells/Summon")]
public class SpellSummon : Spell {

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		References
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Space] [Header("Summon Data")]
	public Sprite _splashArt;

	public enum SummonType { Necromancy, Resurrection };

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Variables
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	[Space]
	[Tooltip("The number of targets that this spell can target at maximum.")]
	public int _healthRequired;

	[Tooltip("The time the spell takes to complete after it has been cast in seconds.")]
	public float _castingDelay;

	[Tooltip("The Summon-Type this spell is. Necromancy kills alive units and resurrects them. Resurrection simply revives previously undead units.")]
	public SummonType _summonType;

	public UnitStyle _unitStyle;

	/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
		Class Calls
	// --------------------------------------------------------------------------------------------------------------------------------------------------------- */

	public override void CastSpell () {



	}

	/* ----------------------------------------------------------------------------- */
	
}
