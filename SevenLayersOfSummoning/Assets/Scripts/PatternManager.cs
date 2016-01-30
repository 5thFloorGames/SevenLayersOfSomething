using UnityEngine;
using System.Collections;

public class PatternManager : MonoBehaviour {
	public Color completedColor;
	public Color activeColor;
	public Color futureColor;

	KeyCode[] pattern;
	GameObject[] shapeList;

	public void NewPattern(KeyCode[] newPattern) {
		pattern = newPattern;
		ResetPattern ();
	}

	void ResetPattern() {

	}

	void Awake () {
		
	}

	void Update () {
		
	}
}
