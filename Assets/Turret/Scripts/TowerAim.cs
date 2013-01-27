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

	public float fHealth = 30.0f;
	public Transform trBulletSpawnPoint;

	// Heartbeat stuff
	public float fMaxHeartBeatTime = 0.5f;
	float fHeartBeatTimer = 0.0f;
	public float fMaxHeartBeatDistance = 1.0f;
	public float fMaxBeatsPerMinute = 150.0f;
	public float fMinBeatsPerMinute = 50.0f;
	public float fBeatsPerMinute;

	public Transform trHealthProjector;
	Projector pHealthProjector;
	HealthProjector scriptHealthProjector;
	
	Quaternion startRotation;
	
	// Use this for initialization
	void Start () {
	
		fCameraDif = Camera.main.transform.position.y - transform.position.y;
		fFrequency = fMinFrequency;

		fBeatsPerMinute = fMinBeatsPerMinute;

		if(!trCannon) {

			// DEBUG
			Debug.LogError("Cannon not defined in the inspector");
		}

		myColor = trCannon.gameObject.renderer.material.color;

		if(!trHealthProjector) {

			// DEBUG
			Debug.LogError("Projector for health not defined in the inspector");
		}
		else {

			pHealthProjector = trHealthProjector.gameObject.GetComponent<Projector>();
			scriptHealthProjector = trHealthProjector.GetComponent<HealthProjector>();
		}

		startRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if(bnTowerIsActive!=lastActiveState){
			lastActiveState=bnTowerIsActive;
		}

		if(!bnTowerIsActive && transform.rotation != startRotation) {

			transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, Time.deltaTime * 5.0f);
		}

		// Check for input and update frequency
		if(bnTowerIsActive) {

			// Reads the mouse
			if(Input.GetButton("Fire1")) {

				fFrequency += fFrequencyIncrease * Time.deltaTime;
				fBeatsPerMinute += 75.0f * Time.deltaTime;

				scriptHealthProjector.SetBPM(fBeatsPerMinute);
			}
			else {
				// Perform the cooldown
				fFrequency -= fFrequencyDecrease * Time.deltaTime;
				fBeatsPerMinute -= 50 * Time.deltaTime;

				scriptHealthProjector.SetBPM(fBeatsPerMinute);
			}

			fFrequency = Mathf.Clamp(fFrequency, fMinFrequency, fMaxFrequency);
			fBeatsPerMinute = Mathf.Clamp(fBeatsPerMinute, fMinBeatsPerMinute, fMaxBeatsPerMinute);
			
			// ----------
			// BEAT
			// ---------
			
			// Updates the timer
			fHeartBeatTimer += Time.deltaTime;

			// Heart beats is in BPMs. How many beats per second? Do 60/BPM
			if(fHeartBeatTimer >= 60/fBeatsPerMinute) {

				// BEAT!
				fHeartBeatTimer = 0.0f;

				Shoot();
			}	
			


			// ----------------


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
					//Shoot();
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
			if(freqRate>=1 || Input.GetMouseButtonDown(1)){

				PlayerOut(freqRate);
			}
			//if(Input.GetMouseButtonDown(1)){
			//	bnTowerIsActive=false;
			//	heart.speed=1+(10*(1-freqRate));
			//	heart.gameObject.renderer.enabled=true;
			//	heart.gameObject.audio.clip=heart.jumpOutSound;
			//	heart.gameObject.audio.Play();

			//	if(scriptHealthProjector) {

			//		scriptHealthProjector.PulseEnable(bnTowerIsActive);
			//	}
			//}
		}

		
	}

	void OnGUI() {
		if(bnTowerIsActive){
			string stText = "Beat: " + String.Format("{0:0.00}", fFrequency) + "\n" 
				+ String.Format("{0:0.0}", fCannonTimer + "\n" 
						+ String.Format("{0:0.0}", fBeatsPerMinute)
						);
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

			if(scriptHealthProjector) {

				scriptHealthProjector.PulseEnable(bnTowerIsActive);
			}
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

		if(scriptHealthProjector) {

			scriptHealthProjector.CheckAndShowHealth(fHealth/30);
		}

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
		PlayerOut(1.0f);

		// Player is here? Get out!
	}


	/// <summary>
	/// Forces the player out of the tower.
	/// </summary>
	void PlayerOut(float freqRate) {

				bnTowerIsActive=false;
				heart.speed=1+(10*(1-freqRate));
				heart.gameObject.renderer.enabled=true;
				heart.gameObject.audio.clip=heart.jumpOutSound;
				heart.gameObject.audio.Play();

				if(scriptHealthProjector) {

					scriptHealthProjector.PulseEnable(bnTowerIsActive);
				}
	}
}
