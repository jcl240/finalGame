using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SimplePlatformController : MonoBehaviour
{

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public Transform groundCheck;
	private string currentPage;
	private bool alive;
	public LayerMask backMask;


	private bool grounded = false;
	private Animator anim;
	private Rigidbody rb;


	// Use this for initialization
	void Awake ()
	{
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
		alive = true;
	}

	// Update is called once per frame
	void Update ()
	{
//		if (gameObject.transform.localPosition.y < GameManager.deathHeight && alive == true)
//			die ();
		grounded = Physics.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
		}
	}

	void FixedUpdate ()
	{
		float h = Input.GetAxis ("Horizontal");

		if (h == -1)
			anim.SetInteger ("run", 2);
		else if (h == 0)
			anim.SetInteger ("run", 0);
		else
			anim.SetInteger ("run", 1);

		if (h > 0 && !facingRight) {
			Flip ();
		} else if (h < 0 && facingRight) {
			Flip ();
		}

		if (jump) {
			anim.ResetTrigger ("land");
			anim.SetTrigger ("jump");
			rb.AddForce (new Vector2 (0f, jumpForce));
			jump = false;
			StartCoroutine (Land ());
		}
	}

	public IEnumerator Land(){
		yield return new WaitForSeconds (.5f);
		if (grounded) {
			anim.ResetTrigger ("jump");
			anim.SetTrigger ("land");
		}
		else {
			StartCoroutine (Land ());
		}
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void die(){
		Camera cam = gameObject.GetComponentInChildren<Camera> ();
		cam.transform.SetParent (null);
		alive = false;
		if (false) //ADD LOSE GAME HERE
			GameObject.Find("GameManager").GetComponent<GameManager>().LoseTheGame ();
		else
			StartCoroutine(reload ());
	}

	IEnumerator reload(){
		yield return new WaitForSeconds (2);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

}