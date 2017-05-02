using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	public enum stages { raw, cooked, burnt };

	public float cookTimer = 30f;
	public stages stage;

	#region Colors
	public Color32 raw;
	public Color32 cooked;
	public Color32 burnt;

	#endregion



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (cookTimer < 0) {

			cookTimer = 0;
		}

			
		if (stage == stages.raw) {
			gameObject.GetComponent<Renderer> ().material.color = raw;
		}

		if (stage == stages.cooked) {
			gameObject.GetComponent<Renderer> ().material.color = cooked;
		}

		if (stage == stages.burnt) {
			gameObject.GetComponent<Renderer> ().material.color = burnt;
		}

			if (cookTimer == 0) {
			if (stage == stages.raw) {
				stage = stages.cooked;
				cookTimer = 30f;
			} else if (stage == stages.cooked) {
				stage = stages.burnt;
				cookTimer = 30f;
			} else if (stage == stages.burnt) {
				Destroy (gameObject);			
				if (gameObject.GetComponent<Pickup> ().closeTo) {
					Pickup.objectsCloseTo -= 1;
				}

				//Instantiate charcoal or ash;
			}
			}
}
}
