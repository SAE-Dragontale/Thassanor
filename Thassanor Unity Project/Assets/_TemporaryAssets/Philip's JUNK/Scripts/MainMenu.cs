using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {

    [Header("Menu Containers")]
    //Menu Containers
    public GameObject[] menuContainers;
    public GameObject[] focusElements;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    //public GameObject loreMenu;
    //public GameObject helpMenu;
    public GameObject audioMenu;
    public GameObject screenMenu;
    public GameObject multiplayerMenu;
    public GameObject playMenu;
    public GameObject hotkeyMenu;
    public GameObject menuObjects;

    [Header("Focus Buttons")]
    [Tooltip("Store all Focus buttons here.")]
    //buttons to focus on (after menu change)
    public GameObject hotkeysButton;
    public GameObject playButton;
    public GameObject audioSlider;
    public GameObject resolutionDropdown;
    public GameObject newgameButton;
    public GameObject createButton;
    public GameObject keybindButton;

    public Animator scrollAnimator;
    public bool startAnimator;

    [Header("UI Windows")]
    [Tooltip("Store all windows here, window named 'MainMenu' will be set active on runtime.")]
    //array storing all the UI windows 
    public GameObject[] windowsUI;
    //public int disableNum;
    //public int enableNum;
    public string disableName;
    public string enableName;
    public int focusNum;
    public int previousFocusNum;
    public int lobbyManagerIndex;
    public int serverListIndex;
    private bool startAnim;
    public RectTransform transform;

    public AudioManager audioManager;

    private void Awake()
    {
        scrollAnimator = GetComponent<Animator>();
        menuContainers = GameObject.FindGameObjectsWithTag("Menu Objects");
        focusElements = GameObject.FindGameObjectsWithTag("Focus");
        for (int i = 1; i < menuContainers.Length; i++)
        {
            if(menuContainers[i].name == "LobbyManager")
            {
                lobbyManagerIndex = i;
            }
            //else if (menuContainers[i].name == "ServerListPanel")
            //{
            //    serverListIndex = i;
            //}
            menuContainers[i].SetActive(false);
        }


        //for (int i = 1; i < focusElements.Length; i++)
        //{
        //    if (!focusElements[i].activeInHierarchy)
        //    focusElements[i].SetActive(false);
        //}

    }

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        MakeMainMenuActive();
        focusElements[5].SetActive(true);
    }

    public void OpenLobbyPanel()
    {
        multiplayerMenu.SetActive(false);
        menuContainers[lobbyManagerIndex].SetActive(true);
    }

    //public void OpenServerList()
    //{
    //    multiplayerMenu.SetActive(false);
    //    menuContainers[lobbyManagerIndex].SetActive(true);
    //}

    //public void uiEnable(int i)
    //{
    //    enableNum = i;
    //}

    //public void uiDisable(int i)
    //{
    //    disableNum = i;
    //}

    public void UIEnable(string name)
    {
        enableName = name;
    }

    public void UIDisable(string name)
    {
        disableName = name;
    }

    public void uiFocusElement(int i)
    {
        focusNum = i;
    }

    public void ButtonTransform(RectTransform passedObject)
    {
        transform = passedObject;
    }

    public void PreviousButtonFocus(GameObject button)
    {
        string objectname;

        objectname = button.name;
        for(int i = 1; i > focusElements.Length; i++)
        {
            if(objectname == focusElements[i].name)
            {
                previousFocusNum = i;
            }
        }
    }

    public void StartAnimation()
    {
        //scrollAnimator.SetBool("StartAnimation", true);
        scrollAnimator.SetBool("StartAnimation", true);
    }

    //public void ActiveUI()
    //{
    //    menuContainers[enableNum].SetActive(true);
    //    menuContainers[disableNum].SetActive(false);
    //    EventSystem.current.SetSelectedGameObject(focusElements[focusNum]);
    //    scrollAnimator.SetBool("StartAnimation", false);
    //}

    public void ActiveUI()
    {
        //audioManager.UIButtonClick(new Vector3 (transform.position.x, transform.position.y, transform.position.z));
        foreach (GameObject menuItem in menuContainers)
        {
            if (menuItem.name == disableName)
            {
                menuItem.SetActive(false);
            }
            else if (menuItem.name == enableName)
            {
                menuItem.SetActive(true);
            }
        }

        EventSystem.current.SetSelectedGameObject(focusElements[focusNum]);
        scrollAnimator.SetBool("StartAnimation", false);
    }

    //public void BackButton(BackButtonScript script)
    //{
    //    enableNum = script.enableNum;
    //    disableNum = script.disableNum;
    //    focusNum = script.focusNum;
    //    StartAnimation();
    //}

    public void BackButton(BackButtonScript script)
    {
        enableName = script.enableName;
        disableName = script.disableName;
        focusNum = script.focusNum;
        StartAnimation();
    }

    //public void ButtonPlay()
    //{
    //    scrollAnimator.SetBool(1, true);
    //    mainMenu.SetActive(false);
    //    playMenu.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(newgameButton);
    //}

    //public void ButtonLore()
    //{

    //}

    //public void ButtonHelp()
    //{

    //}

    //public void ButtonOptions()
    //{
    //    mainMenu.SetActive(false);
    //    optionsMenu.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(hotkeysButton);
    //}

    //public void ButtonHotkeys()
    //{
    //    optionsMenu.SetActive(false);
    //    hotkeyMenu.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(keybindButton);
    //}

    public void ButtonQuit()
    {
        Application.Quit();
    }

    //public void ButtonBackOptions()
    //{
    //    mainMenu.SetActive(true);
    //    optionsMenu.SetActive(false);
    //    EventSystem.current.SetSelectedGameObject(playButton);
    //}

    //public void ButtonBackPlay()
    //{
    //    playMenu.SetActive(false);
    //    mainMenu.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(playButton);
    //}

    //public void ButtonBackAudio()
    //{
    //    audioMenu.SetActive(false);
    //    optionsMenu.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(hotkeysButton);
    //}

    //public void ButtonBackScreen()
    //{
    //    screenMenu.SetActive(false);
    //    optionsMenu.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(hotkeysButton);
    //}

    //public void ButtonBackMultiplayer()
    //{
    //    multiplayerMenu.SetActive(false);
    //    menuObjects.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(newgameButton);
    //}

    //public void ButtonBackHotKeys()
    //{
    //    hotkeyMenu.SetActive(false);
    //    optionsMenu.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(hotkeysButton);
    //}

    //public void ButtonAudio()
    //{
    //    optionsMenu.SetActive(false);
    //    audioMenu.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(audioSlider);
    //}

    //public void ButtonScreenOptions()
    //{
    //    optionsMenu.SetActive(false);
    //    screenMenu.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(resolutionDropdown);
    //}

    //public void ButtonMultiplayerPlay()
    //{
    //    menuObjects.SetActive(false);
    //    multiplayerMenu.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(createButton);
    //}

    void MakeMainMenuActive()
    {
        foreach (GameObject windowUI in windowsUI)
        {
            if (windowUI.name == "MainMenu")
            {
                windowUI.SetActive(true);
            }
            else if (windowUI.name == "MenuObjects")
            {
                windowUI.SetActive(true);
            }
            else
            {
                windowUI.SetActive(false);
            }
        }
    }
}

