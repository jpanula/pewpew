using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	const float BULLET_SPEED = 50f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.right * BULLET_SPEED * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log(collision.gameObject.tag);
		if (collision.gameObject.tag != "Player") {
			Destroy(gameObject);	
		}
	}
}
