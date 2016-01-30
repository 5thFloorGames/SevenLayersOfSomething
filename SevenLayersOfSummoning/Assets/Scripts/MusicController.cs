using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public AudioSource[] DemonLayers;
	private int demonIndex = 0;
	public AudioSource[] ShamanLayers;
	private int shamanIndex = 0;
	public AudioSource neutralLayer;
	private GameController gameController;
	private int lastLayer = 4;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateLayer(int newLayer){
		if (newLayer < lastLayer) {
			if(newLayer < 4){
				removeShamanLayer();
			} else if(newLayer > 4) {
				removeDemonLayer();
			} else {
				demonToNeutral ();
			}
		} else if (newLayer > lastLayer){
			if(newLayer > 4){
				addDemonLayer();
			} else if (newLayer < 4) {
				addShamanLayer();
			} else {
				shamanToNeutral ();
			}
		}
		lastLayer = newLayer;
	}

	IEnumerator fadeIn(AudioSource source){
		float volume = 0f;
		while (volume < 1f) {
			volume += 0.01f;
			source.volume = volume;
			yield return new WaitForSeconds(0.05f);
		}
	}

	IEnumerator fadeOut(AudioSource source){
		float volume = 1f;
		while (volume > 0f) {
			volume -= 0.01f;
			source.volume = volume;
			yield return new WaitForSeconds(0.05f);
		}	
	}

	void addShamanLayer(){
		shamanIndex++;
		fadeIn (ShamanLayers [shamanIndex]);
	}

	void addDemonLayer(){
		demonIndex++;
		fadeIn (DemonLayers [demonIndex]);
	}
	
	void removeShamanLayer(){
		shamanIndex--;
		fadeOut (ShamanLayers [shamanIndex]);
	}
	
	void removeDemonLayer(){
		demonIndex--;
		fadeOut (DemonLayers [demonIndex]);
	}

	void demonToNeutral(){
		removeDemonLayer ();
		fadeIn (neutralLayer);
	}

	void shamanToNeutral(){
		removeShamanLayer ();
		fadeIn (neutralLayer);
	}
}
