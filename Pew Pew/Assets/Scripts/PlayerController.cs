using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	const float MAX_SPEED = 10f;
	bool grounded;
	Rigidbody2D rb;
	Animator animator;
	SpriteRenderer render;
	bool facingRight;
	bool jumping;
	bool touchingGround;
	public CircleCollider2D feet;
	public AudioClip[] footSteps;
	public LayerMask groundLayer;
	bool damaged = false;
	AudioSource playerSoundsSource;
	public AudioClip jumpSound;
	public AudioClip oofSound;
	float recoverTime;

	public void PlayFootstep() {
		playerSoundsSource.PlayOneShot(footSteps[Random.Range(0, footSteps.Length - 1)]);
	}


	public bool IsFacingRight() {
		return facingRight;
	}
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		grounded = true;
		facingRight = true;
		animator = GetComponent<Animator>();
		playerSoundsSource = GetComponent<AudioSource>();
		render = GetComponent<SpriteRenderer>();
	}	
	
	// Update is called once per frame
	void Update () {
		if (grounded && Input.GetKeyDown("space")) {
			rb.AddForce(new Vector2(0.0f, 1600.0f));
			playerSoundsSource.PlayOneShot(jumpSound);
		}
		animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
		animator.SetFloat("YSpeed", Mathf.Abs(rb.velocity.y));
		if (render.color.b < 1) {
			Color color = render.color;
			color.b += 0.03f;
			color.g += 0.03f;
			render.color = color;
		}
	}
	
	void FixedUpdate() {
		
		if (damaged && Time.time > recoverTime) {
			RecoverFromDamage();
		}

		grounded = Physics2D.OverlapCircle(feet.transform.position, feet.radius, groundLayer);
		animator.SetBool("Grounded", grounded);
		rb.velocity = new Vector2(Input.GetAxis("Horizontal") * MAX_SPEED, rb.velocity.y);

		if (Input.GetAxis("Horizontal") > 0 && !facingRight) {
			Flip();
		} else if (Input.GetAxis("Horizontal") < 0 && facingRight) {
			Flip();
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Enemy" || other.otherCollider.gameObject.tag == "Enemy") {
			Vector2 dir = (Vector2) other.gameObject.transform.position - (Vector2) transform.position;
			dir = -dir.normalized;
			TakenDamage();
			rb.AddForce(new Vector2(-0.5f, 0.5f) * 20F, ForceMode2D.Impulse);
		}
	}

	void TakenDamage() {
		playerSoundsSource.PlayOneShot(oofSound);
		recoverTime = Time.time + 1F;
		damaged = true;
		render.color = new Color(1, 0, 0);
		Physics2D.IgnoreLayerCollision(10, 9, true);
	}

	void RecoverFromDamage() {
		damaged = false;
		Physics2D.IgnoreLayerCollision(10, 9, false);
	}


	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
