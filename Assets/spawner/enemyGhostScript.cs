using UnityEngine;
using System.Collections;

public class enemyGhostScript : enemyScript {

	// Use this for initialization
	void Start () {
		CC = gameObject.GetComponent<CharacterController>();
		moveSpeed = 0.6f;
		hitPoints = 100;
		transform.LookAt(target);
		cont = 0;
		iddle = false;
	}
	
	void Update (){
		if(!iddle){
			Move ();
			cont += Time.deltaTime;
		}else{
			iddleCont += Time.deltaTime;
			if(iddleCont > 1){
				iddleCont = 0;
				iddle = false;
				GetComponent<MeshRenderer>().enabled = true;
				transform.LookAt(target);
			}
		}
	}
	
	float cont;
	float randDir;
	bool iddle;
	float iddleCont;
	
	void Move (){
		CC.Move(transform.TransformDirection(new Vector3(0, 0, moveSpeed*Time.deltaTime)));
		if(cont > Random.Range(2f, 3.5f)){
			cont = 0;
			iddle = true;
			randDir = Random.Range(0f, Mathf.PI*2);
			transform.position += new Vector3(Mathf.Cos(randDir), 0, Mathf.Sin(randDir)) * 1.5f;
			GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
