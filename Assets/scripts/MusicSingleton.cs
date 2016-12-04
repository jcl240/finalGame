using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MusicSingleton : MonoBehaviour
{
	public static MusicSingleton instance;
	public static GameObject musicManager;

	public AudioClip music;

	public void Awake() 
	{
		if ( instance != null && instance != this ) 
		{
			Destroy( this.gameObject );
			return;
		} 
		else 
		{
			instance = this;
			musicManager = this.gameObject;
		}
			
		GetComponent<AudioSource>().clip = music;
		GetComponent<AudioSource>().Play();

		DontDestroyOnLoad( this.gameObject );
	}


	public static MusicSingleton GetInstance()
	{
		return instance;
	}


}
