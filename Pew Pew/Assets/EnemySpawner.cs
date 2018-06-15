using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	float nextBuildTime;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextBuildTime) {
			nextBuildTime = Time.time + 5F;
			Instantiate(enemyPrefab, transform.position, Quaternion.identity);
		}
	}
}
