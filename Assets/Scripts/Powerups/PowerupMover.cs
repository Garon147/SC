using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupMover : MonoBehaviour {

	public float speed;
	void Start () 
	{
		transform.Translate (0.0f, 0.0f, speed * Time.deltaTime);
	}

	void Update () 
	{
		transform.Translate (0.0f, 0.0f, speed * Time.deltaTime);
	}
}
