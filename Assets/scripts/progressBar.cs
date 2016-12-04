using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progressBar : MonoBehaviour {

	// Use this for initialization
	float barDisplay = 0;
	public Camera camera;
	Vector2 pos;
	Vector2 size = new Vector2(100,20);
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;

	void OnGUI()
	{
		if (barDisplay > 0) {
			// draw the background:
			GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
			GUI.Box (new Rect (0, 0, size.x, size.y), progressBarEmpty);

			// draw the filled-in part:
			GUI.BeginGroup (new Rect (0, 0, size.x * barDisplay, size.y));
			GUI.Box (new Rect (0, 0, size.x, size.y), progressBarFull);
			GUI.EndGroup ();

			GUI.EndGroup ();
		}
	} 

	void Start()
	{
		pos = camera.WorldToScreenPoint(gameObject.transform.position);
	}

	public void SetBar(float finish ,float progress) {
		float startTime = (finish - 2f);
		barDisplay = (progress - startTime)/(finish - startTime);
	}

	public void ResetBar(){
		barDisplay = 0f;
	}
}
