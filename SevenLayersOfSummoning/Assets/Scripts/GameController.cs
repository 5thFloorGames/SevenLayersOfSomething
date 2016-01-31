using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	private int layer = 4;
	private MusicController music;
	private ComboCreator combo;
	private GameObject demon;
	private GameObject shaman;
	public GameObject shamanPatterns;
	public GameObject demonPatterns;
	public GameObject hell;
	private ScreenShake screenshake;

	void Awake() {
		demon = GameObject.Find ("Demon");
		shaman = GameObject.Find ("Shaman");
		screenshake = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<ScreenShake> ();
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
		screenshake.jiggleCam (0.15f, 0.125f);
		if (tag == "Demon") {
			layer--;
			if(layer == 0){
				StartCoroutine(ShamanWin());
			} else {
				hell.transform.position += new Vector3(0f, 0.35f,0f);
				NewRound();
			}
		}
		if (tag == "Shaman") {
			layer++;
			if(layer == 8){
				StartCoroutine(DemonWin());
			} else {
				hell.transform.position -= new Vector3(0f, 0.35f,0f);
				NewRound ();
			}
		}
	}

	IEnumerator ShamanWin(){
		DestroyBullets ();
		BlockPlayers ();
		music.Quiet ();
		demon.transform.position = new Vector3 (shaman.transform.position.x + 3f, 3.3f,0f);

		music.playStinger("Shaman");
		yield return new WaitForSeconds (3f);
		demon.SendMessage("Die");
		yield return new WaitForSeconds (3f);
		ResetGame ();
	}

	IEnumerator DemonWin(){
		DestroyBullets ();
		BlockPlayers ();
		music.Quiet ();
		yield return new WaitForSeconds (1f);
		shaman.transform.position = new Vector3 (shaman.transform.position.x, 3.5f,0f);
		shaman.transform.Rotate (0f, 0f, -90f);
		shaman.SendMessage("StopAnimation");
		shaman.SendMessage("Die");
		yield return new WaitForSeconds (1.5f);

		music.playStinger("Demon");
		yield return new WaitForSeconds (5f);
		ResetGame ();
	}

	void BlockPlayers(){
		shamanPatterns.SetActive (false);
		demonPatterns.SetActive (false);

		demon.SendMessage("Block");
		shaman.SendMessage("Block");
	}

	void UnblockPlayers(){
		shamanPatterns.SetActive (true);
		demonPatterns.SetActive (true);
		
		demon.SendMessage("Unblock");
		shaman.SendMessage("Unblock");
	}

	void ResetGame(){
		UnblockPlayers ();
		shaman.SendMessage("StartAnimation");
		music.resetMusic();
		layer = 4;
		hell.transform.position = new Vector3(0f,-4.7f,0f);
		NewRound ();
	}

	void NewRound(){
		print ("Layer is: " + layer);
		music.updateLayer (layer);

		DestroyBullets ();

		shaman.transform.rotation = Quaternion.identity;

		demon.transform.position = new Vector3 (4f, -6.6f, 0f);
		shaman.transform.position = new Vector3 (-4f, 3.9f, 0f);

		KeyCode[] pattern1 = combo.newPatternPlayer1();
		KeyCode[] pattern2 = combo.newPatternPlayer2();

		shaman.GetComponent<ShamanShooting> ().NewPattern(pattern1);
		shaman.GetComponent<PatternManager> ().NewPattern (pattern1);

		demon.GetComponent<DemonShooting> ().NewPattern(pattern2);
		demon.GetComponent<PatternManager> ().NewPattern (pattern2);
	}

	void DestroyBullets(){
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Bullet")){
			Destroy(g);
		}
	}
}
