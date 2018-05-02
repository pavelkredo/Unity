using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 10f;
    private Vector3 direction;
    private GameObject parent;
    private SpriteRenderer sprite;

    public Vector3 Direction { set { direction = value; } }

    public GameObject Parent { set { parent = value; } }

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        Destroy(gameObject, 1.5f);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit.gameObject != parent)
        {
            Destroy(gameObject);
        }
    }
}
