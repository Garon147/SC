using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour 
{

	private bool isDoubleScore;
	private bool isHealth;
	private bool isFireRate;

	private bool isPowerupActive;

	private float powerUpDurationCounter;

	private GameController gameController;

	// Use this for initialization
	void Start () 
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isPowerupActive) 
		{
			powerUpDurationCounter -= Time.deltaTime;
			if (isDoubleScore) 
			{
				gameController.isDoubleScore = true;
				gameController.restartText.text = "X2 BONUS!!!!!";
			}
			if (powerUpDurationCounter <= 0) 
			{
				isPowerupActive = false;
				gameController.isDoubleScore = false;
				gameController.restartText.text = "";
			}
		}
		
	}

	public void activatePowerup(bool _isDoubleScore, bool _isHealth, bool _isFireRate, float _duration)
	{
		isDoubleScore = _isDoubleScore;
		isHealth = _isHealth;
		isFireRate = _isFireRate;
		powerUpDurationCounter = _duration;

		isPowerupActive = true;
	}
}
