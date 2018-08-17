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

    public Dragontale.Thassanor thassanor;
    private PlayerData playerData;

    public void Start()
    {
        thassanor = FindObjectOfType<Dragontale.Thassanor>();
        playerData = thassanor.GetComponent<PlayerData>();
        playerData.playerCharacter = Soultry;
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
}
