using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	private CardboardHead head;
	private Vector3 startingPosition;
	private Color color;
	public AudioSource select;
	private bool soundPlayed = false;

	public GameObject mainMenu;
	public GameObject normalMenu;
	public GameObject challengeMenu;
	public GameObject credits;
	
	void Start() {
		head = Camera.main.GetComponent<StereoController>().Head;
		startingPosition = transform.localPosition;
		CardboardGUI.IsGUIVisible = true;
		CardboardGUI.onGUICallback += this.OnGUI;
		color = new Color (1, 1, 1, 1);
	}
	
	void FixedUpdate() {
		RaycastHit hit;
		bool isLookedAt = GetComponent<Collider>().Raycast(head.Gaze, out hit, Mathf.Infinity);
//		GetComponent<Renderer> ().material.color = isLookedAt ? Color.magenta : Color.white;
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
		mainMenu.SetActive(false);
		if (this.gameObject.name == "Normal Mode") {
			normalMenu.SetActive(true);
		} else if (this.gameObject.name == "Challenge Mode") {
			challengeMenu.SetActive(true);
		} else if (this.gameObject.name == "Credits"){
			credits.SetActive(true);
		}
	}
	
	void OnGUI() {

	}
}
