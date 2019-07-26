using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour {
    public GameObject VolumeSlider;
    public GameObject SensitivitySlider;
    public GameObject ToggleFull;
    public AudioMixer am;

    void Start()
    {
        if (Screen.fullScreen)
            ToggleFull.GetComponent<Toggle>().isOn = true;
        else ToggleFull.GetComponent<Toggle>().isOn = false;
    }

    public void ContinuePressed()
    {
        Debug.Log("Continue pressed!");

        Cursor.visible = false;
        StaticValues.notPaused = 1;
        Time.timeScale = StaticValues.notPaused;
    }

    public void AudioVolume(float sliderValue)
    {
        Debug.Log("Volume changed to " + sliderValue);

        am.SetFloat("masterVolume", sliderValue);
        StaticValues.VolumeValue = sliderValue;
    }

    public void SensitivityChange(float sliderValue)
    {
        Debug.Log("Sensitivity changed to " + sliderValue);

        StaticValues.sens_y = sliderValue;
        StaticValues.sens_x = sliderValue;
    }

    public void OnValueChanged(bool b)
    {
        StaticValues.isFullScreen = b;
        Screen.fullScreen = b;
    }

    public void SettingsPressed()
    {
        Debug.Log("Settings pressed!");

        SensitivitySlider.GetComponent<Slider>().value = StaticValues.sens_x;
        VolumeSlider.GetComponent<Slider>().value = StaticValues.VolumeValue;
    }

    public void ExitPressed()
    {
        Debug.Log("Exit pressed!");

        Application.Quit();
    }
}
