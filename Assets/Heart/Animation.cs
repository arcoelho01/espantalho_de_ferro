using UnityEngine;
using System.Collections;
[System.Serializable]
public class Animation{
	public Sprite[] sprites;
	public int pos;
	public int loopCount=1;
	public bool infinite=true;
	private float timeout;
	
	void changeSprite(int n){
		if(n>=0 && n<sprites.Length){
			pos=n;	
			timeout=sprites[pos].time;
		}
	}
	public Texture2D getSprite(){
		return sprites[pos].image;	
	}
	public void refresh(){
		timeout-=Time.deltaTime;
		if(timeout<=0){
			if(pos+1<sprites.Length){
				changeSprite (pos+1);	
			}
			else{
				if(infinite || loopCount>0){
					if(!infinite)
						loopCount--;
					changeSprite (0);
				}
			}
		}
	}
	
}
