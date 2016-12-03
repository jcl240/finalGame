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
	private bool alive;


	private bool grounded = false;
	private Animator anim;
	private Rigidbody rb;

	private minable closestQorkle;
	private float miningProgress;


	// Use this for initialization
	void Awake ()
	{
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
		alive = true;
		miningProgress = 0;
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
		float v = Input.GetAxis ("Vertical");

		if (h == 0 && Input.GetKeyDown (KeyCode.E) && closestQorkle != null)
			StartCoroutine(Mine ());
		else {
			miningProgress = 0;
			Movement (h,v);
		}

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

	void Movement(float h, float v){
		if (h == 0 && v == 0)
			anim.SetInteger ("run", 0);
		else
			anim.SetInteger ("run", 1);

		if(h < 0.001f && h > -0.001f){
			rb.velocity = new Vector3 (0,rb.velocity.y, rb.velocity.z);
		}

		if (h * rb.velocity.x < maxSpeed) {
			rb.AddForce (Vector3.left * h * moveForce);
		}

		if (Mathf.Abs (rb.velocity.x) > maxSpeed) {
			rb.velocity = new Vector3 (Mathf.Sign (rb.velocity.x) * maxSpeed, rb.velocity.y, rb.velocity.z);
		}

		if(v < 0.001f && v > -0.001f){
			rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
		}

		if (h * rb.velocity.z < maxSpeed) {
			rb.AddForce (Vector3.back * v * moveForce);
		}

		if (Mathf.Abs (rb.velocity.z) > maxSpeed) {
			rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y, Mathf.Sign (rb.velocity.z) * maxSpeed);
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

	public void SetQorkleInRange(minable qorkle){
		closestQorkle = qorkle;
		Debug.Log ("enterRange");
	}

	public void RemoveQorkle(minable qorkle){
		closestQorkle = null;
		Debug.Log ("exitRange");
	}

	IEnumerator Mine(){
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		if (miningProgress < 2 && h==0 && v==0 && Input.GetKeyDown (KeyCode.E)) {
			h = Input.GetAxis ("Horizontal");
			v = Input.GetAxis ("Vertical");
			miningProgress += .5f;
			Debug.Log (miningProgress);
		}
		if(miningProgress >= 2)
			closestQorkle.FinishMining ();
		yield return new WaitForSeconds (.5f);
		StartCoroutine (Mine());
	}

}