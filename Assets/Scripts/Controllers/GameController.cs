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

	private bool gameOver;
	private bool restart;
	private int score;

	private PlayerController player;

	public bool isDoubleScore;

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
		gameOverText.text = "";
		restartText.text = "";
		score = 0;
		updateScore ();
		StartCoroutine (spawnWaves ());
	}

	void Update() 
	{
		if (restart) {

			if (Input.GetKeyDown (KeyCode.R)) {
				
				SceneManager.LoadScene ("Main");
			}
		}
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

				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver) 
			{
				if (player.currentHealth == 0) {
					restartText.text = "Press 'R' to restart.";
					restart = true;
					break;
				} 

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

	void updateScore() 
	{
		scoreText.text = "Score: " + score;
	}

	public void gameOverMethod() 
	{
		gameOverText.text = "Game Over!";
		gameOver = true;	
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
