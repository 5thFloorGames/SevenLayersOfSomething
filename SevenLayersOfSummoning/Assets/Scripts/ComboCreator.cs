using UnityEngine;
using System.Collections;

public class ComboCreator : MonoBehaviour {

	private KeyCode[] pattern = {KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V};

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		KeyCode[] pattern = newPattern();
		string toPrint = "";
		for (int i = 0; i < 4; i++) {
			toPrint += pattern[i] + ", ";
		}
		print (toPrint);
	}

	public KeyCode[] newPattern(){
		KeyCode[] pattern = {KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V};
		for (int i = 0; i < 4; i++) {
			pattern[i] = pattern[Random.Range(0,4)];
		}
		return pattern;
	}
}
