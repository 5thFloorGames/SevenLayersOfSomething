using UnityEngine;
using System.Collections;

public class ComboCreator : MonoBehaviour {

	private KeyCode[] pattern1 = {KeyCode.Joystick1Button0, KeyCode.Joystick1Button1, KeyCode.Joystick1Button2, KeyCode.Joystick1Button3};
	private KeyCode[] pattern2 = {KeyCode.Joystick2Button0, KeyCode.Joystick2Button1, KeyCode.Joystick2Button2, KeyCode.Joystick2Button3};

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public KeyCode[] newPatternPlayer1(){
		KeyCode[] pattern = new KeyCode[4];
		for (int i = 0; i < 4; i++) {
			pattern[i] = pattern1[Random.Range(0,4)];
		}
		return pattern;
	}

	public KeyCode[] newPatternPlayer2(){
		KeyCode[] pattern = new KeyCode[4];
		for (int i = 0; i < 4; i++) {
			pattern[i] = pattern2[Random.Range(0,4)];
		}
		return pattern;
	}

	//Square  = joystick button 0
	//	X       = joystick button 1
	//		Circle  = joystick button 2
	//		Triangle= joystick button 3
}
