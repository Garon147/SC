using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{

	public Sprite[] heartSprites;
	public Image currentHealthImage;

	private PlayerController player;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController>();
	}


	void Update () 
	{
		GameObject playerObject = GameObject.FindGameObjectWithTag ("Player");
		if (playerObject != null) 
		{
			player = playerObject.GetComponent<PlayerController> ();
			currentHealthImage.sprite = heartSprites [player.currentHealth];
		}

	}
}
