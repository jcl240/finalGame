﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class progressBar : MonoBehaviour {

	// Use this for initialization
	float barDisplay = 0;
	public Camera camera;
	Vector2 pos;
	Vector2 size = new Vector2(100,20);
	Texture2D progressBarEmpty;
	Texture2D progressBarFull;

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
		// for this example, the bar display is linked to the current time,
		// however you would set this value based on your desired display
		// eg, the loading progress, the player's health, or whatever.
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
