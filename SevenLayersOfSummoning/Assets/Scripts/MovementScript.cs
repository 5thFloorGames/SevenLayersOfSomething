using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	private Rigidbody2D rigid;
	public float moveSpeed = 1;
	public int player = 1;
	private float maxXposition = 10f;
	private AudioClip[] injuries;
	private AudioClip[] deaths;
	private AudioSource audioSource;
	private bool blocked = false;
	private Animator animator;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		injuries = Resources.LoadAll<AudioClip> ("Audio/" + tag +"/Injury");
		deaths = Resources.LoadAll<AudioClip> ("Audio/" + tag +"/Death");
		animator = GetComponentInChildren<Animator> ();

		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!blocked) {
			float input = Input.GetAxis ("MovementPlayer" + player);
			if(Mathf.Abs(Input.GetAxis ("KeyboardMove" + player)) > 0.1f){
				input = Input.GetAxis ("KeyboardMove" + player);
			}
			if(Mathf.Abs(Input.GetAxis ("MovementPlayer" + player)) > 0.1f){
				input = Input.GetAxis ("MovementPlayer" + player);
			}

			if(Mathf.Abs(input) > 0.1f){
				if(input > 0){
					if(tag == "Demon"){
						transform.rotation = new Quaternion(0f,180f,0f,0f);
					} else {
						transform.rotation = new Quaternion(0f,0f,0f,0f);
					}
				} else {
					if(tag == "Shaman"){
						transform.rotation = new Quaternion(0f,180f,0f,0f);
					} else {
						transform.rotation = new Quaternion(0f,0f,0f,0f);
					}
				}
				if(animator != null){
					animator.SetBool("running", true);
				}
			} else {
				if(animator != null){
					animator.SetBool("running", false);
				}
			}
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
		transform.rotation = new Quaternion(0f,0f,0f,270f);
		if(animator != null){
			animator.SetBool("running", false);
		}
	}

	public void Unblock(){
		blocked = false;
	}

	public void Die(){
		audioSource.PlayOneShot (deaths [Random.Range (0, deaths.Length)],1f);
	}

	public void StopAnimation(){
		if (animator != null) {
			animator.Stop();
		}
	}

	public void StartAnimation(){
		if (animator != null) {
			animator.StartPlayback();
		}
	}
}
