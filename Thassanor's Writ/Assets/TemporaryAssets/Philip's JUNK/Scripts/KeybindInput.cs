/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Philip Ong
   File:			KeybindInput.cs
   Version:			0.0.2
   Description: 	Manages players Keybindings
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class KeybindInput : MonoBehaviour
{

    [SerializeField]
    private KeyboardHotkeys _keyboardHotkeys;

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    private KeyAxis[] keyAxis = new KeyAxis[2];
    private KeyCode[] keyCode = new KeyCode[3];

    public TextMeshProUGUI up, down, left, right, unitCommand;

    private GameObject currentKey;



    //[Header("Keybind Setup")]
    //[Tooltip("How many keybinds do you have?")]
    //public KeyData[] keyData;

    // Use this for initialization
    void Start()
    {
        Debug.Log(keyAxis[0]);
        keyAxis[0].positive = _keyboardHotkeys._arrayKeyAxis[0].positive;
        keyAxis[0].negative = _keyboardHotkeys._arrayKeyAxis[0].negative;
        keyAxis[1].positive = _keyboardHotkeys._arrayKeyAxis[1].positive;
        keyAxis[1].negative = _keyboardHotkeys._arrayKeyAxis[1].negative;

        keyCode[0] = _keyboardHotkeys._arrayKeyCode[0];
        keyCode[1] = _keyboardHotkeys._arrayKeyCode[1];
        keyCode[2] = _keyboardHotkeys._arrayKeyCode[2];

        up.text = keyAxis[0].positive.ToString();
        down.text = keyAxis[0].negative.ToString();
        left.text = keyAxis[1].positive.ToString();
        right.text = keyAxis[1].negative.ToString();

        unitCommand.text = keyCode[2].ToString();

        keys.Add("Up", keyAxis[0].positive);
        keys.Add("Down", keyAxis[0].negative);
        keys.Add("Left", keyAxis[1].positive);
        keys.Add("Right", keyAxis[1].negative);
        keys.Add("UnitCommand", keyCode[2]);

        //up.text = key["Move Up"].ToString();
        //down.text = key["Move Down"].ToString();
        //left.text = key["Move Left"].ToString();
        //right.text = key["Move Right"].ToString();
        //unitCommand.text = key["Follow / Stay"].ToString();
    }

    private void OnGUI()
    {
        if (currentKey)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                if (e.keyCode != KeyCode.Return)
                {
                    _keyboardHotkeys = new KeyboardHotkeys();
                    keys[currentKey.name] = e.keyCode;
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

    public void SaveHotKeysToProfile()
    {
        KeyboardHotkeys newKeyboardHotkeys = ScriptableObject.CreateInstance<KeyboardHotkeys>();
        newKeyboardHotkeys._arrayKeyAxis[0].positive = keys["Up"];
        newKeyboardHotkeys._arrayKeyAxis[0].negative = keys["Down"];
        newKeyboardHotkeys._arrayKeyAxis[1].positive = keys["Left"];
        newKeyboardHotkeys._arrayKeyAxis[1].negative = keys["Right"];

        newKeyboardHotkeys._arrayKeyCode[2] = keys["UnitCommand"];
    }
}

