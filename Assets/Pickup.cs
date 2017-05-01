using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pickup : MonoBehaviour {

	public Transform hand;
	public GameObject pickupText;

	public bool closeTo = false;
	public static int objectsCloseTo = 0;

	public bool pickedUp = false;

	// Use this for initialization
	void Start () {
		if (hand == null) {
			hand = GameObject.Find ("Hand").transform;
		}

		if (pickupText == null) {
			pickupText = GameObject.Find ("PickUp");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (pickedUp) {
			transform.parent = GameObject.FindWithTag ("Player").transform.FindChild ("MainCamera").transform;
			transform.parent = GameObject.FindWithTag ("Player").transform;
			transform.position = hand.position;
			gameObject.GetComponent<Rigidbody> ().useGravity = false;
			gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			pickupText.GetComponent<Text> ().text = "";
		} else {
			transform.parent = null;
			gameObject.GetComponent<Rigidbody> ().useGravity = true;
			gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		}


		if (Vector3.Distance (transform.position, GameObject.FindWithTag ("Player").transform.position) < 2) {
			if (!closeTo) {
				objectsCloseTo += 1;
				closeTo = true;
			}

			if (!pickedUp) {
				if (gameObject.GetComponent<Food> () != null) {
					pickupText.GetComponent<Text> ().text = "Press [E] to pick up " + gameObject.GetComponent<Food> ().stage.ToString () + " " + gameObject.name;
				} else {
					pickupText.GetComponent<Text> ().text = "Press [E] to pick up " + gameObject.name;
				}
			} else if (objectsCloseTo == 0) {
				pickupText.GetComponent<Text> ().text = "";
			}
			if (Input.GetKeyDown ("e") && GameObject.FindWithTag ("MainCamera").transform.childCount == 0) {
				pickedUp = !pickedUp;
			}
		} else if (closeTo) {
			closeTo = false;
			objectsCloseTo -= 1;
		} else if (objectsCloseTo == 0) {
			pickupText.GetComponent<Text> ().text = "";
		}
}
}
