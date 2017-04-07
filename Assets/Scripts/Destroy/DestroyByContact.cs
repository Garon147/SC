using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;

	private GameController gameController;

	public int scoreValue;

	void Start() {

		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");

		if (gameControllerObject != null) {

			gameController = gameControllerObject.GetComponent<GameController> ();
		} else {
			Debug.Log ("Can't find gamecontroller");
		}
	}

	void OnTriggerEnter(Collider other) {

		if (other.tag == "Boundary") { return; }
		Instantiate (explosion, transform.position, transform.rotation);

		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.gameOverMethod ();
		}

		gameController.addScore (scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
