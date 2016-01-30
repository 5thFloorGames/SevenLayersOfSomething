using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	private Rigidbody2D rigid;
	public float moveSpeed = 1;
	public int player = 1;
	private float maxXposition = 9f;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		float input = Input.GetAxis("MovementPlayer" + player);
		Vector2 newPosition = transform.position + Vector3.right * moveSpeed * input * Time.deltaTime;
		newPosition.x = Mathf.Clamp (newPosition.x, -maxXposition, maxXposition);
		transform.position = newPosition;
	}

	public void BulletHit(){
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().registerHit (tag);
	}
}
