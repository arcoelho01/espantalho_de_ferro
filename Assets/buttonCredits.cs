using UnityEngine;
using System.Collections;

public class buttonCredits : MonoBehaviour {
	
	public GameObject selecter;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseOver(){
		selecter.GetComponent<selectBHV>().poss = 2;
		if(Input.GetMouseButton(0)){
			Application.LoadLevel("creditScene");
		}
	}
}
