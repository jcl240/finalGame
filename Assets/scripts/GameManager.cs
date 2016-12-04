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

	void Awake(){
		State = GameState.Playing;
		_instance = this;
		_instance.stunObject = GameObject.Find ("/Canvas/stunnersText");
		if (_instance.stunObject != null) {
			_instance.stunnersText = _instance.stunObject.GetComponent<Text> ();
			_instance.stunnersText.text = stunners.ToString ();
		}
	}

	void Start(){
		_instance.setPoints ();
	}

	public void setPoints(){
		GameObject.Find ("/Canvas/quorkleText").GetComponent<Text>().text = points.ToString();
	}

	public static void AddPoints(){
		points += 1;
		_instance.setPoints ();
	}

	public static void removeStun(){
		_instance.stunners-= 1;
		_instance.stunnersText.text = _instance.stunners.ToString();
	}

	public static bool hasStunners(){
		return _instance.stunners!= 0;
	}

	public static int getPoints(){
		return points;
	}

	public void startTimer(){
		timeStarted = Time.time;
	}
		
}
