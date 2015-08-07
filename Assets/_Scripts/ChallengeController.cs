using UnityEngine;
using System.Collections;

public class ChallengeController : MonoBehaviour {

	private CardboardHead head;
	private Vector3 startingPosition;
	private Color color;
	
	public AudioSource select;
	private bool soundPlayed = false;

	public GameObject mainMenu;
	public GameObject challengeMenu;

	// Use this for initialization
	void Start () {
		head = Camera.main.GetComponent<StereoController>().Head;
		startingPosition = transform.localPosition;
		CardboardGUI.IsGUIVisible = false;
		color = new Color (1, 1, 1, 1);
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		RaycastHit hit;
		bool isLookedAt = GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);
		if (isLookedAt) {
			if (color.a > 0) {
				color.a -= .015F;
			}
			else if (!soundPlayed) {
				select.Play ();
				soundPlayed = true;
				selectMenu();
			}
		} else {
			color.a = 1;
			soundPlayed = false;
		}
		GetComponent<Renderer> ().material.color = color;
	}

	void selectMenu() {
		challengeMenu.SetActive (false);
		chooseNextMenu ();
	}

	void chooseNextMenu() {
		mainMenu.SetActive (true);
	}
}
