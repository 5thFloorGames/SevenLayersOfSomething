using UnityEngine;
using System.Collections;

public class TriggerOnHIt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.tag != tag) {
			col.gameObject.SendMessage ("BulletHit");
		}
		Destroy (gameObject);
	}

	void BulletHit(){
	}
}
