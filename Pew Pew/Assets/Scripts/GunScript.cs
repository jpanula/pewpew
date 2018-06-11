using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {

	bool playerFacingRight;
	float angle;
	Vector3 dir;
	public GameObject bulletPrefab;
	public GameObject muzzle;
	AudioSource gunAudioSource;
	public AudioClip pewSound;

	// Use this for initialization
	void Start () {
		gunAudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		playerFacingRight = GameObject.Find("PlayerSprite").GetComponent<PlayerController>().IsFacingRight();
		dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
 		angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		if (!playerFacingRight) {
			angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
		}
 		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


		if (Input.GetMouseButtonDown(0)) {
			Fire();
		}
	}

	void Fire() {
		float fireAngle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		Instantiate(bulletPrefab, (Vector3) muzzle.transform.position, Quaternion.AngleAxis(fireAngle, Vector3.forward));
		gunAudioSource.PlayOneShot(pewSound);
	}
}
