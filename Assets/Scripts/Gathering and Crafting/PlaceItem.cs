using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceItem : MonoBehaviour {

	public GameObject realItem;

	public Material[] materials;

	public float buildProgress;
	public bool canBuild;

	public bool canPlace;
	public bool isPlaced;

	public int logsNeeded;
	public int twigsNeeded;
	public int stonesNeeded;

	int logsInPlace;
	int twigsInplace;
	int stonesInPlace;

	public GameObject interactText;


	// Use this for initialization
	void Start () {
		interactText = GameObject.Find ("InteractText");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && canPlace && !isPlaced) {
			gameObject.GetComponent<Renderer> ().material = materials [2];
			transform.parent = null;
			isPlaced = true;
		}

		if (buildProgress < 0) {
			buildProgress = 0;
		}

		buildProgress -= Time.deltaTime * 2.0f;

		if (Input.GetKey ("f") && canBuild) {
			buildProgress += 5 * Time.deltaTime;
		}

		if (canBuild && Vector3.Distance (transform.position, GameObject.FindWithTag ("Player").transform.position) < 4) {
			interactText.GetComponent<Text>().text = "Press [E] to build " + gameObject.name + " (" + buildProgress.ToString() + ")";
		}else if (Vector3.Distance(transform.position, GameObject.FindWithTag ("Player").transform.position) > 4){
			interactText.GetComponent<Text> ().text = null;
		}

		if (buildProgress >= 100) {
			GameObject item = Instantiate (realItem, transform.position, transform.rotation);
			Destroy (gameObject);
		}

		if (logsInPlace >= logsNeeded && twigsInplace >= twigsNeeded && stonesInPlace >= stonesNeeded) {
			canBuild = true;
		} else {
			canBuild = false;
		}

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Terrain" && !isPlaced && GameObject.Find("MainCamera").transform.rotation.x > 0 && GameObject.Find("MainCamera").transform.rotation.x > -4) {
			gameObject.GetComponent<Renderer> ().material = materials [1];
			canPlace = true;
		} else if (!isPlaced) {
			gameObject.GetComponent<Renderer> ().material = materials [0];
			canPlace = false;
		}

		if (other.tag == "Wood") {
			logsInPlace += 1;
		} else if (other.tag == "Stone") {
			stonesInPlace += 1;
		} else if (other.tag == "Twig") {
			twigsInplace += 1;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Terrain" && !isPlaced && GameObject.Find("MainCamera").transform.rotation.x < 0f) {
			gameObject.GetComponent<Renderer> ().material = materials [1];
			canPlace = true;
		} else if (!isPlaced) {
			gameObject.GetComponent<Renderer> ().material = materials [0];
			canPlace = false;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Terrain" && !isPlaced) {
			gameObject.GetComponent<Renderer> ().material = materials [0];
			canPlace = false;
		}

		if (other.tag == "Wood") {
			logsInPlace -= 1;
		} else if (other.tag == "Stone") {
			stonesInPlace -= 1;
		}else if (other.tag == "Twig") {
			twigsInplace -= 1;
		}
	}
}
