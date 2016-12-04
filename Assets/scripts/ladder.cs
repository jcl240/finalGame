using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder : MonoBehaviour {

	GameObject playerObject;
	bool canClimb = false;
	float speed = 1;

	void Start () {

		playerObject = GameObject.Find("miney_17");
	}

	void OnCollisionEnter (Collision collider){

		if(collider.gameObject == playerObject){

			canClimb = true;
			playerObject.GetComponent<Rigidbody>().useGravity = false;

		}
	}

	void OnCollisionExit (Collision collider){

		if(collider.gameObject == playerObject){
			canClimb = false;
			playerObject.GetComponent<Rigidbody>().useGravity = true;

		}
	}

	void Update () {

		if(canClimb == true){
			if(Input.GetAxis("Horizontal") > 0 && gameObject.transform.position.x < playerObject.transform.position.x
				|| Input.GetAxis("Horizontal") < 0 && gameObject.transform.position.x > playerObject.transform.position.x
				|| Input.GetAxis("Vertical") > 0 && gameObject.transform.position.z < playerObject.transform.position.z
				||Input.GetAxis("Vertical") < 0 && gameObject.transform.position.z > playerObject.transform.position.z
			){
				
				playerObject.transform.position += (new Vector3(0,1,0) * Time.deltaTime*speed);

			}
		}
	}
}
