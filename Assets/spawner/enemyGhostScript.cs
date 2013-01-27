using UnityEngine;
using System.Collections;

public class enemyGhostScript : enemyScript {
	public float inv_timeout=0.5f;
	public bool going_invisible=false;
	// Use this for initialization
	public Animation inv;
	public Animation fly;
	public AnimationManager manager;
	void Start () {
		type="Ghost";
		CC = gameObject.GetComponent<CharacterController>();
		moveSpeed = 0.75f;
		hitPoints = 20.0f;
		transform.LookAt(target);
		cont = 0;
		iddle = false;
		
		inv.infinite=false;
		inv.loopCount=1;
		for(int i=0;i<47;i++){
			inv.sprites[i].time=0.01f;
		}
		fly= manager.currentAnimation;
		
		
	}
	
	void Update (){
		if(going_invisible){
			inv_timeout-=Time.deltaTime;
			if(inv_timeout<=0){
				
				going_invisible=false;
				cont = 0;
				iddle = true;
				randDir = Random.Range(0f, Mathf.PI*2);
				transform.position += new Vector3(Mathf.Cos(randDir), 0, Mathf.Sin(randDir)) * 1.5f;
				//GetComponent<MeshRenderer>().enabled = false;
				transform.position=new Vector3(transform.position.x,-10,transform.position.z);
			}
		}
		if(!iddle){
			Move ();
			cont += Time.deltaTime;
		}else{
			iddleCont += Time.deltaTime;
			if(iddleCont > 1){
				iddleCont = 0;
				iddle = false;
				//GetComponent<MeshRenderer>().enabled = true;
				transform.position=new Vector3(transform.position.x, 0.5f ,transform.position.z);
				transform.LookAt(new Vector3(target.x, transform.position.y, target.z));
				manager.currentAnimation=fly;
			}
		}
	}
	
	float cont;
	float randDir;
	bool iddle;
	float iddleCont;
	
	void Move (){
		CC.Move(transform.TransformDirection(new Vector3(0, 0, moveSpeed*Time.deltaTime)));
		if(!going_invisible && cont > Random.Range(2f, 3.5f)){
			going_invisible=true;
			inv_timeout=47*0.01f;
			inv.pos=0;
			inv.loopCount=0;
			inv.infinite=false;
			manager.currentAnimation=inv;		
		}
	}
}
