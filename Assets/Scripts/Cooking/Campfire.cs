using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Campfire : MonoBehaviour {

	public float fuel;
	public GameObject fire; //Empty parent object met particle effect en point light van campfire;
	public bool isBurning = false;

	public float heat = 0;

	public GameObject interactText; //Ander text object dan bij pickup.cs

	// Use this for initialization
	void Start () {
		interactText = GameObject.Find ("InteractText");
	}
	
	// Update is called once per frame
	void Update () {

		if (heat < 0) {
			heat = 0;
		} 

		heat -= Time.deltaTime * 10f / (Weather.temperature / (20f * Weather.humidity)); //Makes it harder to make a fire in cold weather;

		if (Vector3.Distance (transform.position, GameObject.FindWithTag ("Player").transform.position) < 3) {
			if (Input.GetKeyDown ("e") && !isBurning) {
				heat += 3.5f;
				//Play animation

			}
			if (Pickup.objectsCloseTo == 0 && !isBurning && fuel > 0) {
				interactText.GetComponent<Text> ().text = "Press [E] to heat up the fire (" + ((int)heat) + "%)";
			} else if (isBurning || Pickup.objectsCloseTo > 0 && isBurning) {
				interactText.GetComponent<Text> ().text = null;
			} else if (fuel == 0 && Pickup.objectsCloseTo == 0) {
				interactText.GetComponent<Text> ().text = "Campfire needs fuel (wood) to burn.";
			} else if (Pickup.objectsCloseTo > 0) {
				interactText.GetComponent<Text> ().text = null;
			}
		} else if (Vector3.Distance (transform.position, GameObject.FindWithTag ("Player").transform.position) > 3) {
			interactText.GetComponent<Text> ().text = null;
		}

		if (heat >= 100) {
			{
				heat = 0;
				Burn ();
				if (fuel == 0) {
					//Warning message: "A campfire needs fuel to burn";
				}
			}
		}

			if (fuel > 0) {
				if (isBurning) {
					fuel -= 0.5f * Time.deltaTime;
					Burn ();
				}
			} else if (fuel == 0) {
				fire.SetActive (false);
				isBurning = false;
			}

			if (fuel < 0) {
				fuel = 0;
			} 
	
		}

	void OnTriggerEnter(Collider other)
	{

	}

	void OnTriggerStay(Collider other)
	{

		if (other.tag == "Wood" && fuel < 50 && !other.gameObject.GetComponent<Pickup>().pickedUp) {
			fuel += 40.0f * other.transform.localScale.x;
			Destroy (other.gameObject);
			if (other.gameObject.GetComponent<Pickup> ().closeTo) {
				Pickup.objectsCloseTo -= 1;
			}

		}else if (other.tag == "Twig" && fuel < 50 & !other.GetComponent <Pickup> ().pickedUp) {
			fuel += 5.0f;
			Destroy (other.gameObject);
		}

		if (isBurning) {

			if (other.tag == "Player") {
				other.gameObject.GetComponent<Player> ().GetDamage (5f * Time.deltaTime);
			}

			if (other.tag == "Food") {
				if (!other.gameObject.GetComponent<Pickup> ().pickedUp) {
					other.gameObject.GetComponent<Food> ().cookTimer -= Time.deltaTime;
				}
			}

		}
	}

	void Burn()
	{
		fire.SetActive (true);
		isBurning = true;
	}
}
