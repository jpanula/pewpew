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

		if (collision.gameObject.tag == "Enemy") {
			Vector2 dir = (Vector2) GameObject.Find("PlayerSprite").transform.position - (Vector2) transform.position;
			dir = -dir.normalized;
			GameObject enemy = collision.gameObject;
			enemy.GetComponent<EnemyScript>().TakeDamage();
			enemy.GetComponent<Rigidbody2D>().AddForce(dir * 10f);
		}
		if (collision.gameObject.tag != "Player") {
			Destroy(gameObject);	
		}
	}
}
