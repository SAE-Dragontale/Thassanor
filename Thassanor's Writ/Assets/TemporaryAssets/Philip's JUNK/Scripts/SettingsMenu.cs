using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    public Toggle windowedToggle;
    public Dropdown resolutionsDropdown;
    //public Text textDebug;


    Resolution[] resolutions;

    private void Start()
    {
        if (!Screen.fullScreen)
        { 
            windowedToggle.isOn = false;
        }

        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            //string option = $"{resolutions[i].width} x {resolutions[i].height} @ {resolutions[i].refreshRate}";
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();

    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution;

        if (PlayerPrefs.HasKey("resolutionIndex"))
        {
            int storedResolutionIndex = PlayerPrefs.GetInt("resolutionIndex");
            resolution = resolutions[storedResolutionIndex];
        }
        else
        {
            resolution = resolutions[resolutionIndex];
        }

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolutionIndex", resolution.width);
    }

    public void SetVolume(float volume)
    {
        if (PlayerPrefs.HasKey("storedVolume"))
        {
            int storedVolume = PlayerPrefs.GetInt("storedVolume");
            audiomixer.SetFloat("volume", storedVolume);
        }
        else
        {
            audiomixer.SetFloat("volume", volume);
        }
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //toggling windowed mode.
    //Initially Screen.fullScreen = !Screen.fullScreen was used but ran into problems even though this was noted in the unity API
    public void SetFullscreen(bool fullScreenBool)
    {
        bool storedIsFullScreen;
        Resolution resolution = Screen.currentResolution;
        if (PlayerPrefs.HasKey("isFullScreen"))
        {
            int isFullScreenInt = PlayerPrefs.GetInt("isFullScreen");

            if (isFullScreenInt == 1)
            {
                storedIsFullScreen = true;

            }
            else
            {
                storedIsFullScreen = false;
            }
            Screen.SetResolution(resolution.width, resolution.height, storedIsFullScreen);
        }
        else
        {
            if (!fullScreenBool)
            {
                Screen.SetResolution(resolution.width, resolution.height, fullScreenBool);
                PlayerPrefs.SetInt("isFullscreen", 1);
            }
        }

        Screen.fullScreen = fullScreenBool;

        //Debug.Log(Screen.fullScreen);
        //if (Screen.fullScreen == false)
        //{
        //    //Screen.fullScreen = true;
        //    Screen.SetResolution(Screen.width, Screen.height, true, 60);
        //}
        //else
        //{
        //    Screen.SetResolution(1920, 1080, false, 60);
        //    //Screen.fullScreen = false;
        //
    }

    //public void GoFuckkaYourself(string HueSaturationYelling)
    //{
    //    textDebug.text += $"\n {HueSaturationYelling}";
    //}

}
    