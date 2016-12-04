using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightDetector : MonoBehaviour {

	bool playerInRange = false;
	public LayerMask ignoreCone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		if (playerInRange)
			FindPlayer ();
	}

	void FindPlayer(){
		Vector3 player = GameObject.Find ("miney_17").transform.position;
		Vector3 direction = player - gameObject.transform.parent.position;
		RaycastHit hit;
		Ray ray = new Ray (gameObject.transform.parent.position, direction);
		Physics.Raycast (ray, out hit, Vector3.Distance (gameObject.transform.parent.position, player), ignoreCone);
		if (hit.collider.tag == "Player") {
			GameObject.Find ("miney_17").GetComponentInParent<Animator> ().SetTrigger ("foundya");
			Destroy(gameObject.transform.parent.GetComponentInParent<patrol> ());
			GameObject.Find ("miney_17").GetComponent<SimplePlatformController> ().Die ();
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player")
			playerInRange = true;
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player")
			playerInRange = false;
	}

}
