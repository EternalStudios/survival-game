using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float health = 100;
	public float hunger = 150;
	public float thirst = 250;

	public bool canSwing = true;
	public float animationTime = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		hunger -= Time.deltaTime * 0.1f;
		thirst -= Time.deltaTime * 0.35f;

		if (hunger < 0) {
			hunger = 0;
		}

		if (health < 0) {
			health = 0;
		}

		if (thirst < 0) {
			thirst = 0;
		}

		if (hunger == 0) {
			health -= 0.2f * Time.deltaTime;
		}

		if (thirst == 0) {
			health -= 0.4f * Time.deltaTime;
		}

		if (Input.GetMouseButtonDown (0) && canSwing && transform.FindChild("Axe") != null) {
			StartCoroutine (Swing ());
		}
	}

	public void GetDamage(float damage)
	{
		health -= damage;
	}

	IEnumerator Swing()
	{
		transform.FindChild ("Axe").gameObject.GetComponent<Animation> ().Play ("AxeSwing");
		canSwing = false;
		yield return new WaitForSeconds (animationTime);
		canSwing = true;
	}
}
