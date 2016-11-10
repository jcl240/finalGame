using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class levelEnd : MonoBehaviour {

	public GameObject player;
	public SimplePlatformController playerController;
	public Vector3 playerPosition;


	// Use this for initialization
	void Start () {
		player = GameObject.Find ("/hero");
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<BoxCollider2D>().IsTouching(player.GetComponent<BoxCollider2D>()) && Input.GetKeyDown(KeyCode.E)) {
			UseDoor ();
		}
	}

	void UseDoor(){
		GameObject.Find ("/GameManager").GetComponent<fading> ().LoadStage (SceneManager.GetActiveScene().buildIndex+1);
	}
}
