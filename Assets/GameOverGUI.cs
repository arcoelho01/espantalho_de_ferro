using UnityEngine;
using System.Collections;

public class GameOverGUI : MonoBehaviour {
	public GameController game;
	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI() {
		if(game.gameOver){
			GUI.color=Color.black;
			
			GUI.Label(new Rect(150,50,200,200), "Kills: "+game.enemiesKilled);
			GUI.Label(new Rect(150,70,200,200), "Standards Killed: "+game.standartKilled);
			GUI.Label(new Rect(150,90,200,200), "Tanks Killed "+game.tankKilled);
			GUI.Label(new Rect(150,110,200,200), "Fasts Killed: "+game.fastKilled);
			GUI.Label(new Rect(150,130,200,200), "Dodgers Killed: "+game.dodgerKilled);
			GUI.Label(new Rect(150,150,200,200), "Ghosts Killed: "+game.ghostKilled);
			GUI.Label(new Rect(150,170,200,200), "Phoenix Killed (not really): "+game.phoenixKilled);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(game.gameOver)
			renderer.enabled=true;
	}
}
