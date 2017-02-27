using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	
	public AudioClip hitSound;
	
	private Paddle paddle;
	private bool hasStarted = false;
	private Vector3 paddleToBallVector;
	// Use this for initialization
	
	void Start ()
	{
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBallVector = this.transform.position - paddle.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (!hasStarted)
		{
			//Lock ball relative to paddle
			this.transform.position = paddle.transform.position + paddleToBallVector;
						
			//Launch ball on left mouse click
			if (Input.GetMouseButtonDown(0))
			{
				hasStarted = true;
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 10f); 
			}
		}
		
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (hasStarted)
		{
			AudioSource.PlayClipAtPoint(hitSound, this.transform.position);
			this.GetComponent<Rigidbody2D>().velocity = new Vector3(Mathf.Clamp(this.GetComponent<Rigidbody2D>().velocity.x, -10, 10), Mathf.Clamp(this.GetComponent<Rigidbody2D>().velocity.y, -10, 10), 0f);	
			Vector2 tweak = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
			this.GetComponent<Rigidbody2D>().velocity += tweak;
		}
		Debug.Log ("Ball velocity: " + this.GetComponent<Rigidbody2D>().velocity);
	}
	
	public void Reset()
	{
		this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		hasStarted = false;
	}
}
