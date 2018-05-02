using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;

    [SerializeField]
    GUIStyle pauseButton;

    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect((Screen.width - 50) / 2, 20, 60, 60), "", pauseButton))
        {
            PauseGame();
        }
    }
}