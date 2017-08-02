using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayerByContact: MonoBehaviour
{
	public GameObject playerExplosion;

	private GameController gameController;
	private PlayerController player;

	void Start() 
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		GameObject playerObject = GameObject.FindWithTag ("Player");


		if (gameControllerObject != null && playerObject != null) 
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
		if (other.CompareTag("Player")) 
		{
			if (player.currentHealth > 0) 
			{
				player.currentHealth--;
			} 
			else 
			{
				Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
				gameController.gameOverMethod ();
				Destroy (other.gameObject);
				Destroy (gameObject);
			}

		}
	}

}



