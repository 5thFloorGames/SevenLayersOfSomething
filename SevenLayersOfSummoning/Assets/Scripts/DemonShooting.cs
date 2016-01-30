﻿using UnityEngine;
using System.Collections;

public class DemonShooting : MonoBehaviour {

	public GameObject bullet;
	private KeyCode[] pattern = {KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V};
	private int comboPhase = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(pattern [comboPhase])) {
			comboPhase++;
		}
		if (comboPhase == 4) {
			GameObject firedBullet = (GameObject)Instantiate (bullet, transform.position, transform.rotation);
			Rigidbody2D bulletrb = firedBullet.GetComponent<Rigidbody2D> ();
			bulletrb.AddForce (transform.up * 200f);
			Physics2D.IgnoreCollision (firedBullet.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
			comboPhase = 0;
		}
	}
}