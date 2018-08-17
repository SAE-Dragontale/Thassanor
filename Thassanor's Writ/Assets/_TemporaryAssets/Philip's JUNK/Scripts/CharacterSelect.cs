/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Philip Ong
   File:			CharacterSelect.cs
   Version:			0.0.1
   Description: 	Script that handles character selection and passes info to DontDestroyOnLoad object [Thassanor]
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
using UnityEngine;

public class CharacterSelect : MonoBehaviour{

    public NecromancerStyle Cute;
    public NecromancerStyle Soultry;

    public GameObject thassanor;
    private PlayerData playerData;

    public void Start()
    {
        thassanor = GameObject.Find("[Thassanor]");
        thassanor.GetComponent<PlayerData>().playerCharacter = Soultry;
    }

    public void CharacterSelection(int characterIndex)
    {
        if(characterIndex == 0)
        {
            thassanor.GetComponent<PlayerData>().playerCharacter = Soultry;
            //characterSelection = Soultry;
        }
        else if(characterIndex == 1)
        {
            thassanor.GetComponent<PlayerData>().playerCharacter = Cute;
            //characterSelection = Cute;
        }
    }
}
