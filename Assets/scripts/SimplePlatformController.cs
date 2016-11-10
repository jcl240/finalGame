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
	private Rigidbody2D rb2d;


	// Use this for initialization
	void Awake ()
	{
		gameObject.GetComponentInChildren<Camera> ().cullingMask = backMask;
		currentPage = "frontPage";
		Physics2D.IgnoreLayerCollision (14,15,true);
		Physics2D.IgnoreLayerCollision (9,15,true);
		Physics2D.IgnoreLayerCollision (9,14,false);
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
		alive = true;
	}

	// Update is called once per frame
	void Update ()
	{
		if (gameObject.transform.localPosition.y < GameManager.deathHeight && alive == true)
			die ();
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer (currentPage));

		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
		}
	}

	void FixedUpdate ()
	{
		float h = Input.GetAxis ("Horizontal");

		if (currentPage == "backPage")
			h *= -1;

		if(h < 0.001f && h > -0.001f){
			rb2d.velocity = new Vector2(0,rb2d.velocity.y);
		}

		anim.SetFloat ("Speed", Mathf.Abs (h));

		if (h * rb2d.velocity.x < maxSpeed) {
			rb2d.AddForce (Vector2.right * h * moveForce);
		}

		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
			rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		}

		if (h > 0 && !facingRight) {
			Flip ();
		} else if (h < 0 && facingRight) {
			Flip ();
		}

		if (jump) {
			anim.SetTrigger ("Jump");
			rb2d.AddForce (new Vector2 (0f, jumpForce));
			jump = false;
		}
	}


	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void changePage(){
		if (currentPage == "frontPage") {
			currentPage = "backPage";
			Physics2D.IgnoreLayerCollision (9, 14, true);
			Physics2D.IgnoreLayerCollision (9, 15, false);
		} else {
			currentPage = "frontPage";
			Physics2D.IgnoreLayerCollision (9,15,true);
			Physics2D.IgnoreLayerCollision (9, 14, false);
		}
	}

	public string getPage(){
		return currentPage;
	}

	void die(){
		Camera cam = gameObject.GetComponentInChildren<Camera> ();
		cam.transform.SetParent (null);
		hearts.removeLife ();
		alive = false;
		if (hearts.lives == 0) 
			GameObject.Find("GameManager").GetComponent<GameManager>().LoseTheGame ();
		else
			StartCoroutine(reload ());
	}

	IEnumerator reload(){
		yield return new WaitForSeconds (2);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

}