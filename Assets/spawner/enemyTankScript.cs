using UnityEngine;
using System.Collections;

public class enemyTankScript : enemyScript {

	// Use this for initialization
	void Start () {
		type="Tank";
		CC = gameObject.GetComponent<CharacterController>();
		moveSpeed = 0.5f;
		hitPoints = 50.0f;
	}
}
