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
		/* Spammed Spawn
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
		*/
		ComplexSpawnUpdate();
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
	
	
	//SUPER ALGORITMO DE SPAWN!
	/* Sufixos
	 std = standart enemy
	 fst = fast enemy
	 ddg = dodger enemy
	 tnk = tank enemy	 
	 gst = ghost enemy
	 pnx = phoenix enemy
	 */
	
	float stdCont = 0;
	float stdContLimit = 5f;
	float stdValue = 60;
	float stdBase = 1;
	float fstCont = 0;
	float fstContLimit = 9f;
	float fstValue = 22;
	float fstBase = 2;
	float ddgCont = 0;
	float ddgContLimit = 13.5f;
	float ddgValue = 11;
	float ddgBase = 3;
	float tnkCont = 0;
	float tnkContLimit = 17.5f;
	float tnkValue = 5;
	float tnkBase = 4;
	float gstCont = 0;
	float gstContLimit = 21f;
	float gstValue = 3;
	float gstBase = 5;
	float pnxCont = 0;
	float pnxContLimit = 25f;
	float pnxValue = 0;
	float pnxBase = 6;
	
	void ComplexSpawnUpdate (){
		//adiciona nos contadores
		float dTime = Time.deltaTime * 4;
		stdCont += dTime;
		fstCont += dTime;
		ddgCont += dTime;
		tnkCont += dTime;
		gstCont += dTime;
		pnxCont += dTime;
		
		//verifica o valor dos contadores para incrementar o Value
		if(stdCont >= stdContLimit){
			stdValue++;
			stdCont = 0;
		}
		if(fstCont >= fstContLimit){
			fstValue++;
			fstCont = 0;
		}
		if(ddgCont >= ddgContLimit){
			ddgValue++;
			ddgCont = 0;
		}
		if(tnkCont >= tnkContLimit){
			tnkValue++;
			tnkCont = 0;
		}
		if(gstCont >= gstContLimit){
			gstValue++;
			gstCont = 0;
		}
		if(pnxCont >= pnxContLimit){
			pnxValue++;
			pnxCont = 0;
		}
	}
	
	public void ComplexSpawn (){
		float totalValue = 0;
		totalValue += stdValue * stdBase;
		totalValue += fstValue * fstBase;
		totalValue += ddgValue * ddgBase;
		totalValue += tnkValue * tnkBase;
		totalValue += gstValue * gstBase;
		totalValue += pnxValue * pnxBase;
		
		float randomNumber = Random.Range(1, totalValue);
		float testValue = 0;
		testValue += stdValue * stdBase;
		if(randomNumber < testValue){
			//spawn Standart
			InstantiateEnemy(enemyModels[0]);
			return;
		}
		testValue += fstValue * fstBase;
		if(randomNumber < testValue){
			//spawn Fast
			InstantiateEnemy(enemyModels[1]);
			return;
		}
		testValue += ddgValue * ddgBase;
		if(randomNumber < testValue){
			//spawn Dodge
			InstantiateEnemy(enemyModels[2]);
			return;
		}
		testValue += tnkValue * tnkBase;
		if(randomNumber < testValue){
			//spawn Tank
			InstantiateEnemy(enemyModels[3]);
			return;
		}
		testValue += gstValue * gstBase;
		if(randomNumber < testValue){
			//spawn Ghost
			InstantiateEnemy(enemyModels[4]);
			return;
		}
		testValue += pnxValue * pnxBase;
		if(randomNumber < testValue){
			//spawn Phoeni5
			InstantiateEnemy(enemyModels[5]);
			return;
		}
	}
}











