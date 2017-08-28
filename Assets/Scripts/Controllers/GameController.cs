using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;

	public GameObject[] powerups;
	public int powerupCount;

	public float spawnWait;
	public float startWait;
	public float waveWait;

	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GUIText highscoreText;
	public GUIText doubleScoreText;
	public GUIText fireRateText;
	public GUIText yourScoreText;

	private bool gameOver;
	private bool restart;
	private int score;

	public PlayerController player;

	public bool isDoubleScore;
	public bool isFireRate;

	public float bonusProbability;

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

		bonusProbability = 0.0f;

		gameOverText.text = "";
		restartText.text = "";
		highscoreText.text = "";
		doubleScoreText.text = "";
		fireRateText.text = "";
		yourScoreText.text = "";

		score = 0;
		updateScore ();
		StartCoroutine (spawnWaves ());
		StartCoroutine (spawnPowerups ());

		Cursor.visible = false;
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
		updateBonusSpawnProbability ();

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
				if (player.currentHealth == -1) {
					
				} 
				restartText.text = "Go to bar to restart";
				restart = true;
				break;

			}

			increaseHazardCount();
//			updateBonusSpawnProbability ();
		}
	}

	IEnumerator spawnPowerups()
	{
		yield return new WaitForSeconds (0.5f);
		while (true) 
		{
			for (int i = 0; i < powerupCount; i++) 
			{
				GameObject powerup = powerups [Random.Range (0, powerups.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;

				if (score < 1000 && score > 20000) 
				{
					if (bonusProbability > 0.95f) 
					{
						Instantiate (powerup, spawnPosition, spawnRotation);
					}
				} 
				else 
				{
					if (bonusProbability > 0.9f) 
					{
						Instantiate (powerup, spawnPosition, spawnRotation);
					}
				}
				yield return new WaitForSeconds (0.0001f);

			}
			yield return new WaitForSeconds (2.0f);
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
		yourScoreText.text = "Your score : " + score;

		System.IO.File.AppendAllText(
			@"D:\logs\results.txt",
			currentPhoneNumberKey + " : " + score + System.Environment.NewLine
			);
		if (PlayerPrefs.HasKey ("Highscore")) 
		{
			if (score > PlayerPrefs.GetInt ("Highscore")) 
			{
				PlayerPrefs.SetInt ("Highscore", score);
				PlayerPrefs.SetString ("HighscoreKey", currentPhoneNumberKey);
				highscoreText.text = "Highscore" + " : " + score;
			} 
			else 
			{
				highscoreText.text = "Highscore" + " : " + PlayerPrefs.GetInt("Highscore");
			}
		} 
		else 
		{
			PlayerPrefs.SetInt ("Highscore", score);
			PlayerPrefs.SetString ("HighscoreKey", currentPhoneNumberKey);
			highscoreText.text = "Highscore" + " : " + score;
		}
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
		hazardCount += (1 + Mathf.FloorToInt (score / 1000));
	}

	public void updateBonusSpawnProbability()
	{
		if (score < 1000) 
		{
			bonusProbability = Random.Range (1, 40) * 0.025f;
			powerupCount = 1;
		} 
		else if (score > 20000) 
		{
			bonusProbability = Random.Range (1, 20) * 0.025f;
			powerupCount = 2;
		} 
		else 
		{
			bonusProbability = Random.Range (1, 20) * 0.05f;
			powerupCount = 3;
		}
	}

	public void addHighScore(string name, int score)
	{
		int newScore;
		string newName;
		int oldScore;
		string oldName;
		newScore = score;
		newName = name;
		string hsScoreKey = "HighScore";
		string hsNameKey = "HighScoreName";
		for(int i = 0 ; i < 10 ; i++)
		{
			if(PlayerPrefs.HasKey(i + hsScoreKey))
			{
				if(PlayerPrefs.GetInt(i+hsScoreKey) < newScore)
				{ 
					// new score is higher than the stored score
					oldScore = PlayerPrefs.GetInt(i+hsScoreKey);
					oldName = PlayerPrefs.GetString(i+hsNameKey);
					PlayerPrefs.SetInt(i+hsScoreKey,newScore);
					PlayerPrefs.SetString(i+hsNameKey,newName);
					highscoreText.text = PlayerPrefs.GetString(i+hsNameKey) + " : " + PlayerPrefs.GetInt(i+hsScoreKey);
					newScore = oldScore;
					newName = oldName;
					Debug.Log (newName + " : " + newScore); 
				}
			}
			else
			{
				PlayerPrefs.SetInt(i+hsScoreKey,newScore);
				PlayerPrefs.SetString(i+hsNameKey,newName);
				highscoreText.text = PlayerPrefs.GetString(i+hsNameKey) + " : " + PlayerPrefs.GetInt(i+hsScoreKey);
				newScore = 0;
				newName = "";
			}
		}
	}
}
