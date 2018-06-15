using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

	AudioSource enemySoundSource;
	public AudioClip[] enemyDamageSounds;
	float nextSoundTime;
	SpriteRenderer render;
	Rigidbody2D rb;
	GameObject player;
	public LayerMask enemyLayer;
	public LayerMask groundLayer;
	float lastShotTime;
	public GameObject[] parts;
	bool isDead;
	int health;
	MonoBehaviour script;
	static int amntKilled = 0;
	Text scoreText;
	
	void Awake() {
		transform.localScale = new Vector3(0, 0, 0);
	}

	// Use this for initialization
	void Start () {
		enemySoundSource = GetComponent<AudioSource>();		
		render = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.Find("PlayerSprite");
		Physics2D.IgnoreLayerCollision(8, 9, true);
		script = GetComponent<EnemyScript>();
		isDead = false;
		health = 5;
		scoreText = GameObject.Find("Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x < 0.4) {
			Vector3 newTransform = transform.localScale;
			newTransform.x += 0.05F;
			newTransform.y += 0.05F;
			transform.localScale = newTransform;
		}
		if (render.color.b < 1) {
			Color color = render.color;
			color.b += 0.10f;
			color.g += 0.10f;
			render.color = color;
		}
		if (Time.time > lastShotTime) {
			rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, 0.1f));
		}
	}

	public void TakeDamage() {
		
		health--;

		if (health <= 0 && !isDead)  {
			isDead = true;
			Die();
		}

		lastShotTime = Time.time + 0.2F;

		if (Time.time > nextSoundTime) {
			AudioClip randomSound = enemyDamageSounds[Random.Range(0, enemyDamageSounds.Length - 1)];
			enemySoundSource.PlayOneShot(randomSound);
			nextSoundTime = Time.time + randomSound.length + 1F;
		}

		render.color = new Color(1, 0, 0);
	}

	void Die() {

		foreach (GameObject part in parts) {
			part.SetActive(true);
		}
		Destroy(render);
		Destroy(script);
		Destroy(rb);
		Destroy(GetComponent<BoxCollider2D>());
		amntKilled++;
		scoreText.text = "Kills: " + amntKilled;
	}
}
