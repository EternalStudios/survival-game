﻿using UnityEngine;
using System.Collections;

public class Weather : MonoBehaviour {

	public static float temperature = 20.0f;
	public static float humidity = 1.0f; //1 = normal, 1.25 = moist, 1.75 = rainy;

	public bool isRaining = false;

	public float rainChance;
	public float rainTimer = 180;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (humidity);
		Debug.Log (temperature);

		if (isRaining) {
			humidity = 1.75f;
			rainTimer -= Time.deltaTime;
		} else {
			//FIXME: Humidity is 1 or 2 depending on the weather;
		}

		if (rainTimer < 0) {
			rainTimer = 0;
		}
		if (rainTimer == 0) {
			isRaining = false;
		}

		rainChance = Random.Range (0f, 100f);

		if (rainChance > 99.8f) {
			isRaining = true;
		}
	}
}
