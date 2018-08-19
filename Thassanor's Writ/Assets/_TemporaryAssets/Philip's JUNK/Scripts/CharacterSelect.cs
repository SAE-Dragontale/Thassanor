/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Philip Ong
   File:			CharacterSelect.cs
   Version:			0.0.1
   Description: 	Script that handles character selection and passes info to DontDestroyOnLoad object [Thassanor]
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour{


    public NecromancerStyle Cute;
    public NecromancerStyle Soultry;

    public Spell SummonMelee;
    public Spell SummonRange;
    public Spell Resurrect;
    private Spell spell1;
    private Spell spell2;
    public Dropdown spell1DropDown;
    public Dropdown spell2DropDown;

    public Dragontale.Thassanor thassanor;
    private PlayerData playerData;


    public void Start()
    {
        thassanor = FindObjectOfType<Dragontale.Thassanor>();
        playerData = thassanor.GetComponent<PlayerData>();
        playerData.playerCharacter = Soultry;
        spell1 = SummonMelee;
        spell2 = SummonRange;
        playerData.playerSpells[0] = SummonMelee;
        playerData.playerSpells[1] = SummonRange;
    }

    public void CharacterSelection(int characterIndex)
    {
        if(characterIndex == 0)
        {
            playerData.playerCharacter = Soultry; 
            //characterSelection = Soultry;
        }
        else if(characterIndex == 1)
        {
            playerData.playerCharacter = Cute;
            //characterSelection = Cute;
        }
    }
    public void SpellLoadout1(int spellIndex)
    {
        if (spellIndex == 0)
        {
            playerData.playerSpells[0] = SummonMelee;
        }
        else if (spellIndex == 1)
        {
            playerData.playerSpells[0] = SummonRange;
        }
        else if (spellIndex == 2)
        {
            playerData.playerSpells[0] = Resurrect;
        }
    }

    public void SpellLoadout2(int spellIndex)
    {
        if (spellIndex == 0)
        {
            playerData.playerSpells[1] = SummonMelee;
        }
        else if (spellIndex == 1)
        {
            playerData.playerSpells[1] = SummonRange;
        }
        else if (spellIndex == 2)
        {
            playerData.playerSpells[1] = Resurrect;
        }
    }
}
