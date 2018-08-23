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

    public void SpellSelection1(int value)
    {
        characterSelect.SpellLoadout1(value);
    }

    public void SpellSelection2(int value)
    {
        characterSelect.SpellLoadout2(value);
    }
}
