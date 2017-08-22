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



	// Use this for initialization
	void Start () 
	{
		powerupManager = FindObjectOfType<PowerupManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag ("Player")) 
		{
			powerupManager.activatePowerup (isDoubleScore, isHealth, isFireRate, powerUpDuration);
		}

		gameObject.SetActive (false);
	}
}
