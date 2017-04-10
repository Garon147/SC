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

		if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) { return; }

		if (explosion != null) Instantiate (explosion, transform.position, transform.rotation);


		if (other.CompareTag("Player")) {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.gameOverMethod ();
		}

		gameController.addScore (scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
