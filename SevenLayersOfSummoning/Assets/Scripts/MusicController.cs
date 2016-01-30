using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public AudioSource[] demonLayers;
	private int demonIndex = -1;
	public AudioSource[] shamanLayers;
	private int shamanIndex = -1;
	public AudioSource neutralLayer;
	private GameController gameController;
	private int lastLayer = -1;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateLayer(int newLayer){
		if (lastLayer == -1) {
			lastLayer = newLayer;
			return;
		}
		if (newLayer < lastLayer) {
			if(newLayer < 4){
				addShamanLayer();
			} else if(newLayer > 4) {
				removeDemonLayer();
			} else {
				demonToNeutral ();
			}
		} else if (newLayer > lastLayer){
			if(newLayer > 4){
				addDemonLayer();
			} else if (newLayer < 4) {
				removeShamanLayer();
			} else {
				shamanToNeutral ();
			}
		}
		if (lastLayer == 4) {
			StartCoroutine(fadeOut(neutralLayer));
		}
		lastLayer = newLayer;
	}

	IEnumerator fadeIn(AudioSource source){
		float volume = 0f;
		while (volume < 1f) {
			volume += 0.01f;
			source.volume = volume;
			//print(source.time);
			yield return new WaitForSeconds(0.00927f);
		}
	}

	IEnumerator fadeOut(AudioSource source){
		float volume = 1f;
		while (volume > 0f) {
			volume -= 0.01f;
			source.volume = volume;
			yield return new WaitForSeconds(0.00927f);
		}	
	}

	void addShamanLayer(){
		shamanIndex++;
		if (shamanLayers.Length <= shamanIndex) {
			return;
		}
		StartCoroutine(fadeIn (shamanLayers [shamanIndex]));
	}

	void addDemonLayer(){
		demonIndex++;
		if (demonLayers.Length <= demonIndex) {
			return;
		}
		StartCoroutine(fadeIn (demonLayers [demonIndex]));
	}
	
	void removeShamanLayer(){
		StartCoroutine(fadeOut (shamanLayers [shamanIndex]));
		shamanIndex--;
	}
	
	void removeDemonLayer(){
		StartCoroutine(fadeOut (demonLayers [demonIndex]));
		demonIndex--;
	}

	void demonToNeutral(){
		removeDemonLayer ();
		StartCoroutine(fadeIn (neutralLayer));
	}

	void shamanToNeutral(){
		removeShamanLayer ();
		StartCoroutine(fadeIn (neutralLayer));
	}
}
