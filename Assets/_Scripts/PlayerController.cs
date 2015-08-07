using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private int lastPoint;

	public GameObject winText;
	public GameObject loseText;
	public GameObject startText;
	public GameObject endText;

	private bool hasWon = false;
	private bool hasLost = false;

	public AudioSource loseSound;
	public AudioSource winSound;
	private bool soundHasPlayed = false;

	private float timer = 5;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		lastPoint = 0;
	}

	void FixedUpdate() {
		if (soundHasPlayed) {
			if (timer > 1) {
				timer -= Time.deltaTime;
				return;
			} else Application.LoadLevel("Main Menu");
		} 
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "StartPoint") {
			lastPoint = 1;
		} else if ((other.gameObject.tag == "MidPoint") && (lastPoint == 1)) {
			lastPoint = 2;
		} else if ((other.gameObject.tag == "EndPoint") && (lastPoint == 2)) {
			if (lastPoint == 2) {
				lastPoint = 3;
				hasWon = true;
				if (!hasLost) {
					setWinTexts();
					if (!soundHasPlayed) {
						winSound.Play();
						soundHasPlayed = true;
						PlayerPrefs.SetFloat(Application.loadedLevelName,1);
					}
				}
			} else {
				lastPoint = 0;
			}
		} else if (other.gameObject.tag == "Wire") {
			hasLost = true;
			if (!hasWon) {
				setLoseTexts();
				if (!soundHasPlayed) {
					loseSound.Play();
					soundHasPlayed = true;
				}
			}
		}
	}

	void setWinTexts() {
		winText.SetActive(true);
		startText.SetActive(false);
		endText.SetActive(false);
	}


	void setLoseTexts() {
		loseText.SetActive(true);
		startText.SetActive(false);
		endText.SetActive(false);
	}
}
