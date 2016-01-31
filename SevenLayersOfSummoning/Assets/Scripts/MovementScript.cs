using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	private Rigidbody2D rigid;
	public float moveSpeed = 1;
	public int player = 1;
	private float maxXposition = 10f;
	private AudioClip[] injuries;
	private AudioClip[] deaths;
	private AudioClip[] moves;
	public AudioSource audioSource;
	public AudioSource walkSound;
	private bool blocked = false;
	private Animator animator;
	private Transform spriteTransform;
	
	void Start () {
		spriteTransform = transform.FindChild ("Sprite").transform;
		rigid = GetComponent<Rigidbody2D> ();
		injuries = Resources.LoadAll<AudioClip> ("Audio/" + tag +"/Injury");
		deaths = Resources.LoadAll<AudioClip> ("Audio/" + tag +"/Death");
		moves = Resources.LoadAll<AudioClip> ("Audio/" + tag +"/Move");
		animator = GetComponentInChildren<Animator> ();
	}

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
				if(!walkSound.isPlaying){
					walkSound.clip = moves[Random.Range(0,moves.Length)];
					walkSound.Play ();
				}

				if(input > 0){
					if(tag == "Demon"){
						spriteTransform.rotation = new Quaternion(0f,180f,0f,0f);
					} else {
						spriteTransform.rotation = new Quaternion(0f,0f,0f,0f);
					}
				} else {
					if(tag == "Shaman"){
						spriteTransform.rotation = new Quaternion(0f,180f,0f,0f);
					} else {
						spriteTransform.rotation = new Quaternion(0f,0f,0f,0f);
					}
				}
				animator.SetBool("running", true);
			} else {
				animator.SetBool("running", false);
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
		spriteTransform.rotation = new Quaternion(0f,0f,0f,270f);
		animator.SetBool("running", false);
	}

	public void Unblock(){
		blocked = false;
	}

	public void Die(){
		audioSource.PlayOneShot (deaths [Random.Range (0, deaths.Length)],1f);
	}

	public void StopAnimation(){
		animator.enabled = false;
	}

	public void StartAnimation(){
		animator.enabled = true;
	}
}
