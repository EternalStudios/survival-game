using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItem : MonoBehaviour {

	public GameObject realItem;

	public Material[] materials;

	public bool canPlace;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<Renderer> ().material == materials[0]) {
			canPlace = false;
		} else {
			canPlace = true;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Terrain") {
			gameObject.GetComponent<Renderer> ().material = materials [1];
			if (Input.GetMouseButtonDown (0) && canPlace) {
				GameObject item = Instantiate (realItem, transform.position, transform.rotation);
				Destroy (gameObject);
			}
		} else {
			gameObject.GetComponent<Renderer> ().material = materials [0];
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Terrain") {
			gameObject.GetComponent<Renderer> ().material = materials [0];
		}
	}
}
