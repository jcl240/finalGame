using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightDetector : MonoBehaviour {

	bool playerInRange = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		if (playerInRange)
			findPlayer ();
	}

	void findPlayer(){
		Vector3 player = GameObject.Find ("miney").transform.position;
		Vector3 direction = player - transform.position;
		if (Physics.Raycast (transform.position, direction, 2))
			Debug.Log ("Found him");
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
