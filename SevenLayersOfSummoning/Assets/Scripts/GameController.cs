using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public int layer = 4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void registerHit(string tag){
		if (tag == "Demon") {
			layer--;
			if(layer == 0){
				print ("Demon wins!");
				Application.Quit();
			}
		}
		if (tag == "Shaman") {
			layer++;
			if(layer == 8){
				print ("Shaman wins!");
				Application.Quit();
			}
		}
		NewRound ();
	}

	void NewRound(){
		print ("Layer is: " + layer);

		// Destroy all bullets

		// Reset player positions

		// Update combos

		// Countdown
	}
}
