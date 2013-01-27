using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	public Texture2D[] screens;
	public int pos=0;
	public void changeScreen(int n){
		if(n>=0 && n<screens.Length){
			renderer.material.mainTexture=screens[n];	
		}
		pos=n;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.LoadLevel("menuScene");
		}
		if(Input.anyKeyDown){
			if(pos+1<screens.Length){
				changeScreen(pos+1);	
			}
			else{
				changeScreen(0);
			}
		}
	}
}
