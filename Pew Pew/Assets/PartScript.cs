using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Physics2D.IgnoreLayerCollision(11, 9, true);
		Physics2D.IgnoreLayerCollision(11, 10, true);
		Physics2D.IgnoreLayerCollision(11, 11, true);
		GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-2, 2), Random.Range(-2, 2)), ForceMode2D.Impulse);	
		GetComponent<Rigidbody2D>().AddTorque(Random.Range(-1.5F, 1.5F), ForceMode2D.Impulse);	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
