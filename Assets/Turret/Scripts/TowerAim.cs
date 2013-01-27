using UnityEngine;
using System;
using System.Collections;

public class TowerAim : MonoBehaviour {

	private Vector3 v3WorldPos;
	private float fCameraDif;
	bool bnTowerIsActive = false;	// the tower is enabled or not?
	private bool lastActiveState=false;
	private float fFrequency;
	private float fMinFrequency = 0.5f;
	private float fMaxFrequency = 2f;
	private float fFrequencyIncrease = .5f;
	private float fFrequencyDecrease = .3f;
	private float fCannonTimer = 0.0f;

	public Transform trCannon = null;
	private Color myColor;

	public CProjectile trBulletType;
	//Tower

	private Heart heart;

	public float fHealth = 100.0f;
	public Transform trBulletSpawnPoint;
	
	// Use this for initialization
	void Start () {
	
		fCameraDif = Camera.main.transform.position.y - transform.position.y;
		fFrequency = fMinFrequency;

		if(!trCannon) {

			// DEBUG
			Debug.LogError("Cannon not defined in the inspector");
		}

		myColor = trCannon.gameObject.renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		if(bnTowerIsActive!=lastActiveState){
			lastActiveState=bnTowerIsActive;
		}

		// Check for input and update frequency
		if(bnTowerIsActive) {

			// Reads the mouse
			if(Input.GetButton("Fire1")) {

				fFrequency += fFrequencyIncrease * Time.deltaTime;
			}
			else {
				// Perform the cooldown
				fFrequency -= fFrequencyDecrease * Time.deltaTime;
			}

			fFrequency = Mathf.Clamp(fFrequency, fMinFrequency, fMaxFrequency);
			
			
			// Moves the cannon accordingly to the frequency
			if(trCannon) {
	
				myColor.a -= 0.3f * Time.deltaTime;
				myColor.a = Mathf.Clamp01(myColor.a);
	
				fCannonTimer -= Time.deltaTime;
				if(fCannonTimer <= 0.0f) {
	
					// Adjusts the new frequency
					fCannonTimer = 1/fFrequency;
	
					// DEBUG
					//Debug.Log(fCannonTimer);
	
					// ... and then shoots
					Shoot();
				}
	
				trCannon.gameObject.renderer.material.color = myColor;
			}
		
			// Rotates the tower
			Vector3 v3AimRotation = new Vector3(Input.mousePosition.x, Input.mousePosition.y, fCameraDif);
			v3WorldPos = Camera.main.ScreenToWorldPoint(v3AimRotation);
			transform.LookAt(v3WorldPos);
			
			//Frequency Management
			
			float freqRate;// %of frequency used
			freqRate=(fFrequency-fMinFrequency)/(fMaxFrequency-fMinFrequency);
			if(freqRate>=1){
				bnTowerIsActive=false;
				heart.speed=1+(10*(1-freqRate));
				heart.gameObject.renderer.enabled=true;
				heart.gameObject.audio.clip=heart.jumpOutSound;
				heart.gameObject.audio.Play();
			}
			if(Input.GetMouseButtonDown(1)){
				bnTowerIsActive=false;
				heart.speed=1+(10*(1-freqRate));
				heart.gameObject.renderer.enabled=true;
				heart.gameObject.audio.clip=heart.jumpOutSound;
				heart.gameObject.audio.Play();
			}
		}

		
	}

	void OnGUI() {
		if(bnTowerIsActive){
			string stText = "Beat: " + String.Format("{0:0.00}", fFrequency) + "\n" 
				+ String.Format("{0:0.0}", fCannonTimer);
			GUI.Label(new Rect(10,10,100,100), stText);
		}
	}

	void Shoot() {

		heart.gameObject.audio.clip=heart.shotSound;
		heart.gameObject.audio.Play();
		// Create the bullet
		CProjectile newBullet;
		if(trBulletSpawnPoint) {

			newBullet = (CProjectile)GameObject.Instantiate(trBulletType, trBulletSpawnPoint.transform.position, transform.rotation);
		}
		else {

			newBullet = (CProjectile)GameObject.Instantiate(trBulletType, transform.position, transform.rotation);
		}

		newBullet.SetShooter(this.transform);

		//
		myColor.a = 1.0f;
	}
	void OnTriggerEnter(Collider other){
		if(other.tag == "Heart"){
	    	bnTowerIsActive=true;
			heart=other.GetComponent<Heart>();
			heart.transform.position=transform.position;
			heart.gameObject.renderer.enabled=false;
			heart.speed=0;
			heart.gameObject.audio.clip=heart.jumpInSound;
			heart.gameObject.audio.Play();
		}
		if(other.gameObject.layer==8){
			Debug.Log ("hit");	
			TakeDamage(10);
			Destroy(other.gameObject);
		}
	}

	/// <summary>
	/// </summary>
	public virtual void TakeDamage(float fAmount) {

		fHealth -= fAmount;

		// DEBUG
		Debug.Log(this.transform + " Damage: " + fAmount + " Current health: " + fHealth);

		if(fHealth <= 0) {

			Die();
		}
	}

	/// <summary>
	/// </summary>
	protected virtual void Die() {
		Debug.Log ("morreu um");
		transform.position=new Vector3(0,-10,0);
		GameObject.Find("GameController").GetComponent<GameController>().turrentsCount--;
	}
}
