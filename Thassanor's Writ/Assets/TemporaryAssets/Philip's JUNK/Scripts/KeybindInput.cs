/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Philip Ong
   File:			KeybindInput.cs
   Version:			0.0.1
   Description: 	Manages players Keybindings
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeybindInput : MonoBehaviour {

    private Dictionary<string, KeyCode> key = new Dictionary<string, KeyCode>();

    public TextMeshProUGUI up, down, left, right, castspell, quickspell1, quickspell2, quickspell3;

    private GameObject currentKey;

    //[Header("Keybind Setup")]
    //[Tooltip("How many keybinds do you have?")]
    //public KeyData[] keyData;


	// Use this for initialization
	void Start () {
        //int keybindStringIndex = 0;
        //if(key.Count > keybindStringIndex + 1)
        //{
        //    key.Add(keyData[keybindStringIndex].Action, KeyCode.W);
        //}

        key.Add("Move Up", KeyCode.W);
        key.Add("Move Down", KeyCode.S);
        key.Add("Move Left", KeyCode.A);
        key.Add("Move Right", KeyCode.D);
        key.Add("Cast Spell", KeyCode.Space);
        key.Add("Quick Spell 1", KeyCode.Alpha1);
        key.Add("Quick Spell 2", KeyCode.Alpha2);
        key.Add("Quick Spell 3", KeyCode.Alpha3);

        up.text = key["Move Up"].ToString();
        down.text = key["Move Down"].ToString();
        left.text = key["Move Left"].ToString();
        right.text = key["Move Right"].ToString();
        castspell.text = key["Cast Spell"].ToString();
        quickspell1.text = key["Quick Spell 1"].ToString();
        quickspell2.text = key["Quick Spell 2"].ToString();
        quickspell3.text = key["Quick Spell 3"].ToString();


        //buttonText[0].text = key["Move Up"].ToString();
        //buttonText[1].text = key["Move Down"].ToString();
        //buttonText[2].text = key["Move Left"].ToString();
        //buttonText[3].text = key["Move Right"].ToString();
        //buttonText[4].text = key["Cast Spell"].ToString();
        //buttonText[5].text = key["Quick Spell 1"].ToString();
        //buttonText[6].text = key["Quick Spell 2"].ToString();
        //buttonText[7].text = key["Quick Spell 3"].ToString();
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnGUI()
    {
        if (currentKey)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                if(e.keyCode == KeyCode.Return) {
                    key[currentKey.name] = e.keyCode;
                    currentKey.GetComponentInChildren<TextMeshProUGUI>().text = e.keyCode.ToString();
                    currentKey = null;
                }
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }
}
