using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PatternManager : MonoBehaviour {
	public Color completedColor;
	public Color activeColor;
	public Color futureColor;

	[SerializeField] Sprite square; // 0
	[SerializeField] Sprite cross;
	[SerializeField] Sprite circle; // 2
	[SerializeField] Sprite triangle; // 3

	KeyCode[] pattern;
	Transform[] shapeList;
	Dictionary<KeyCode, Sprite> matching;

	int activeInt;

	public void NewPattern(KeyCode[] newPattern) {
		activeInt = 0;
		pattern = newPattern;
		ShowPattern ();
		RefreshPattern ();
	}

	public void CorrectButtonPressed() {
		activeInt++;
		RefreshPattern ();
	}

	public void WrongButtonPressed() {
		activeInt--;
		if (activeInt < 0) {
			activeInt = 0;
		}
		RefreshPattern ();
	}
	
	public void ShotFired() {
		activeInt = 0;
		RefreshPattern ();
	}

	void ShowPattern() {
		int i = 0;
		foreach (KeyCode kc in pattern) {
			shapeList[i].GetComponent<Image>().sprite = GetShape(kc);
			i++;
		}
	}

	void RefreshPattern() {
		int i = 0;
		foreach (Transform shape in shapeList) {
			if (i < activeInt) {
				shape.GetComponent<Image>().color = completedColor;
				shape.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 20);
			} else if (i == activeInt) {
				shape.GetComponent<Image>().color = activeColor;
				shape.GetComponent<RectTransform>().sizeDelta = new Vector2(30, 30);
			} else {
				shape.GetComponent<Image>().color = futureColor;
				shape.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 20);
			}
			i++;
		}
	}

	void AwakingAssignments() {
		matching = new Dictionary<KeyCode, Sprite> {
			{KeyCode.Joystick1Button0, square},
			{KeyCode.Joystick2Button0, square},
			{KeyCode.Joystick1Button1, cross},
			{KeyCode.Joystick2Button1, cross},
			{KeyCode.Joystick1Button2, circle},
			{KeyCode.Joystick2Button2, circle},
			{KeyCode.Joystick1Button3, triangle},
			{KeyCode.Joystick2Button3, triangle}
		};
		shapeList = new Transform[4];
		int i = 0;
		foreach (Transform shape in transform.FindChild("Patterns")) {
			shapeList[i] = shape;
			shape.GetComponent<Image> ().sprite = cross;
			shape.GetComponent<RectTransform>().sizeDelta = new Vector2(20, 20);
			shape.GetComponent<Image>().color = futureColor;
			i++;
		}
		activeInt = 0;
	}

	Sprite GetShape(KeyCode kc) {
		if (matching.ContainsKey (kc)) {
			return matching [kc];
		} else {
			print("WARNING: Searched for non-existing keys from matching!");
			return null;
		}
	}

	void Awake () {
		AwakingAssignments ();
	}

	void Update () {
		
	}
}
