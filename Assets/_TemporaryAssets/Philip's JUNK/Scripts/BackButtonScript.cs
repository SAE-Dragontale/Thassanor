/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Philip Ong
   File:			BackButtonScript.cs
   Version:			0.0.0
   Description: 	
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonScript : MonoBehaviour {

    //public int disableNum;
    //public int enableNum;
    public string disableName;
    public string enableName;
    public int focusNum;
    public MainMenu mainMenu;

    public void Start()
    {
        //disableNum = mainMenu.enableNum;
        //enableNum = mainMenu.disableNum;
        disableName = mainMenu.enableName;
        enableName = mainMenu.disableName;
        focusNum = mainMenu.previousFocusNum;
    }

    public void OnEnable()
    {
        //enableNum = FindObjectOfType<MainMenu>().enableNum;
        //disableNum = FindObjectOfType<MainMenu>().disableNum;
        //focusNum = FindObjectOfType<MainMenu>().focusNum;
        mainMenu = FindObjectOfType<MainMenu>();
    }

}
