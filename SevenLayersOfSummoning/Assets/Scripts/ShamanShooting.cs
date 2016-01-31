using UnityEngine;
using System.Collections;

public class ShamanShooting : MonoBehaviour {

	public GameObject bullet;
	private KeyCode[] pattern;
	private int comboPhase = 0;
	private PatternManager patternManager;
	private AudioClip[] buttons;
	private AudioClip[] wrongs;
	private AudioClip[] shots;
	private AudioSource audio;
	private bool blocked = false;
	
	void Start () {
		patternManager = GetComponent<PatternManager> ();
		buttons = Resources.LoadAll<AudioClip>("Audio/Shaman/Button");
		wrongs = Resources.LoadAll<AudioClip>("Audio/Shaman/Wrong");
		shots = Resources.LoadAll<AudioClip>("Audio/Shaman/Shoot");
		audio = GetComponent<AudioSource> ();
	}

	void Update () {
		if (!blocked) {
			if (Input.GetKeyDown ((pattern [comboPhase]))) {
				audio.PlayOneShot (buttons [Random.Range (0, buttons.Length)]);
				patternManager.CorrectButtonPressed ();
				comboPhase++;
			} else if (JoystickKeyPressed ()) {
				audio.PlayOneShot (wrongs [Random.Range (0, wrongs.Length)]);
				patternManager.WrongButtonPressed ();
				comboPhase--;
				if (comboPhase < 0) {
					comboPhase = 0;
				}
			}
			if (comboPhase == 4 || Input.GetButtonDown ("Fire1")) {
				audio.PlayOneShot (shots [Random.Range (0, shots.Length)]);
				GameObject firedBullet = (GameObject)Instantiate (bullet, transform.position, transform.rotation);
				Rigidbody2D bulletrgb = firedBullet.GetComponent<Rigidbody2D> ();
				bulletrgb.AddForce (transform.up * (-1) * 600f);
				Physics2D.IgnoreCollision (firedBullet.GetComponent<Collider2D> (), GetComponent<Collider2D> ());
				comboPhase = 0;
				patternManager.ShotFired ();
			}
		}
	}

	public bool JoystickKeyPressed(){
		return Input.GetKeyDown(KeyCode.Joystick1Button0)
			|| Input.GetKeyDown (KeyCode.Joystick1Button1)
			|| Input.GetKeyDown (KeyCode.Joystick1Button2)
			|| Input.GetKeyDown (KeyCode.Joystick1Button3);
	}
	
	public void NewPattern(KeyCode[] newPattern){
		comboPhase = 0;
		pattern = newPattern;
	}

	public void Block(){
		blocked = true;
	}
	
	public void Unblock(){
		blocked = false;
	}
}
