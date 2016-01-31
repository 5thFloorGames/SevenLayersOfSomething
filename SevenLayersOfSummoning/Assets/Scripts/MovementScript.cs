using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	private Rigidbody2D rigid;
	public float moveSpeed = 1;
	public int player = 1;
	private float maxXposition = 10f;
	private AudioClip[] injuries;
	private AudioSource audioSource;
	private bool blocked = false;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		if (tag == "Demon") {
			injuries = Resources.LoadAll<AudioClip> ("Audio/Demon/Injury");
		} else {
			injuries = Resources.LoadAll<AudioClip> ("Audio/Shaman/Injury");
		}
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!blocked) {
			float input = Input.GetAxis ("MovementPlayer" + player);
			Vector2 newPosition = transform.position + Vector3.right * moveSpeed * input * Time.deltaTime;
			newPosition.x = Mathf.Clamp (newPosition.x, -maxXposition, maxXposition);
			transform.position = newPosition;
		}
	}

	public void BulletHit(){
		audioSource.PlayOneShot (injuries [Random.Range (0, injuries.Length)]);
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().registerHit (tag);
	}

	public void Block(){
		blocked = true;
	}

	public void Unblock(){
		blocked = false;
	}
}
