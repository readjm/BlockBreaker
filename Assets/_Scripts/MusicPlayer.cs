using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	static MusicPlayer instance = null;
	
	public AudioClip[] trackList;

	void Awake ()
	{
		Debug.Log("Music Player Awake " + GetInstanceID());
		
		if (instance != null)
		{
			
			GameObject.Destroy(gameObject);
			Debug.Log ("Destroyed duplicate Music Player");
		}
		else
		{
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
		this.GetComponent<AudioSource>().clip = trackList[Application.loadedLevel];
	}
	
	// Use this for initialization
	void Start ()
	{
		Debug.Log("Music Player Start " + GetInstanceID());
	}
	
	public void PlayTrack(int track)
	{
		Debug.Log ("Level/track #: " + trackList[track]);
		if (this.GetComponent<AudioSource>().clip != trackList[track])
		{
			this.GetComponent<AudioSource>().clip = trackList[track];
			this.GetComponent<AudioSource>().Play();
		}
	}
}
