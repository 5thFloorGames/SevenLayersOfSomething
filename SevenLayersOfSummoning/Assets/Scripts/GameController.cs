﻿using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public int layer = 4;
	private MusicController music;
	private ComboCreator combo;
	private GameObject demon;
	private GameObject shaman;

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

		KeyCode[] pattern1 = combo.newPatternPlayer1();
		KeyCode[] pattern2 = combo.newPatternPlayer2();

		shaman.GetComponent<ShamanShooting> ().newPattern(pattern1);
		demon.GetComponent<DemonShooting> ().newPattern(pattern2);
		
		demon.GetComponent<PatternManager> ().NewPattern (pattern2);
		shaman.GetComponent<PatternManager> ().NewPattern (pattern1);

		// Update combos

		// Countdown
	}

	void DestroyBullets(){
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Bullet")){
			Destroy(g);
		}
	}
}
