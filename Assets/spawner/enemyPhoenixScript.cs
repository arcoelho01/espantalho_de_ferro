using UnityEngine;
using System.Collections;

public class enemyPhoenixScript : enemyScript {

	// Use this for initialization
	void Start () {
		CC = gameObject.GetComponent<CharacterController>();
		moveSpeed = 0.8f;
		hitPoints = 100;
	}
}
