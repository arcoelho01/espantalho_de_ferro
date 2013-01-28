using UnityEngine;
using System.Collections;

public class GameOverGUI : MonoBehaviour {
	public GameController game;
	// Use this for initialization
	float posx=0;
	void Start () {
		posx=Camera.mainCamera.WorldToScreenPoint(transform.position).x-100;
	}
	
	void OnGUI() {
		if(game.gameOver){
			GUI.color=Color.black;
			
			GUI.Label(new Rect(posx,50,200,200), "Kills: "+game.enemiesKilled);
			GUI.Label(new Rect(posx,70,200,200), "Standards Killed: "+game.standartKilled);
			GUI.Label(new Rect(posx,90,200,200), "Tanks Killed "+game.tankKilled);
			GUI.Label(new Rect(posx,110,200,200), "Fasts Killed: "+game.fastKilled);
			GUI.Label(new Rect(posx,130,200,200), "Dodgers Killed: "+game.dodgerKilled);
			GUI.Label(new Rect(posx,150,200,200), "Ghosts Killed: "+game.ghostKilled);
			//GUI.Label(new Rect(150,170,200,200), "Phoenix Killed (not really): "+game.phoenixKilled);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(game.gameOver)
			renderer.enabled=true;
	}
}
