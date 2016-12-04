using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class levelEnd : MonoBehaviour {

	public GameObject player;
	public SimplePlatformController playerController;
	public Vector3 playerPosition;
	bool playerInRange = false;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("miney_17");
	}
	
	// Update is called once per frame
	void Update () {
		if (playerInRange && Input.GetKeyDown(KeyCode.E)) {
			UseDoor ();
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

	void UseDoor(){
<<<<<<< HEAD
		GameObject.Find ("GameManager").GetComponent<fading> ().LoadStage (SceneManager.GetActiveScene().buildIndex+1);
=======
		if(SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCount)
			GameObject.Find ("/GameManager").GetComponent<fading> ().LoadStage (SceneManager.GetActiveScene().buildIndex+1);
		else
			GameObject.Find ("/GameManager").GetComponent<fading> ().returnToMain();
>>>>>>> origin/master
	}
}
