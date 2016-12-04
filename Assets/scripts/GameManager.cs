using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public enum GameState{
		Playing, Ended
	}

	private static GameManager _instance;

	GameObject stunObject;
	public Text stunnersText;
	GameObject loseGameGraphics;

	public static GameState State = GameState.Playing;

	public static float timeStarted;
	public int stunners;
	private static int points;
	static float timeScale;
	public static bool paused = false;


	void Awake(){
		timeScale = Time.timeScale;
		State = GameState.Playing;
		_instance = this;
		_instance.stunObject = GameObject.Find ("/Canvas/stunnersText");
		if (_instance.stunObject != null) {
			_instance.stunnersText = _instance.stunObject.GetComponent<Text> ();
			_instance.stunnersText.text = stunners.ToString ();
		}
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();
	}

	void Start(){
		_instance.SetPoints ();
	}

	public void SetPoints(){
		GameObject.Find ("/Canvas/quorkleText").GetComponent<Text>().text = points.ToString();
	}

	public static void AddPoints(){
		points += 1;
		_instance.SetPoints ();
	}

	public static void RemoveStun(){
		_instance.stunners-= 1;
		_instance.stunnersText.text = _instance.stunners.ToString();
	}

	public static bool HasStunners(){
		return _instance.stunners!= 0;
	}

	public static int GetPoints(){
		return points;
	}

	public void StartTimer(){
		timeStarted = Time.time;
	}
		

	public void PauseButton(){
		Debug.Log ("Pressed");
		if (paused)
			UnPause ();
		else
			Pause ();
	}

	public static void Pause(){
		Time.timeScale = 0;
		paused = true;
	}

	public static void UnPause(){
		Time.timeScale = timeScale;
		paused = false;
	}
}
