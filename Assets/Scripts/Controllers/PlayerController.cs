using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	private AudioSource audioSource;

	public float speed;
	public Boundary boundary;
	public float tilt;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	private float nextFire;

	void Update() {

		audioSource = GetComponent<AudioSource> ();
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {

			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
		}

	}

	void FixedUpdate() {

		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		rb = GetComponent<Rigidbody> ();

		Vector3 playerMovement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = playerMovement * speed;

		Vector3 playerPosition = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);
		rb.position = playerPosition;

		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * (- tilt));
	}
}

[System.Serializable]
public class Boundary {

	public float xMin, xMax, zMin, zMax;
}
