using UnityEngine;
using System.Collections;

public class buttonHowToPlay : MonoBehaviour {
	
	public GameObject selecter;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver(){
		selecter.GetComponent<selectBHV>().poss = 1;
		if(Input.GetMouseButton(0)){
			Application.LoadLevel("tutorialScene");
		}
	}
}
