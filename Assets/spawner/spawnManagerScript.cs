using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawnManagerScript : MonoBehaviour {

	public List<GameObject> spawner;
	float cont;
	public float spawnRate;
	public float decreaseRate;
	
	// Use this for initialization
	void Start () {
		cont = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(cont >= spawnRate){
			int rand = Random.Range (0, 4);
			spawner[rand].GetComponent<enemySpawner>().ComplexSpawn();
			//spawnRate -= decreaseRate;
			cont = 0;
		}
		cont += Time.deltaTime;
	}
}
