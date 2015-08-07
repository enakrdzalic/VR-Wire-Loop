using UnityEngine;
using System.Collections;

public class NormalMenuController : MonoBehaviour {
	private CardboardHead head;
	private Vector3 startingPosition;
	private Color color;

	public AudioSource select;
	private bool soundPlayed = false;

	public GameObject normalMenu;
	public GameObject promptMenu;

	public GameObject mainMenu;
	public GameObject htpMenu;

	public GameObject levelSelected = null;
	public GameObject promptText;
	public GameObject yesButton;

	void Start() {
		head = Camera.main.GetComponent<StereoController>().Head;
		startingPosition = transform.localPosition;
		CardboardGUI.IsGUIVisible = false;
		color = new Color (1, 1, 1, 1);
	}

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
		normalMenu.SetActive (false);
		chooseNextMenu ();
	}

	void chooseNextMenu() {
		string name = this.gameObject.name;
		if (name == "HTP") {
			htpMenu.SetActive(true);
		} else if (name == "Main Menu") {
			mainMenu.SetActive(true);
		} else {
			levelSelected = this.gameObject;
			doYouWantToPlay();
		}
	}

	void doYouWantToPlay(){
		promptText.GetComponent<TextMesh> ().text = "START " + levelSelected.name.ToUpper() + "?";
		yesButton.GetComponent<PromptController> ().levelSelected = levelSelected.name;
		promptMenu.SetActive (true);
	}

	void OnGUI() {

	}
}
