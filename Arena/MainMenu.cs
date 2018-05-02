using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour
{
    private int connectPort = 25000;
    private string stringToEdit = "ENTER IP";
    private int window;

    [SerializeField]
    GUIStyle startButton;
    [SerializeField]
    GUIStyle connectButton;
    [SerializeField]
    GUIStyle exitButton;
    [SerializeField]
    GUIStyle label;
    [SerializeField]
    GUIStyle textArea;

    private void Start()
    {
        window = 1;
    }

    private void OnGUI()
    {
        GUI.BeginGroup(new Rect(Screen.width / 2 - 120, Screen.height / 2 - 100, 300, 300));

        if (window == 1)
        {
            if (GUI.Button(new Rect(0, 15, 240, 50), "START", startButton))
            {
                window = 2;
            }
            if (GUI.Button(new Rect(0, 75, 240, 50), "CONNECT", connectButton))
            {
                window = 3;
            }
            if (GUI.Button(new Rect(0, 135, 240, 50), "EXIT", exitButton))
            {
                window = 4;
            }
        }

        if (window == 2)
        {
            Network.InitializeServer(32, connectPort, false);
            Application.LoadLevel("Main");
        }

        if (window == 3)
        {
            stringToEdit = GUI.TextArea(new Rect(0, 15, 240, 50), stringToEdit, textArea);

            if (GUI.Button(new Rect(0, 75, 240, 50), "CONNECT", connectButton))
            {
                Network.Connect(stringToEdit, connectPort);
                Application.LoadLevel("Main");
            }

            if (GUI.Button(new Rect(0, 135, 240, 50), "BACK", exitButton))
            {
                window = 1;
            }
        }

        if (window == 4)
        {
            GUI.Label(new Rect(50, 30, 180, 30), "Are you sure?", label);
            if (GUI.Button(new Rect(0, 60, 240, 50), "YES", exitButton))
            {
                Application.Quit();
            }
            if (GUI.Button(new Rect(0, 120, 240, 50), "NO", startButton))
            {
                window = 1;
            }
        }

        GUI.EndGroup();
    }
}