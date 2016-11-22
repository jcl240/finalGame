using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolPath : MonoBehaviour {

	public LinkedList<Transform> path = new LinkedList<Transform>();

	// Use this for initialization
	void Start () {
		foreach (Transform point in gameObject.GetComponentsInChildren<Transform> ()) {
			if (point != gameObject.transform) {
				point.position += new Vector3 (0, .25f, 0);
				path.AddLast (point);
				Debug.Log (point.position);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
