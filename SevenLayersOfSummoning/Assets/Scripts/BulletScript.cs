using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public Type type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.tag != type.ToString()) {
			col.gameObject.SendMessage ("BulletHit");
		}
		Destroy (gameObject);
	}

	void BulletHit(){
	}	
}
