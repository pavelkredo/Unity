using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField]
    private Character character;
    private EdgeCollider2D cd;

    void Awake()
    {
        character = FindObjectOfType<Character>();
        cd = GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        if (character.transform.position.y > this.transform.position.y)
        {
            cd.isTrigger = false;
        }
        else
        {
            cd.isTrigger = true;
        }
    }
}
