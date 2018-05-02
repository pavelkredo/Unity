using UnityEngine;

public class LivesCount : MonoBehaviour
{
    [SerializeField]
    private Texture2D live;
    [SerializeField]
    Character character;

    private void Awake()
    {
        character = FindObjectOfType<Character>();
    }

    private void OnGUI()
    {
        int left = 20;

        for (int i = 0; i < character.Lives; i++)
        {
            GUI.DrawTexture(new Rect(left, 20, 35, 30), live);
            left += 30;
        }
    }
}
