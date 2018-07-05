using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour {

    [Header("Menu Containers")]
    //Menu Containers
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject loreMenu;
    public GameObject helpMenu;
    public GameObject audioMenu;
    public GameObject screenMenu;
    public GameObject multiplayerMenu;
    public GameObject playMenu;

    [Header("Focus Buttons")]
    [Tooltip("Store all Focus buttons here.")]
    //buttons to focus on (after menu change)
    public GameObject hotkeysButton;
    public GameObject playButton;
    public GameObject audioSlider;
    public GameObject resolutionDropdown;
    public GameObject newgameButton;
    public GameObject createButton;

    [Header("UI Windows")]
    [Tooltip("Store all windows here, window named 'MainMenu' will be set active on runtime.")]
    //array storing all the UI windows 
    public GameObject[] windowsUI;

    private void Awake()
    {
        MakeMainMenuActive();
    }

    public void ButtonPlay()
    {
        mainMenu.SetActive(false);
        playMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(newgameButton);
    }

    public void ButtonLore()
    {

    }

    public void ButtonHelp()
    {

    }

    public void ButtonOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(hotkeysButton);
    }

    public void ButtonQuit()
    {
        Application.Quit();
    }

    public void ButtonBackOptions()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    public void ButtonBackPlay()
    {
        playMenu.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    public void ButtonBackAudio()
    {
        audioMenu.SetActive(false);
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(hotkeysButton);
    }

    public void ButtonBackScreen()
    {
        screenMenu.SetActive(false);
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(hotkeysButton);
    }

    public void ButtonBackMultiplayer()
    {
        multiplayerMenu.SetActive(false);
        playMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(newgameButton);
    }

    public void ButtonAudio()
    {
        optionsMenu.SetActive(false);
        audioMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(audioSlider);
    }

    public void ButtonScreenOptions()
    {
        optionsMenu.SetActive(false);
        screenMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(resolutionDropdown);
    }

    public void ButtonMultiplayerPlay()
    {
        playMenu.SetActive(false);
        multiplayerMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(createButton);
    }

    void MakeMainMenuActive()
    {
        foreach (GameObject windowUI in windowsUI)
        {
            if (windowUI.name != "MainMenu")
            {
                windowUI.SetActive(false);
            }
            else
            {
                windowUI.SetActive(true);
            }
        }
    }
}
