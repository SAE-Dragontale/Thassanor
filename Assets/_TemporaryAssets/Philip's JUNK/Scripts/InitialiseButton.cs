using UnityEngine;
using UnityEngine.EventSystems;

public class InitialiseButton : MonoBehaviour
{

    GameObject lastSelect;

    // Called at start of runtime
    void Start()
    {
        lastSelect = EventSystem.current.firstSelectedGameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelect);
        }
        else
        {
            lastSelect = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void CurrentMouseHover(GameObject focusButton)
    {
        lastSelect = focusButton;
    }
}