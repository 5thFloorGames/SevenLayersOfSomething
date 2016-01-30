using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	public int layer = 4;
	private MusicController music;
	private ComboCreator combo;
	private GameObject demon;
	private GameObject shaman;

	void Awake() {
		demon = GameObject.Find ("Demon");
		shaman = GameObject.Find ("Shaman");
	}

	// Use this for initialization
	void Start () {
		music = GetComponent<MusicController> ();
		combo = GetComponent<ComboCreator> ();
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
				layer = 4;
				music.playStinger("Shaman");
				music.resetMusic();
			}
		}
		if (tag == "Shaman") {
			layer++;
			if(layer == 8){
				print ("Demon wins!");
				layer = 4;
				music.playStinger("Demon");
				music.resetMusic();
			}
		}
		NewRound ();
	}

	void NewRound(){
		print ("Layer is: " + layer);
		music.updateLayer (layer);

		DestroyBullets ();

		demon.transform.position = new Vector3 (0f, -6.6f, 0f);
		shaman.transform.position = new Vector3 (0f, 3.5f, 0f);

		KeyCode[] pattern1 = combo.newPatternPlayer1();
		KeyCode[] pattern2 = combo.newPatternPlayer2();

		shaman.GetComponent<ShamanShooting> ().NewPattern(pattern1);
		shaman.GetComponent<PatternManager> ().NewPattern (pattern1);

		demon.GetComponent<DemonShooting> ().NewPattern(pattern2);
		demon.GetComponent<PatternManager> ().NewPattern (pattern2);


		// Update combos

		// Countdown
	}

	void DestroyBullets(){
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Bullet")){
			Destroy(g);
		}
	}
}
