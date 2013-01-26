using UnityEngine;
using System.Collections;

public class CrowSoundPlayer : MonoBehaviour {
	
	public float cont;
	
	// Use this for initialization
	void Start () {
		cont = 0;
		audio.loop = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(cont > 10){
			if(Random.Range(0, 2) != 1){
				audio.Play();
				cont = 0;
			}else{
				Debug.Log ("deu 1");
				cont = 0;
			}
		}
		if(!audio.isPlaying){
			cont += Time.deltaTime;
		}
	}
}
