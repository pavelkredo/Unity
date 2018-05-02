using System.Collections;
using UnityEngine;

public class Monster : Unit
{
    protected int lives = 10;

    [SerializeField]
    protected Character character;
    protected SpriteRenderer sprite;

    protected Color originalColor;

    protected virtual void Awake()
    {
        character = FindObjectOfType<Character>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        originalColor = sprite.color;
    }

    protected virtual void Update()
    {
        if (lives <= 0)
        {
            character.Kills++;
            ReceiveDamage();
        }
    }

    protected virtual IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();

        if (bullet)
        {
            Color tempColor = Color.red;
            sprite.color = tempColor;
            StartCoroutine(ChangeColor());

            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);

            lives--;
        }
    }
}