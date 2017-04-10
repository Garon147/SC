using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;

	private AudioSource audioSource;

	void Start () {

		audioSource = GetComponent<AudioSource> ();
		InvokeRepeating ("enemyWeaponFire", delay, fireRate);
	}

	void enemyWeaponFire() {

		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play ();
	}

}
