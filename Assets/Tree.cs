using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour {

	public int hitsLeft = 5;

	public int health = 3;

	public Transform logPrefab;

	public AudioClip falling;
	public AudioClip breaking;

	public bool hasFallen = false;

	public int speed = 8;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody> ().isKinematic = true;
	}

	// Update is called once per frame
	void Update () {
		if (hitsLeft <= 0 && !hasFallen) {
			gameObject.GetComponent<MeshCollider> ().convex = true;
			gameObject.GetComponent<Rigidbody> ().isKinematic = false;
			gameObject.GetComponent<Rigidbody> ().AddForce (transform.up * speed);
			hasFallen = true;
			gameObject.GetComponent<AudioSource> ().PlayOneShot (falling);
		}

		if (health <= 0) {
			GameObject log = Instantiate (logPrefab, transform.position, transform.rotation) as GameObject;
			GameObject log2 = Instantiate (logPrefab, transform.position, transform.rotation) as GameObject;
			GameObject log3 = Instantiate (logPrefab, transform.position, transform.rotation) as GameObject;
			gameObject.GetComponent<AudioSource> ().PlayOneShot (breaking);
			Destroy (gameObject);
		}
	}
}
