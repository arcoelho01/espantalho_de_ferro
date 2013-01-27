using UnityEngine;
using System.Collections;

public class selectBHV : MonoBehaviour {

	public Vector3[] pos;
	public int poss;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = pos[poss];
	}
}
