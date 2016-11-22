using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour {

	public patrolPath enemyPath;
	private LinkedListNode<Transform> currentPoint;
	private Animator anim;
	private bool facingRight = false;
	public bool standing = false;

	// Use this for initialization
	void Start () {
		currentPoint = enemyPath.path.First;
		anim = GetComponent<Animator> ();
		gameObject.transform.position = enemyPath.path.First.Value.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(!standing)
			walkToNextPoint ();
	}

	void walkToNextPoint(){
		if (gameObject.transform.position == currentPoint.Value.position) {
			StartCoroutine(Stand ());
			if (currentPoint == enemyPath.path.Last)
				currentPoint = enemyPath.path.First;
			else	
				currentPoint = currentPoint.Next;
		}
		else {
			Vector3 moveForward = Vector3.MoveTowards (transform.position, currentPoint.Value.position, .01f);
			if (gameObject.transform.position.x < moveForward.x) {
				if (!facingRight)
					Flip ();
			} else if(facingRight && gameObject.transform.position.x > moveForward.x){
				Flip ();
			}
			anim.SetInteger ("walk", 1);
			gameObject.transform.position = moveForward;
		}
	}

	public IEnumerator Stand(){
		standing = true;
		anim.SetInteger ("walk", 0);
		yield return new WaitForSeconds (3);
		standing = false;
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
