/* --------------------------------------------------------------------------------------------------------------------------------------------------------- //
   Author: 			Philip Ong
   File:			KeybindInput.cs
   Version:			0.0.2
   Description: 	Manages players Keybindings
// --------------------------------------------------------------------------------------------------------------------------------------------------------- */
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class KeybindInput : MonoBehaviour
{
    [SerializeField]
    private KeyboardHotkeys _keyboardHotkeys; //base class create by Hayden Reeve which stores necesarry keycodes and provides default keycodes
    private string gameDataFileName = "data.json"; //name of json file to store keybindings
    private GameData loadedData = new GameData(); //base class created to store info from json file
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    private KeyAxis[] keyAxis = new KeyAxis[2];
    private KeyCode[] keyCode = new KeyCode[3];
    private GameObject currentKey; //used to store button pressed to change key bindings, targets the keybinding button in UI
    private string filePath;
    public TextMeshProUGUI up, down, left, right, unitCommand;
    //public Text debugLoggerText;

    private void Awake()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);/*stores the file path to the json file located
        in the streaming assets folder for use in JSON loading and saving*/
    }

    void Start()
    {
        LoadKeyLayout();
        /*keys dictionary stores necessary keycodes along with string name which reflects the 
        GameObject Buttons names, this is required to easily store changed keys in OnGui()*/
        keys.Add("ButtonMoveUp", keyAxis[0].positive);
        keys.Add("ButtonMoveDown", keyAxis[0].negative);
        keys.Add("ButtonMoveLeft", keyAxis[1].negative);
        keys.Add("ButtonMoveRight", keyAxis[1].positive);
        keys.Add("ButtonUnitCommand", keyCode[2]);


        //Debug.Log(keys["ButtonMoveUp"]);

        //keybindings text is displayed in the keybindings menu on the buttons
        up.text             =   keyAxis[0].positive.ToString();
        down.text           =   keyAxis[0].negative.ToString();
        left.text           =   keyAxis[1].negative.ToString();
        right.text          =   keyAxis[1].positive.ToString();
        unitCommand.text    =   keyCode[2].ToString();
    }

    private void OnGUI()
    {
        if (currentKey)//GameObject stored in the ChangeKey() method
        {
            Event e = Event.current;//e is declared as the current event thats happening
            if (e.isKey)//if the current event is a key press then...
            {
                if (e.keyCode != KeyCode.Return)/*if the key isn't Return (this unfortunately is a poor work around for when 
               the player presses enter to select the button to change the key but unfortunately it also inputs Return as the key to 
               store aswell*/
                {
                    //_keyboardHotkeys = new KeyboardHotkeys();
                    //Debug.Log(currentKey.name);
                    keys[currentKey.name] = e.keyCode;/*stores the keycode pressed into the dictionary under its relevent string key
                    (this is the reason for storing the buttons name exactly)*/
                    currentKey.GetComponentInChildren<TextMeshProUGUI>().text = e.keyCode.ToString();/*change the text on the button 
                    to that of the key pressed*/
                    currentKey = null;//resets currentKey back to null to avoid running through loop again
                    SaveKeyLayoutToScriptableObject();
                }
            }
        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;//button pressed is stored
    }

    private void LoadKeyLayout()
    {
        if (File.Exists(filePath))//if the json file exists
        {
            string dataAsJson = File.ReadAllText(filePath);//stores the text from the json file to a string
            if (string.IsNullOrEmpty(dataAsJson))//if the json file is empty then this is the first time the program has been run
            {
                //stores the default keybindings...
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
                //otherwise store loaded keybindings from previous session
                loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

                keyAxis[0].positive = loadedData._arrayKeyAxis[0].positive;
                keyAxis[0].negative = loadedData._arrayKeyAxis[0].negative;
                keyAxis[1].positive = loadedData._arrayKeyAxis[1].positive;
                keyAxis[1].negative = loadedData._arrayKeyAxis[1].negative;

                keyCode[2] = loadedData._arrayKeyCode[0];
            }
        }
        else//if the json file doesn't exist
        {
            Debug.LogError("Cannot load game data!");
        }
    }

    private void SaveKeyLayoutToScriptableObject()
    {
        _keyboardHotkeys._arrayKeyAxis[0].positive = keys["ButtonMoveUp"];
        _keyboardHotkeys._arrayKeyAxis[0].negative = keys["ButtonMoveDown"];
        _keyboardHotkeys._arrayKeyAxis[1].positive = keys["ButtonMoveRight"];
        _keyboardHotkeys._arrayKeyAxis[1].negative = keys["ButtonMoveLeft"];
        _keyboardHotkeys._arrayKeyCode[2] = keys["ButtonUnitCommand"];
    }

    public void SaveKeyLayout()
    {
        SaveKeyLayoutToScriptableObject();
        /*stores the file path to the json file located
        in the streaming assets folder for use in JSON loading and saving*/
        //when this method is called (Save button is pressed) store all current stored keybindings into the json file.
        loadedData._arrayKeyAxis[0].positive = keys["ButtonMoveUp"];
        loadedData._arrayKeyAxis[0].negative = keys["ButtonMoveDown"];
        loadedData._arrayKeyAxis[1].positive = keys["ButtonMoveLeft"];
        loadedData._arrayKeyAxis[1].negative = keys["ButtonMoveRight"];
        loadedData._arrayKeyCode[0] = keys["ButtonUnitCommand"]; ;

        string dataAsJson = JsonUtility.ToJson(loadedData);
        File.WriteAllText(filePath, dataAsJson);
    }
}

