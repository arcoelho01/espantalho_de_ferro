using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {
	
	public Vector3 target; //posição alvo do inimigo
	public float moveSpeed;
	public float hitPoints;
	public string type;
	//protected float fHealth = 20.0f;
	
	public CharacterController CC; //variável que guarda a informação do componente Character Controller deste objeto
	
	// Use this for initialization
	void Start () {
		CC = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}
	
	public virtual void Move () {
		if(Vector3.Distance(transform.position, target) > 0.3f) {

			transform.LookAt(target);
		}

		CC.Move(transform.TransformDirection(new Vector3(0, 0, moveSpeed*Time.deltaTime))); //transforma o vetor de movimento para a rotação atual do objeto, então usa a função CharacterController.Move() para mover


	}

	/// <summary>
	/// </summary>
	public virtual void TakeDamage(float fAmount) {

		hitPoints -= fAmount;

		// DEBUG
		Debug.Log(this.transform + " Damage: " + fAmount + " Current health: " + hitPoints);

		if(hitPoints <= 0) {
			GameController game = GameObject.Find("GameController").GetComponent<GameController>();
			game.enemiesKilled++;
			switch(type){
				case "Standart":
					game.standartKilled++;
				break;
				case "Tank":
					game.tankKilled++;
				break;
				case "Fast":
					game.fastKilled++;
				break;
				case "Dodger":
					game.dodgerKilled++;
				break;
				case "Ghost":
					game.ghostKilled++;
				break;
				case "Phoenix":
					game.phoenixKilled++;
				break;
			}
			Die();
		}
	}

	/// <summary>
	/// </summary>
	protected virtual void Die() {

		Destroy(gameObject);

	}
}
