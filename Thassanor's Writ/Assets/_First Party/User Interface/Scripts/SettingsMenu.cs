using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    public Toggle windowedToggle;
    public Text textDebug;

    private void Start()
    {
        if (Screen.fullScreen == false)
        {
            windowedToggle.isOn = false;
        }
        else
        {
            windowedToggle.isOn = true;
        }
    }

    public void SetVolume(float volume)
    {
        audiomixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //toggling windowed mode.
    //Initially Screen.fullScreen = !Screen.fullScreen was used but ran into problems even though this was noted in the unity API
    public void SetFullscreen(bool fullScreenBool)
    {
        Screen.fullScreen = fullScreenBool;
        if (!fullScreenBool)
        {
            Resolution resolution = Screen.currentResolution;
            Screen.SetResolution(resolution.width, resolution.height, fullScreenBool);
        }

        GoFuckkaYourself(Screen.fullScreen.ToString());
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

    public void SetResolution()
    {

    }

    public void GoFuckkaYourself(string HueSaturationYelling)
    {
        textDebug.text += $"\n {HueSaturationYelling}";
    }

}
    