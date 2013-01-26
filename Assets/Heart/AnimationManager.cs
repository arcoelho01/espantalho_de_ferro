using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {
	public Animation currentAnimation;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currentAnimation.refresh();
		renderer.material.mainTexture=currentAnimation.getSprite();
	}
}
