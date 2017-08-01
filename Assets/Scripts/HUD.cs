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
		currentHealthImage.sprite = heartSprites [player.currentHealth];
	}
}
