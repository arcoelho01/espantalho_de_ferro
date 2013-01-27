using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour {
	public float speed=1;
	//private CharacterController character;
	// Use this for initialization
	public AudioClip jumpInSound;
	public AudioClip jumpOutSound;
	public AudioClip shotSound;
	void Start () {
		GetComponent<CharacterController>().isTrigger=true;
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if(speed>0){
			CharacterController character = GetComponent<CharacterController>();
			Vector3 mousePos = Camera.mainCamera.ScreenToWorldPoint(Input.mousePosition);
			Vector2 moveDir= new Vector2(0,0);
			moveDir=new Vector2(mousePos.x-transform.position.x,mousePos.z-transform.position.z).normalized;
			Vector2 distance = new Vector2(mousePos.x,mousePos.z)-new Vector2(transform.position.x, transform.position.z);
			Debug.Log ("Magnitude: "+distance.magnitude);
			if(distance.magnitude>1)
				character.Move(new Vector3(moveDir.x,0,moveDir.y)*speed*Time.deltaTime);
			
		}
	}
}
