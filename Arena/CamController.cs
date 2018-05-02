﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {
    [SerializeField]
    private float speed = 2.0f;

    private Transform target;

	void Awake () {
        target = FindObjectOfType<Character>().transform;
	}	

	void Update () {
        Vector3 position = target.position;
        position.z = -10.0f;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
	}
}
