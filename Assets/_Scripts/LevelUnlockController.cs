using UnityEngine;
using System.Collections;

public class LevelUnlockController : MonoBehaviour {

	public string previousLevel;
	public Material notUnlocked;

	// Use this for initialization
	void Start () {
		if (previousLevelFinished() || isLevel1()) {

		} else {
			this.gameObject.GetComponent<BoxCollider>().enabled = false;
			this.gameObject.GetComponent<TextMesh>().color = new Color(255,255,255,255);
			GameObject box = GameObject.Find(this.gameObject.name + "B");
			box.GetComponent<Renderer>().material = notUnlocked;
		}
	}

	bool previousLevelFinished() {
		return (PlayerPrefs.GetFloat (previousLevel) == 1);
	}

	bool isLevel1() {
		return (this.gameObject.name == "Level 1");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
