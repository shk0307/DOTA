﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMonsterScript : MonoBehaviour {
    public GameObject target;
    private float speed = 25f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.localPosition += transform.forward * -10f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.localScale.y, target.transform.position.z), speed * Time.deltaTime);
        if (transform.localPosition.x == target.transform.position.x && transform.localPosition.z == target.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }
}
