using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour {

	public GameObject[] items;
	public Transform instPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Campfire()
	{
		GameObject campfire = Instantiate (items [0], instPosition.position, instPosition.rotation);
		campfire.transform.SetParent (GameObject.FindWithTag ("Player").transform.FindChild ("MainCamera").transform);
	}
}
