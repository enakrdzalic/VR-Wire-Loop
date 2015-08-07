using UnityEngine;
using System.Collections;

public class PromptController : MonoBehaviour {

	public GameObject normalMenu;
	public GameObject promptMenu;
	public string level;

	private CardboardHead head;
	private Vector3 startingPosition;
	private Color color;
	
	public AudioSource select;
	private bool soundPlayed = false;

	public NormalMenuController normalMenuController;

	public string levelSelected = null;

	// Use this for initialization
	void Start () {
		head = Camera.main.GetComponent<StereoController>().Head;
		startingPosition = transform.localPosition;
		CardboardGUI.IsGUIVisible = false;
		color = new Color (1, 1, 1, 1);	
	}

	// Update is called once per frame
	void FixedUpdate () {
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
		//promptMenu.SetActive (false);
		yesOrNo ();
	}

	void yesOrNo() {
		if (this.gameObject.name == "Yes") {
			Application.LoadLevel (levelSelected);
		} else {
			promptMenu.SetActive(false);
			normalMenu.SetActive (true);
		}
	}
}
