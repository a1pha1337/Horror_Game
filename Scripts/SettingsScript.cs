using UnityEngine;
using UnityEngine.Audio;

public class SettingsScript : MonoBehaviour {
    public AudioMixer am;

    private string WxH;
    void Start()
    {
        StaticValues.isFullScreen = Screen.fullScreen;
        StaticValues.screenHeight = Screen.height;
        StaticValues.screenWidth = Screen.width;

        WxH = (Screen.width).ToString() + "x" + (Screen.height.ToString());
    }

    public void OnValueChangedResolution(int res)
    {
        switch (res)
        {
            case 0:
                Screen.SetResolution(1024, 768, true);

                StaticValues.screenHeight = 768;
                StaticValues.screenWidth = 1024;

                Debug.Log("Resolution setted to 1024x768");
                break;

            case 1:
                Screen.SetResolution(1152, 864, true);

                StaticValues.screenHeight = 864;
                StaticValues.screenWidth = 1152;

                Debug.Log("Resolution setted to 1152x864");
                break;

            case 2:
                Screen.SetResolution(1280, 720, true);

                StaticValues.screenHeight = 720;
                StaticValues.screenWidth = 1280;

                Debug.Log("Resolution setted to 1280x720");
                break;

            case 3:
                Screen.SetResolution(1440, 900, true);

                StaticValues.screenHeight = 900;
                StaticValues.screenWidth = 1440;

                Debug.Log("Resolution setted to 1440x900");
                break;

            case 4:
                Screen.SetResolution(1600, 900, true);

                StaticValues.screenHeight = 900;
                StaticValues.screenWidth = 1600;

                Debug.Log("Resolution setted to 1600x900");
                break;

            case 5:
                Screen.SetResolution(1680, 1050, true);

                StaticValues.screenHeight = 1050;
                StaticValues.screenWidth = 1680;

                Debug.Log("Resolution setted to 1680x1050");
                break;

            case 6:
                Screen.SetResolution(1920, 1080, true);

                StaticValues.screenHeight = 1080;
                StaticValues.screenWidth = 1920;

                Debug.Log("Resolution setted to 1920x1080");
                break;
        }

    }

    public void OnValueChanged(int dif)
    {
        StaticValues.difficulty = dif;
        Debug.Log("Difficulty setted to " + dif);
    }

    public void FullScreenToggle()
    {
        StaticValues.isFullScreen = !StaticValues.isFullScreen;
        Screen.fullScreen = StaticValues.isFullScreen;
        Debug.Log("Fullscreen!");
    }

    public void AudioVolume(float sliderValue)
    {
        am.SetFloat("masterVolume", sliderValue);
        StaticValues.VolumeValue = sliderValue;
    }

    public void SensitivityChange(float sliderValue)
    {
        StaticValues.sens_y = sliderValue;
        StaticValues.sens_x = sliderValue;
    }
}
