using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	public static int breakableCount = 0;
	public Sprite[] hitSprites;
	public AudioClip hitSound;
	public GameObject smoke;
	public PowerUp powerUp;
	
	private bool isBreakable;
	private bool isPowerUp = false;
	private int timesHit;
	private LevelManager levelManager;
	
	// Use this for initialization
	void Start ()
	{
		isBreakable = (this.tag != "Unbreakable");
		if (isBreakable)
		{
			breakableCount++;
		}
		
		isPowerUp = (this.tag == "PowerUp");
		
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		timesHit = 0;
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		AudioSource.PlayClipAtPoint(hitSound, this.transform.position);
		if (isBreakable)
		{
			HandleHits();
		}		
	}
	
	void HandleHits()
	{
		timesHit++;
		
		int maxHits = hitSprites.Length + 1;
				
		if (timesHit >= maxHits)
		{
			PuffSmoke();
			breakableCount--;
			levelManager.BrickDestroyed();
			
			if (isPowerUp)
			{
				SpawnPowerUp();	
			}
			
			GameObject.Destroy(gameObject);
		}
		else
		{
			LoadSprites();
		}
	}
	
	void PuffSmoke()
	{
		GameObject puff = (GameObject)Object.Instantiate(smoke, this.transform.position, Quaternion.identity);
		puff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	void LoadSprites()
	{
		int spriteIndex = timesHit - 1;
		if (hitSprites[spriteIndex])
		{
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit - 1];
		}
		else
		{
			Debug.LogError("Brick sprite missing");
		}
	}
	
	void SimulateWin()
	{
		levelManager.LoadNextLevel();
	}
	
	void SpawnPowerUp()
	{
		Object.Instantiate(powerUp, this.transform.position, Quaternion.identity);		
	}
}
