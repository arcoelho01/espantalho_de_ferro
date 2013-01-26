using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemySpawner : MonoBehaviour {
	
	public bool vertical; //define a posição do spawner na tela
	public GameObject turret; //define qual objeto será o alvo dos inimigos criados neste Spawner
	public float Vsize; //tamanho vertical do spawner
	public float Hsize; //tamanho horizontal do spawner
	public List<GameObject> enemyModels; //lista de objetos inimigos que podem ser criados
		
	GameObject tempEnemyRef; //referência temporaria ao inimigo recém-spawnado
	enemyScript tempScriptRef; //referência temporária ao script
	
	int randModel;
	
	void Start () {
		cont = 0;
		spawnRate = 1;
		//WAVESPAWNER
		contAux = 0;
		spawnRate2 = 3f;
		increaseRate = 0.01f;
		numberAux = 1;
	}	
	void Update () {
		//SimpleSpawn();
		if(contAux > spawnRate2){
			WaveSpawn(numberAux, enemyModels[Chance()]);
			contAux = 0;
		}
		if(numberCount > 10){
			numberAux++;
			numberCount = 0;
		}
		if(speedCount > 35){
			spawnRate2 -= 0.1f;
			numberAux = 1;
			speedCount = 0;
		}
		contAux += Time.deltaTime;
		numberCount += Time.deltaTime;
		speedCount += Time.deltaTime;
	}
	
	//SIMPLE SPAWN VARIABLES
	float spawnRate; //taxa de spawns por segundo
	float cont;
	
	void SimpleSpawn (){
		if(cont > 1/spawnRate){
			InstantiateEnemy(enemyModels[0]);
			cont = 0;
		}
		cont += Time.deltaTime;
	}
	
	//INSTANTIATE ENEMY VARIABLES
	float randomPos; //variável que guarda a posição aleatória de spawn atual
	float spawnPos;
	
	void InstantiateEnemy(GameObject enemy){
		if(vertical){ // se o spawner estiver em uma posição vertical(esquerda ou direita)
			randomPos = Random.Range(0, 19);
			spawnPos = randomPos - (Vsize/2);				
			tempEnemyRef = Instantiate(enemy, new Vector3(transform.position.x, 0, spawnPos), Quaternion.identity) as GameObject;
			tempEnemyRef.GetComponent<enemyScript>().target = turret.gameObject.transform.position;
				
		}else{ //se o spawner estiver em uma posição horizontal(cima ou baixo)
			randomPos = Random.Range(0, 33);
			spawnPos = randomPos - (Hsize/2);
			tempEnemyRef = Instantiate(enemy, new Vector3(spawnPos, 0, transform.position.z), Quaternion.identity) as GameObject;
			tempEnemyRef.GetComponent<enemyScript>().target = turret.gameObject.transform.position;
		}
	}
	
	//WAVE SPAWN VARIABLES
	float spawnRate2;
	float increaseRate;
	float contAux;
	int numberAux;
	float numberCount;
	float speedCount;
		
	void WaveSpawn(int spawnNumber, GameObject model){
	//essa função spawna uma certa quantia fixa de inimigos cada vez que é chamada
		for(int i = 0; i < spawnNumber; i++){
			InstantiateEnemy(model);
		}
	}
	
	int Chance(){
		int randNumber = Random.Range(1, 101);
		if(randNumber < 50){
			return 0;
		}else if(randNumber < 70){
			return 1;
		}else if(randNumber < 85){
			return 3;
		}else if(randNumber < 95){
			return 2;
		}else{
			return 4;
		}
	}
}











