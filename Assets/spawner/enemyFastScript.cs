using UnityEngine;
using System.Collections;

public class enemyFastScript : enemyScript {

	// Use this for initialization
	void Start () {
		type="Fast";
		CC = gameObject.GetComponent<CharacterController>();
		moveSpeed = 1.6f;
		hitPoints = 10;
	}
}
