using UnityEngine;
using System.Collections;

public class enemyStandartScript : enemyScript {

	// Use this for initialization
	void Start () {
		type="Standart";
		CC = gameObject.GetComponent<CharacterController>();
		moveSpeed = 0.8f;
		hitPoints = 20.0f;
	}
}
