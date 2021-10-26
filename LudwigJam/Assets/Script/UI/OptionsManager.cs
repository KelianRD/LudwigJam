using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour
{
    private MainMenuUIManager mainMenu;
    public AudioMixer volumeMixer;

    private void Start()
    {
        mainMenu = GetComponent<MainMenuUIManager>();
    }

    //Called when the back to main menu button is pressed
    public void BackToMainMenu()
    {
        mainMenu.mainMenuObject.SetActive(true);
        mainMenu.optionsMenuObject.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        volumeMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
