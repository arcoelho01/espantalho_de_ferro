using UnityEngine;
using System.Collections;

public class HealthProjector : MonoBehaviour {

	Projector thisProjector;
	float fHealth = 1;

	// --------------------
	public float fMaxTime = 0.5f;
	float fTimer = 0.0f;
	public float fMaxDistance = 1.0f;
	float fSpeed;
	float fSize;
	float fDuration = 1;

	public Material[] healthMaterials = new Material[3];

	Vector3 v3OriginalScale;
	float fOriginalSize;

	// Use this for initialization
	void Start () {
	
		thisProjector = GetComponent<Projector>();
		fOriginalSize = thisProjector.orthographicSize;
		fSize = fOriginalSize;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(fTimer >= fMaxTime) {

			fTimer = 0.0f;
		}

		fTimer += Time.deltaTime;

		fSize = fOriginalSize + fTimer * fMaxDistance/fMaxTime;
		thisProjector.orthographicSize = fSize;

		// Updates the color of the cursor, changing it's textures
		if(fHealth < .33)  {

			this.thisProjector.material = healthMaterials[2];
		}
		else if(fHealth <= .66) {

			this.thisProjector.material = healthMaterials[1];
		}
		else {

			this.thisProjector.material = healthMaterials[0];
		}
	}
}
