using UnityEngine;
using System.Collections;

public class startButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseOver(){
		if(Input.GetMouseButton(0)){
			Application.LoadLevel("gameScene");
		}
	}
}
