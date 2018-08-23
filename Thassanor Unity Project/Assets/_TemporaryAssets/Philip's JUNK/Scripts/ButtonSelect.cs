using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ButtonSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
        private EventSystem eventSystem;
        private GameObject lastSelectedObject;

        // Use this for initialization
        void Start()
        {
            eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            lastSelectedObject = EventSystem.current.firstSelectedGameObject;
    }

        public void OnPointerEnter(PointerEventData eventData)
        {
            eventSystem.SetSelectedGameObject(eventData.pointerEnter);
            lastSelectedObject = eventSystem.currentSelectedGameObject;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eventSystem.SetSelectedGameObject(lastSelectedObject);
        }

}
