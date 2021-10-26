using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCam : MonoBehaviour
{
    private InputManager im;

    private void Start()
    {
        im = GetComponent<InputManager>();
    }

    private void Update()
    {
        if (im.escape == 1)
            SceneManager.LoadScene(0);
    }
}
