using UnityEngine;
using System.Collections;

public class DestroyInTime : MonoBehaviour {

	public float lifeTime;

	float startTime;

	void Start () {
		startTime = Time.time;
	}

	void Update () {
		if (startTime + lifeTime < Time.time) {
			Destroy(gameObject);
		}
	}
}
