using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    public Toggle fullScreenToggle;
    public Dropdown resolutionsDropdown;
    public TextMeshProUGUI volumeText;
    public Slider volumeSlider;

    private PlayerOptions playerOptions = new PlayerOptions();
    private Resolution[] allResolutions;
    //private List<Resolution> resolutionsWithRefreshRate = new List<Resolution>();
    private Resolution resolution;

    private void Awake()
    {
        allResolutions = Screen.resolutions;
        LoadPlayerOptions();
    }

    private void Start()
    {
        fullScreenToggle.isOn = Screen.fullScreen;

        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < allResolutions.Length; i++)
        {
            string option = $"{allResolutions[i].width} x {allResolutions[i].height} @ {allResolutions[i].refreshRate}";
            options.Add(option);

            if (allResolutions[i].width == Screen.currentResolution.width && allResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        //for (int i = 0; i < allResolutions.Length; i++)
        //{
        //    if (allResolutions[i].refreshRate == Screen.currentResolution.refreshRate)
        //    {
        //        resolutionsWithRefreshRate.Add(allResolutions[i]);
        //    }
        //}

        //foreach (var res in resolutionsWithRefreshRate)
        //{
        //    string option = $"{res.width} x {res.height} @ {res.refreshRate}";
        //    options.Add(option);

        //    if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
        //    {
        //        currentResolutionIndex = resolutionsWithRefreshRate.IndexOf(res);
        //    }
        //}

        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {

        resolution = allResolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("storedResolutionIndex", playerOptions.storedResolutionIndex);
    }

    public void SetVolume(float volume)
    {
        volumeText.text = $"Volume: {volume.ToString("F2")}";
        audiomixer.SetFloat("volume", volume);
        playerOptions.storedVolume = volume;
        PlayerPrefs.SetFloat("storedVolume", playerOptions.storedVolume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("storedQualityIndex", playerOptions.storedQualityIndex);
    }

    //toggling windowed mode.
    //Initially Screen.fullScreen = !Screen.fullScreen was used but ran into problems even though this was noted in the unity API
    public void SetFullScreen(bool fullScreenBool)
    {
        Screen.SetResolution(resolution.width, resolution.height, fullScreenBool);
        if (fullScreenBool)
        {
            playerOptions.storedIsFullScreen = 1;
        }
        else
        {
            playerOptions.storedIsFullScreen = 0;
        }
        PlayerPrefs.SetInt("storedIsFullScreen", playerOptions.storedIsFullScreen);
    }

    private void LoadPlayerOptions()
    {
        if (PlayerPrefs.HasKey("storedIsFullScreen"))
        {
            playerOptions.storedIsFullScreen = PlayerPrefs.GetInt("storedIsFullScreen");

            if (playerOptions.storedIsFullScreen == 1)
            {
                Screen.fullScreen = true;
            }
            else
            {
                Screen.fullScreen = false;
            }
        }

        if (PlayerPrefs.HasKey("storedResolutionIndex"))
        {
            playerOptions.storedResolutionIndex = PlayerPrefs.GetInt("storedResolutionIndex");
            resolution = allResolutions[playerOptions.storedResolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        if (PlayerPrefs.HasKey("storedVolume"))
        {
            playerOptions.storedVolume = PlayerPrefs.GetFloat("storedVolume");
            audiomixer.SetFloat("volume", playerOptions.storedVolume);
            volumeText.text = $"Volume: {playerOptions.storedVolume.ToString("F2")}";
            volumeSlider.value = playerOptions.storedVolume;
        }
    }

    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
    