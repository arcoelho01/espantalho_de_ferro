using UnityEngine;
using System.Collections;

public class enemyFastScript : enemyScript {

	// Use this for initialization
	void Start () {
		CC = gameObject.GetComponent<CharacterController>();
		moveSpeed = 2.0f;
		hitPoints = 30;
	}
}
