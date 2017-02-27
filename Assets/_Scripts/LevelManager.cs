using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	
	public static int lives = 2;
	public Text lifeCounter;
	
	public void Start()
	{
        //Screen.lockCursor = true;

        //Screen.fullScreen = true;

        if (lifeCounter != null)
		{
			lifeCounter.text = ("Lives: " + lives.ToString());
		}
		
	}

	public void LoadLevel(string name)
	{
		Debug.Log("Level load requested for : " + name);
		Brick.breakableCount = 0;
		Application.LoadLevel(name);
	}
	
	public void LoadLevel(int level)
	{
		Debug.Log("Level load requested for: " + level);
		Brick.breakableCount = 0;
		GameObject.FindObjectOfType<MusicPlayer>().PlayTrack(level);
		
		if (level >= Application.levelCount)
		{
			Cursor.visible = true;
		}
		Application.LoadLevel(level);
			
	}
	
	public void QuitRequest()
	{
		Debug.Log ("Quit requested.");
		Application.Quit();
	}
	
	public void LoadNextLevel()
	{
		Brick.breakableCount = 0;
		Application.LoadLevel(Application.loadedLevel + 1);
		GameObject.FindObjectOfType<MusicPlayer>().PlayTrack(Application.loadedLevel + 1);		
	}
	
	public void BrickDestroyed()
	{
		if (Brick.breakableCount <= 0)
		{
			LoadNextLevel();
		}
	}
	
	public void LoseBall()
	{
		lives--;
		lifeCounter.text = ("Lives: " + lives.ToString());
		if (lives <= -1)
		{
			Cursor.visible = true;
			lives = 2;
			LoadLevel("Lose");
		}
		else
		{
			GameObject.FindObjectOfType<Ball>().Reset(); 
		}
	}
}