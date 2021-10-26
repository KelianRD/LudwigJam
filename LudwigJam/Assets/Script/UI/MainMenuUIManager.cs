using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public GameObject mainMenuObject, optionsMenuObject, tutorialObject;

    private void Start()
    {
        mainMenuObject.SetActive(true);
        optionsMenuObject.SetActive(false);
        tutorialObject.SetActive(false);
    }

    //Called when the quit play is pressed
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Load next scene in build settings order
    }

    //Called when the options button is pressed
    public void Options()
    {
        mainMenuObject.SetActive(false);
        optionsMenuObject.SetActive(true);
    }

    //Called when the quit button is pressed
    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    public void Tutorial()
    {
        mainMenuObject.SetActive(false);
        tutorialObject.SetActive(true);
    }

    public void BackFromTuto()
    {
        mainMenuObject.SetActive(true);
        tutorialObject.SetActive(false);
    }
}
