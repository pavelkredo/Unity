using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    private bool isSpawned = false;
    private Box box;
    private MoveableMonster moveableMonster;
    private MoveableMonster moveableMonsterResource;

    // Use this for initialization
    void Start () {
        box = Resources.Load<Box>("Box");
        moveableMonster = FindObjectOfType<MoveableMonster>();
        moveableMonsterResource = Resources.Load<MoveableMonster>("MoveableMonster");
    }
	
	// Update is called once per frame
	void Update () {
        if (moveableMonster == null)
        {
            Vector3 position = new Vector3(Random.Range(-7.1f, 21.1f), Random.Range(0.0f, 11.0f));
            Instantiate(moveableMonsterResource, position, transform.rotation);
            moveableMonster = FindObjectOfType<MoveableMonster>();
        }

        if (!isSpawned)
        {
            StartCoroutine(RespawnBox());
            isSpawned = true;
        }
	}

    IEnumerator RespawnBox()
    {
        yield return new WaitForSeconds(15);
        Vector3 position = new Vector3(Random.Range(-7.1f, 21.1f), 10);
        Instantiate(box, position, transform.rotation);
        isSpawned = false;
    }

    IEnumerator RespawnMonster()
    {
        yield return new WaitForSeconds(25);
    }
}
