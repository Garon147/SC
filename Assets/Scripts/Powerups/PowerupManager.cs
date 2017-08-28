using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour 
{

	private bool isDoubleScore;
	private bool isHealth;
	private bool isFireRate;

	private bool isPowerupActive;
	private bool isPowerupScoreActive;
	private bool isPowerupFireActive;

	private float powerUpDurationCounter;
	private float powerUpDurationCounterFire;

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
			
			if (isDoubleScore) 
			{
				powerUpDurationCounter -= Time.deltaTime;
				gameController.isDoubleScore = true;
				gameController.doubleScoreText.text = "DOUBLE POINTS BONUS!";
			}

			if (isFireRate) 
			{
				powerUpDurationCounterFire -= Time.deltaTime;
				gameController.isFireRate = true;
				gameController.fireRateText.text = "MORE FIREPOWER BONUS!";
			}

			if (powerUpDurationCounter <= 0) 
			{
				isDoubleScore = false;
				gameController.isDoubleScore = false;
				gameController.doubleScoreText.text = "";
			}

			if (powerUpDurationCounterFire <= 0) 
			{
				isFireRate = false;
				gameController.isFireRate = false;
				gameController.fireRateText.text = "";
			}

			if (powerUpDurationCounter <= 0 && powerUpDurationCounterFire <= 0) 
			{
				isPowerupActive = false;
			}

//			gameController.restartText.text = powerUpDurationCounter+" ________ "+powerUpDurationCounterFire;
		}

		if (isHealth) 
		{
			if (gameController.player.currentHealth < 3) 
			{
				gameController.player.currentHealth += 1;
			} 

			isHealth = false;
		}
		
	}

	public void activatePowerup(bool _isDoubleScore, 
								bool _isHealth, 
								bool _isFireRate, 
								float _duration)
	{
		if (!isDoubleScore) 
		{
			isDoubleScore = _isDoubleScore;
		} 
		if (!isFireRate) 
		{
			isFireRate = _isFireRate;
		}

		isHealth = _isHealth;

		if (_isDoubleScore) 
		{
			powerUpDurationCounter = _duration;
		} 
		else if(_isFireRate)
		{
			powerUpDurationCounterFire = _duration;
		}

		if (_isDoubleScore || _isFireRate) 
		{
			isPowerupActive = true;
		}

		gameController.addScore (75);


	}
}
