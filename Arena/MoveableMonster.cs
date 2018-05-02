using System.Collections;
using UnityEngine;

public class MoveableMonster : Monster
{
    [SerializeField]
    private float speed = 3.0f;

    private Rigidbody2D rigidbody;
    private Bullet bullet;

    private Vector3 direction;

    protected override void Awake()
    {
        lives -= 6;
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        originalColor = sprite.color;
        bullet = Resources.Load<Bullet>("Bullet");

        if (character == null)
        {
            character = FindObjectOfType<Character>();
        }
    }

    protected override void Update()
    {
        if (character == null)
        {
            character = FindObjectOfType<Character>();
        }

        direction = transform.right;

        if (lives <= 0)
        {
            character.Kills++;
            ReceiveDamage();
        }

        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, character.transform.position, speed * Time.deltaTime);

        if (character.transform.position.x > transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    protected override IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();

        if (bullet)
        {
            Color tempColor = Color.red;
            sprite.color = tempColor;
            StartCoroutine(ChangeColor());

            if (character.transform.position.x > transform.position.x)
            {
                direction *= -1;
            }
            transform.position = Vector3.Lerp(transform.position, transform.position + direction, 3.0f);

            lives--;
        }
    }
}