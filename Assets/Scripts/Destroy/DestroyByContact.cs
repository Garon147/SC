﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;

	private GameController gameController;
	private PlayerController player;

	public int scoreValue;

	void Start() 
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		GameObject playerObject = GameObject.FindWithTag ("Player");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<GameController> ();
			player = playerObject.GetComponent<PlayerController> ();
		} 
		else 
		{
			Debug.Log ("Can't find gamecontroller");
		}
	}


	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) { return; }

		if (explosion != null) 
		{
			Instantiate (explosion, transform.position, transform.rotation);
		}


		if (other.CompareTag("Player")) 
		{
			GameObject curPlayer = other.gameObject;
			if (curPlayer != null) {
				player = curPlayer.GetComponent<PlayerController> ();
			}

			if (player.currentHealth > 0) 
			{
				player.applyDamage (1);
				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
				Instantiate (curPlayer, transform.position, transform.rotation);
			} 
			else 
			{
				gameController.gameOverMethod ();
			}

		}

		gameController.addScore (scoreValue);

		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
