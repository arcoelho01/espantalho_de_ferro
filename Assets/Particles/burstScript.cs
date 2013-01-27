using UnityEngine;
using System.Collections;

public class burstScript : MonoBehaviour {
	
	float cont;
	public float duration;
	
	// Use this for initialization
	void Start () {
		GetComponent<ParticleEmitter>().Emit();
	}
	
	// Update is called once per frame
	void Update () {
		cont += Time.deltaTime;
		if(cont > duration){
			Destroy (gameObject);
		}
	}
}
