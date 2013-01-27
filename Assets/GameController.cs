using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public int turrentsCount=4, enemiesKilled;
	public bool gameOver=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameOver){
			if(turrentsCount<=0){
					gameOver=true;
			}
		}
		else{
			Application.LoadLevel("menuScene");
		}
	}
}
