using System.Collections;
using UnityEngine;

public class Character : Unit
{
    private int lives = 5;
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float jumpForce = 15.0f;

    private bool isGrounded = false;
    private bool isReloaded = true;
    private bool isProtected = false;

    private int kills = 0;

    private Bullet bullet;

    private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (lives <= 0)
        {
            Application.LoadLevel("Menu");
        }

        if (isGrounded)
        {
            State = CharState.Idle;
        }

        if (Input.GetButton("Horizontal"))
        {
            Run();
        }

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (isReloaded && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if (isProtected)
        {
            StartCoroutine(Protection());
        }
    }

    public int Kills
    {
        get { return kills; }
        set { kills = value; }
    }

    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }

    public bool Protected
    {
        set { isProtected = value; }
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(1);
        isReloaded = true;
    }

    IEnumerator Protection()
    {
        sprite.color = Color.blue;
        yield return new WaitForSeconds(5);
        isProtected = false;
        sprite.color = Color.white;
    }

    private void Run()
    {
        float axis = Input.GetAxis("Horizontal");
        Vector3 direction = transform.right * axis;

        if ((transform.position.x >= -7.1 || axis > 0) && (transform.position.x <= 21.1 || axis < 0))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
        }

        sprite.flipX = direction.x < 0.0f;

        if (isGrounded)
        {
            State = CharState.Walk;
        }
    }

    private void Jump()
    {
        rigidbody.AddForce(new Vector2(0, 2500));
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);

        isGrounded = colliders.Length > 1;

        if (!isGrounded)
        {
            State = CharState.Jump;
        }
    }

    private void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.5f;

        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0f : 1.0f);

        isReloaded = false;
        StartCoroutine(Reloading());
    }

    public override void ReceiveDamage()
    {
        lives--;

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 14.0f, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.gameObject.GetComponent<Unit>();

        if(unit && !(unit is Box) && !isProtected)
        {
            ReceiveDamage();
        }
    }
}

public enum CharState
{
    Idle,
    Walk,
    Jump
}