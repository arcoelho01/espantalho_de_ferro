using UnityEngine;
using System.Collections;

public class HealthProjector : MonoBehaviour {

	Projector thisProjector;
	bool bnPulseEnable = false;

	// --------------------
	public float fMaxTime = 0.5f;
	float fTimer = 0.0f;
	public float fMaxDistance = 1.0f;
	float fSize;
	int textureIdx = 0;

	public Material[] healthMaterialsActive = new Material[3];
	public Material[] healthMaterialsInactive = new Material[3];

	Vector3 v3OriginalScale;
	float fOriginalSize;

	// Use this for initialization
	void Start () {
	
		thisProjector = GetComponent<Projector>();
		fOriginalSize = thisProjector.orthographicSize;
		fSize = fOriginalSize;

		if(!bnPulseEnable) {

			thisProjector.orthographicSize = fOriginalSize + fMaxDistance;
		}
		this.thisProjector.material	= (bnPulseEnable == true ? healthMaterialsActive[textureIdx] : healthMaterialsInactive[textureIdx]);
	}
	
	// Update is called once per frame
	void Update () {
	
		if(fTimer >= fMaxTime) {

			fTimer = 0.0f;
		}

		fTimer += Time.deltaTime;

		fSize = fOriginalSize + fTimer * fMaxDistance/fMaxTime;

		if(bnPulseEnable) {

			thisProjector.orthographicSize = fSize;
		}
	}

	/// <summary>
	/// Changes the beats per minute (pulsation of the projector)
	/// </summary>
	public void SetBPM(float fBPM) {

		fMaxTime = 60/fBPM;
	}

	/// <summary> 
	/// Check if is needed to change the health indicator. Health must come in the range [0,1]
	/// </summary>
	public void CheckAndShowHealth(float fHealth) {

		Debug.Log(fHealth);
		// Updates the color of the cursor, changing it's textures
		if(fHealth < .34)  {

			textureIdx = 2;
		}
		else if(fHealth <= .67) {

			textureIdx = 1;
		}
		else {

			textureIdx = 0;
		}

		this.thisProjector.material	= (bnPulseEnable == true ? healthMaterialsActive[textureIdx] : healthMaterialsInactive[textureIdx]);
	}

	/// <summary>
	/// Enables or disables the pulsing. It's needed so it doesn't pulse when the tower is inactive
	/// </summary>
	public void PulseEnable(bool bnState) {

		bnPulseEnable = bnState;

		// If the pulse is disabled, keep max size so it's visible by the player
		if(!bnPulseEnable) {

			thisProjector.orthographicSize = fOriginalSize + fMaxDistance;
		}

		this.thisProjector.material	= (bnPulseEnable == true ? healthMaterialsActive[textureIdx] : healthMaterialsInactive[textureIdx]);
	}
}
