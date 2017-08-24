using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;

	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText highscoreText;
	public GUIText doubleScoreText;
	public GUIText fireRateText;

	private bool gameOver;
	private bool restart;
	private int score;

	public PlayerController player;

	public bool isDoubleScore;
	public bool isFireRate;

	void Start() 
	{
		GameObject playerObject = GameObject.FindWithTag ("Player");
		if (playerObject != null) 
		{
			player = playerObject.GetComponent<PlayerController> ();
		}

		gameOver = false;
		restart = false;

		isDoubleScore = false;
		isFireRate = false;

		gameOverText.text = "";
		restartText.text = "";
		highscoreText.text = "";
		doubleScoreText.text = "";
		fireRateText.text = "";

		score = 0;
		updateScore ();
		StartCoroutine (spawnWaves ());
	}

	void Update() 
	{
		if (restart) {

			if (Input.GetKeyDown (KeyCode.R)) {
				
				SceneManager.LoadScene ("KeyboardScene");
			}
		}

		GameObject playerObject = GameObject.FindWithTag ("Player");
		if (playerObject != null) 
		{
			player = playerObject.GetComponent<PlayerController> ();
		}
		updateFireRate ();
	}
		
	IEnumerator spawnWaves() 
	{
		yield return new WaitForSeconds (startWait);
		while (true) 
		{

			for (int i = 0; i < hazardCount; i++) 
			{

				GameObject hazard = hazards [Random.Range (0, hazards.Length)];

				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;

//				if (hazard.CompareTag ("Powerup")) {
//					spawnRotation = new Quaternion ();
//					spawnRotation.x = -90.0f;
//				}

				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) 
			{
				if (player.currentHealth == -1) {
					
				} 
				restartText.text = "Press 'R' to restart.";
				restart = true;
				break;

			}

//			decreaseSpawnWait (0.1f);
//			decreaseWaveWait(0.5f);
			increaseHazardCount();
		}
	}

	public void addScore(int newScore) 
	{
		if (isDoubleScore) 
		{
			newScore *= 2;
		}
		score += newScore;
		updateScore ();
	}

	public void updateFireRate()
	{
		if (isFireRate) 
		{
			player.setFireRate (0.1f);
		} 
		else 
		{
			player.setFireRate (0.25f);
		}

	}

	void updateScore() 
	{
		scoreText.text = "Score: " + score;
	}

	public void gameOverMethod() 
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
		string currentPhoneNumberKey = PlayerPrefs.GetString ("CurrentPhoneNumber");
		PlayerPrefs.SetInt (currentPhoneNumberKey, score);
		highscoreText.text = currentPhoneNumberKey + " : " + score;
	}

	public void decreaseWaveWait(float delta)
	{
		if (waveWait != 0) 
		{
			waveWait -= delta;
			gameOverText.text = waveWait.ToString ();
		} 
		else 
		{
			return;
		}
	}

	public void decreaseSpawnWait(float delta)
	{
		if (spawnWait != 0) 
		{
			spawnWait -= delta;
			gameOverText.text = spawnWait.ToString();
		} 
		else 
		{
			return;
		}
	}

	public void increaseHazardCount()
	{
		hazardCount++;
	}
}
