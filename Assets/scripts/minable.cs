using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minable : MonoBehaviour {

	SimplePlatformController player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("miney_17").GetComponent<SimplePlatformController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			player.SetQorkleInRange (this);
		}

	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			player.RemoveQorkle (this);
		}
	}

	public void FinishMining(){
		Debug.Log ("Finished mining");
		Destroy (gameObject);
	}
}
