using UnityEngine;
using System.Collections;

public class ShamanShooting : MonoBehaviour {

	public GameObject bullet;
	private KeyCode[] pattern;
	private int comboPhase = 0;
	private PatternManager patternManager;
	
	void Start () {
		patternManager = GetComponent<PatternManager> ();
	}

	void Update () {
		if (Input.GetKeyDown((pattern [comboPhase]))) {
			// correct button pressed
			patternManager.CorrectButtonPressed();
			comboPhase++;
		}
		if (comboPhase == 4) {
			GameObject firedBullet = (GameObject)Instantiate (bullet, transform.position, transform.rotation);
			Rigidbody2D bulletrb = firedBullet.GetComponent<Rigidbody2D> ();
			bulletrb.AddForce (transform.up * (-1) * 200f);
			Physics2D.IgnoreCollision(firedBullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			comboPhase = 0;
			patternManager.ShotFired();
		}
	}

	
	public void NewPattern(KeyCode[] newPattern){
		pattern = newPattern;
	}
}
