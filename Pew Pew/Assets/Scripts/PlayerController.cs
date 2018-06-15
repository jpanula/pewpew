using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	const float MAX_SPEED = 10f;
	bool grounded;
	Rigidbody2D rb;
	Animator animator;
	bool facingRight;
	bool jumping;
	bool touchingGround;
	public CircleCollider2D feet;
	public AudioClip[] footSteps;
	public LayerMask groundLayer;
	AudioSource playerSoundsSource;

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
	}	
	
	// Update is called once per frame
	void Update () {
		if (grounded && Input.GetKeyDown("space")) {
			rb.AddForce(new Vector2(0.0f, 1600.0f));
		}
		animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
		animator.SetFloat("YSpeed", Mathf.Abs(rb.velocity.y));
	}
	
	void FixedUpdate() {
		
		grounded = Physics2D.OverlapCircle(feet.transform.position, feet.radius, groundLayer);
		animator.SetBool("Grounded", grounded);
		rb.velocity = new Vector2(Input.GetAxis("Horizontal") * MAX_SPEED, rb.velocity.y);

		if (Input.GetAxis("Horizontal") > 0 && !facingRight) {
			Flip();
		} else if (Input.GetAxis("Horizontal") < 0 && facingRight) {
			Flip();
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
