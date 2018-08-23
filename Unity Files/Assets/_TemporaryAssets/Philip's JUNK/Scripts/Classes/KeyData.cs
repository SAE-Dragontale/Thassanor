/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Philip Ong
   File:			KeyData.cs
   Version:			0.0.1
   Description: 	Class that stores a string for ingame action and its associated buttontext for setting keybinds
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
using UnityEngine;
using TMPro;

[System.Serializable]
public class KeyData {

    [Tooltip("Input Action Name")]
    public string Action;
    [Tooltip("Select corresponding button text")]
    public TextMeshProUGUI buttonText;
}
