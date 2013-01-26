using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {
	
	public Vector3 target; //posição alvo do inimigo
	public float moveSpeed;
	public int hitPoints;
	
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
		transform.LookAt(target);
		CC.Move(transform.TransformDirection(new Vector3(0, 0, moveSpeed*Time.deltaTime))); //transforma o vetor de movimento para a rotação atual do objeto, então usa a função CharacterController.Move() para mover
	}
}
