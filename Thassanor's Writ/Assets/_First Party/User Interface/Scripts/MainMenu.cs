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

    [Header("Focus Buttons")]
    //buttons to focus on (after menu change)
    public GameObject hotkeysButton;
    public GameObject playButton;
    public GameObject audioSlider;

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoreButton()
    {

    }

    public void HelpButton()
    {

    }

    public void OptionsButton()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(hotkeysButton);
    }

    public void QuitButton()
    {

    }

    public void BackButtonOptions()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    public void BackButtonAudio()
    {
        audioMenu.SetActive(false);
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(hotkeysButton);
    }

    public void AudioButton()
    {
        optionsMenu.SetActive(false);
        audioMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(audioSlider);
    }
}
