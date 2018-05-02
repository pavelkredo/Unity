using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillsCount : MonoBehaviour {
    private int kills;

    [SerializeField]
    private Character character;

    [SerializeField]
    GUIStyle kill;

    void Awake () {
        character = FindObjectOfType<Character>();
	}
	
	void Update () {
        kills = character.Kills;
	}

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 60, 10, 50, 50), kills.ToString(), kill);
    }
}
