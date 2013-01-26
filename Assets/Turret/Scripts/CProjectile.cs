using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))] // Needed for collision check

public class CProjectile : MonoBehaviour {

	float fSpeed = 2.0f;
	float fMaxSpeed = 2.5f;
	float fDamage = 10.0f;
	float fAcceleration = 1.0f;
	
	private Vector3 direction;
	private float fTimeToLive = 10.0f;

	Transform trShooter;

	// Use this for initialization
	void Start () {
	
		direction = transform.forward;
		gameObject.GetComponent<SphereCollider>().isTrigger = true;

		StartCoroutine(kill());
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.position += (direction * fSpeed * Time.deltaTime);

		if(fSpeed < fMaxSpeed) {

			fSpeed += fAcceleration * Time.deltaTime;
		}
	}

	/// <summary>
	/// Kills the object after his life span, freeing up some memory
	/// </summary>
	/// <returns>
	/// A <see cref="IEnumerator"/>
	/// </returns>
	protected IEnumerator kill() {
		
		yield return new WaitForSeconds(fTimeToLive);
		
		Destroy(gameObject);
	}

	/// <summary>
	/// Collisions with the enemy
	/// </summary>
	void OnTriggerEnter(Collider other) {

		if(other.gameObject.layer == 8) {

			// Get the enemy component
			enemyScript eScript = other.gameObject.GetComponent<enemyScript>();

			if(eScript) {

				// Does the damage
				eScript.TakeDamage(fDamage);
			}

			// Destroy the projectile
			Die();
		}
		//else if(other.gameObject.layer == 9 && other.gameObject.transform != trShooter) {

		//	// Get the tower component
		//	TowerAim towerScript = other.gameObject.GetComponent<TowerAim>();

		//	if(towerScript) {

		//		// Take the hit
		//		towerScript.TakeDamage(fDamage);
		//	}

		//	// Destroy the projectile
		//	Die();
		//}
	}

	/// <summary>
	/// Keeps who shot this projectile
	/// </summary>
	public void SetShooter(Transform tShooter) {

		trShooter = tShooter;

		// DEBUG
		Debug.Log("Shooter set to " + trShooter);
	}

	/// <summary>
	/// </summary>
	protected virtual void Die() {

		Destroy(gameObject);
	}
}
