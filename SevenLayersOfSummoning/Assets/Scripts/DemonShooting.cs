using UnityEngine;
using System.Collections;

public class DemonShooting : MonoBehaviour {

	public GameObject bullet;
	private KeyCode[] pattern = {KeyCode.Joystick2Button0, KeyCode.Joystick2Button1, KeyCode.Joystick2Button2, KeyCode.Joystick2Button3};
	private int comboPhase = 0;
	private PatternManager patternManager;

	void Start () {
		patternManager = GetComponent<PatternManager> ();
	}

	void Update () {
		if (Input.GetKeyDown(pattern [comboPhase])) {
			// correct button pressed
			patternManager.CorrectButtonPressed();
			comboPhase++;
		}
		if (comboPhase == 4) {
			GameObject firedBullet = (GameObject)Instantiate (bullet, transform.position, transform.rotation);
			Rigidbody2D bulletrb = firedBullet.GetComponent<Rigidbody2D> ();
			bulletrb.AddForce (transform.up * 200f);
			Physics2D.IgnoreCollision (firedBullet.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
			comboPhase = 0;
			patternManager.ShotFired();
		}
	}

	public void NewPattern(KeyCode[] newPattern){
		pattern = newPattern;
	}
}
