using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GameController : MonoBehaviour
{
    EventSystem eventSystem;
    GameObject lastSelectedObject;

    void Awake()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    void Update()
    {
        ReselectButton();
    }

    void ReselectButton()
    {
        if (eventSystem.currentSelectedGameObject != null)
        {
            lastSelectedObject = eventSystem.currentSelectedGameObject;
        }
        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(lastSelectedObject);
        }
    }
}