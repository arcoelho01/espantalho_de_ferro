using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
	public bool heartIsHere=false;
	private bool lastHeartState=false;
	private Heart heart;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(heartIsHere!=lastHeartState){
			lastHeartState=heartIsHere;
			if(heartIsHere)
				renderer.material.color=new Color(0,0,0);
			else
				renderer.material.color=new Color(1,1,1);
		}
		if(heartIsHere){
			if(Input.GetMouseButtonDown(0)){
				Debug.Log ("shot");
				heart.gameObject.audio.clip=heart.shotSound;
				heart.gameObject.audio.Play();
			}
			if(Input.GetMouseButtonDown(1)){
				Debug.Log ("exit");
				heartIsHere=false;
				heart.speed=10;
				heart.gameObject.renderer.enabled=true;
				heart.gameObject.audio.clip=heart.jumpOutSound;
				heart.gameObject.audio.Play();
			}
		}
		
		
	}
	void OnTriggerEnter(Collider other){
		if(other.tag == "Heart"){
	    	heartIsHere=true;
			heart=other.GetComponent<Heart>();
			heart.transform.position=transform.position;
			heart.gameObject.renderer.enabled=false;
			heart.speed=0;
			heart.gameObject.audio.clip=heart.jumpInSound;
			heart.gameObject.audio.Play();
		}
	}
	
}
