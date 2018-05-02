using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Unit {
    private Live live;
    private Shield shield;

    private void Awake()
    {
        live = Resources.Load<Live>("LiveObject");
        shield = Resources.Load<Shield>("Shield");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();

        int randomNumber = Mathf.RoundToInt(Random.Range(0.0f, 1.0f));

        if (randomNumber == 0)
        {
            Instantiate(live, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(shield, transform.position, transform.rotation);
        }

        ReceiveDamage();
    }
}
