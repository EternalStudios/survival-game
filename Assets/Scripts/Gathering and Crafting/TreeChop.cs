using UnityEngine;
using System.Collections;

public class TreeChop : MonoBehaviour {

	public int rayLength = 10;

	public GameObject hitObject;
	public AudioClip[] hitSounds;
	public AudioClip currenthitSound;

	public GameObject woodHole;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Vector3 fwd = transform.TransformDirection (Vector3.forward);

		if (Physics.Raycast (transform.position, fwd, out hit, rayLength)) {
			if (hit.collider.gameObject.tag == "Tree") {
				if (Input.GetMouseButtonDown (0) && GameObject.FindWithTag ("Player").GetComponent<Player> ().canSwing) {
					hitObject = hit.collider.gameObject;
					StartCoroutine (Hit (hit));
				}
			}
		}
	}

	IEnumerator Hit(RaycastHit hitPoint)
	{
		currenthitSound = hitSounds [Random.Range (0, hitSounds.Length)];
		yield return new WaitForSeconds (GameObject.FindWithTag ("Player").GetComponent<Player> ().animationTime);
		gameObject.GetComponent<AudioSource> ().PlayOneShot (currenthitSound);
		GameObject hole = Instantiate (woodHole, hitPoint.point, Quaternion.LookRotation (hitPoint.normal)) as GameObject;
		hole.transform.SetParent (hitPoint.transform);
		if (hitObject.GetComponent<Tree> ().hitsLeft > 0) {
			hitObject.GetComponent<Tree> ().hitsLeft -= 1;
		} else {
			hitObject.GetComponent<Tree> ().health -= 1;
		}
	}
}
