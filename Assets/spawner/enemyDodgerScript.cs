using UnityEngine;
using System.Collections;

public class enemyDodgerScript : enemyScript {
	
	// Use this for initialization
	void Start () {
		type="Dodger";
		CC = gameObject.GetComponent<CharacterController>();
		moveSpeed = 1.0f;
		hitPoints = 20;
		cont = 0;
	}
	
	void Update (){
		Move ();	
	}
	
	float cont;
	
	void Move (){
		//transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + Mathf.Sin(cont)*Mathf.PI/2, transform.eulerAngles.z);
		CC.Move(transform.TransformDirection(new Vector3(0, 0, moveSpeed*Time.deltaTime)));
		CC.Move(transform.TransformDirection(new Vector3((Mathf.Sin(cont*3)*Mathf.PI/6)/10, 0, 0)));
		cont += Time.deltaTime;
		transform.LookAt(target);
		//Debug.Log ("rodou");
	}
	
}
