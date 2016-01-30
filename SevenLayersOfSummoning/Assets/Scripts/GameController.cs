using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public int layer = 4;
	private MusicController music;

	// Use this for initialization
	void Start () {
		music = GetComponent<MusicController> ();
		NewRound ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void registerHit(string tag){
		if (tag == "Demon") {
			layer--;
			if(layer == 0){
				print ("Shaman wins!");
				Application.Quit();
			}
		}
		if (tag == "Shaman") {
			layer++;
			if(layer == 8){
				print ("Demon wins!");
				Application.Quit();
			}
		}
		NewRound ();
	}

	void NewRound(){
		print ("Layer is: " + layer);
		music.updateLayer (layer);

		DestroyBullets ();

		// Reset player positions

		// Update combos

		// Countdown
	}

	void DestroyBullets(){
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Bullet")){
			Destroy(g);
		}
	}
}
