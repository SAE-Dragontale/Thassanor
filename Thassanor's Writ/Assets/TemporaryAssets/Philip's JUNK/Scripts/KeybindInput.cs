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
using System.IO;

public class KeybindInput : MonoBehaviour
{
    [SerializeField]
    private KeyboardHotkeys _keyboardHotkeys;

    private string gameDataFileName = "data.json";
    private GameData loadedData = new GameData();

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    private KeyAxis[] keyAxis = new KeyAxis[2];
    private KeyCode[] keyCode = new KeyCode[3];

    public TextMeshProUGUI up, down, left, right, unitCommand;
    public Text debugLoggerText;

    private GameObject currentKey;

    //[Header("Keybind Setup")]
    //[Tooltip("How many keybinds do you have?")]
    //public KeyData[] keyData;

    // Use this for initialization
    void Start()
    {
        LoadKeyLayout();
        up.text     =   keyAxis[0].positive.ToString();
        down.text   =   keyAxis[0].negative.ToString();
        left.text   =   keyAxis[1].positive.ToString();
        right.text  =   keyAxis[1].negative.ToString();

        unitCommand.text = keyCode[2].ToString();

        //keys.Add("Up", keyAxis[0].positive);
        //keys.Add("Down", keyAxis[0].negative);
        //keys.Add("Left", keyAxis[1].positive);
        //keys.Add("Right", keyAxis[1].negative);
        //keys.Add("UnitCommand", keyCode[2]);
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

    private void LoadKeyLayout()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(dataAsJson))
            {
                debugLoggerText.text = "true";
                keyAxis[0].positive = _keyboardHotkeys._arrayKeyAxis[0].positive;
                keyAxis[0].negative = _keyboardHotkeys._arrayKeyAxis[0].negative;
                keyAxis[1].positive = _keyboardHotkeys._arrayKeyAxis[1].positive;
                keyAxis[1].negative = _keyboardHotkeys._arrayKeyAxis[1].negative;

                keyCode[0] = _keyboardHotkeys._arrayKeyCode[0];
                keyCode[1] = _keyboardHotkeys._arrayKeyCode[1];
                keyCode[2] = _keyboardHotkeys._arrayKeyCode[2];
            }
            else
            {
                debugLoggerText.text = "false";
                loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
                keyAxis[0].positive = loadedData._arrayKeyAxis[0].positive;
                keyAxis[0].negative = loadedData._arrayKeyAxis[0].negative;
                keyAxis[1].positive = loadedData._arrayKeyAxis[1].positive;
                keyAxis[1].negative = loadedData._arrayKeyAxis[1].negative;

                keyCode[2] = loadedData._arrayKeyCode[0];
            }
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    public void SaveKeyLayout()
    {
        loadedData._arrayKeyAxis[0].positive = keyAxis[0].positive;
        loadedData._arrayKeyAxis[0].negative = keyAxis[0].negative;
        loadedData._arrayKeyAxis[1].positive = keyAxis[1].positive;
        loadedData._arrayKeyAxis[1].negative = keyAxis[1].negative;
        loadedData._arrayKeyCode[0] = keyCode[2];

        string dataAsJson = JsonUtility.ToJson(loadedData);
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        File.WriteAllText(filePath, dataAsJson);
    }
}

