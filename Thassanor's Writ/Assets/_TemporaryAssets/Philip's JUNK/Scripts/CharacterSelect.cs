/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Philip Ong
   File:			CharacterSelect.cs
   Version:			0.0.0
   Description: 	
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour{

    public NecromancerStyle Cute;
    public NecromancerStyle Soultry;

    public GameObject thassanor;
    private int sceneIndex;
    private PlayerData playerData;

    //[SerializeField]
    //private GameObject characterControllerObject;
    public GameObject playerCharacter;
    public CharVisuals playerSprite;
    private bool setCharacterSprite = true;

    public void Start()
    {
        thassanor = GameObject.Find("[Thassanor]");
        thassanor.GetComponent<PlayerData>().playerCharacter = Soultry;
    }

    public void Update()
    {
        //if(SceneManager.GetActiveScene().buildIndex == 1 && setCharacterSprite == true)
        //{
        //    playerCharacter = GameObject.FindGameObjectWithTag("Player");
        //    playerSprite = playerCharacter.GetComponent<CharVisuals>();
        //    playerSprite._necromancerStyle = characterSelection;
        //    setCharacterSprite = false;
        //}
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
