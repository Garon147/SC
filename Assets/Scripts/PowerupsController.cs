using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsController : MonoBehaviour 
{

	public bool isDoubleScore;
	public bool isHealth;
	public bool isFireRate;

	public float powerUpDuration;

	private PowerupManager powerupManager;

	public float speed;

	void Start () 
	{
		powerupManager = FindObjectOfType<PowerupManager> ();
	}

	void Update () 
	{
		transform.Translate (0.0f, speed * Time.deltaTime, 0.0f);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			Physics.IgnoreCollision (this.GetComponent<Collider> (), other);
		}

		if (other.CompareTag ("Player")) 
		{
			powerupManager.activatePowerup (isDoubleScore, isHealth, isFireRate, powerUpDuration);
			gameObject.SetActive (false);
		}


	}
}
