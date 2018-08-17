using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GrabPlayerProfile : MonoBehaviour {

    private CharacterSelect characterSelect;

    private void Start()
    {
        characterSelect = (GameObject.Find("PlayerProfile").GetComponent<CharacterSelect>());
    }

    public void CharacterSelection(int value)
    {
        characterSelect.CharacterSelection(value);
    }

    //private void Update()
    //{
    //    switch (characterDropDown.value)
    //    {
    //        case 1:
    //            characterSelect.CharacterSelection(characterDropDown.value);
    //            break;
    //        case 2:
    //            characterSelect.CharacterSelection(characterDropDown.value);
    //            break;
    //    }
    //}

    //EventTrigger.Entry entry1 = new EventTrigger.Entry();
    //entry1.eventID = EventTriggerType.Drag; // Here would not be drag as you need buttons and not joystick
    //entry1.callback.AddListener((eventData) => { GetComponent<PlayerMovement>().OnJoystickDrag(); }); // here you call whatever method you want that belongs to script that is attached to this gameobject
    //joystick.GetComponent<EventTrigger>().triggers[0] = entry1; // assign event that you setup to event trigger
}
