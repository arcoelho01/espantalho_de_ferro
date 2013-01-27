using UnityEngine;
using System.Collections;

public class GameOverGUI : MonoBehaviour {
	public GameController game;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI() {
		if(game.gameOver){
			string stText = "Kills: "+game.enemiesKilled;
			GUI.color=Color.black;
			
			GUI.Label(new Rect(150,50,200,200), stText);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(game.gameOver)
			renderer.enabled=true;
	}
}
