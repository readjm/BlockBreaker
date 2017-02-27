using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public bool autoplay = false;
	public float minX, maxX;
		
	private Ball ball;
	private float mousePosInBlocks;
	private Vector3 mouseDelta = Vector3.zero;
	private Vector3 lastPos = Vector3.zero;
	 
	// Use this for initialization
	void Start ()
	{
		Cursor.visible = false;
		ball = GameObject.FindObjectOfType<Ball>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		mouseDelta = Input.mousePosition - lastPos;
		lastPos = Input.mousePosition;
				
		if (!autoplay)
		{
			//MoveWithKey();
			MoveWithMouse();
			
		}
		else
		{
			Autoplay();
		}
	}
	
	void MoveWithKey()
	{
		float speed = 20.0f;
		
		var move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
		transform.position += move * speed * Time.deltaTime;
		/*
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			Debug.Log ("Left Arrow Down");
			this.rigidbody2D.AddForce(new Vector2(-10f, 0f));
		}
		else if (Input.GetKeyDown (KeyCode.RightArrow))
		{
			Debug.Log ("Right Arrow Down");
			this.rigidbody2D.AddForce(new Vector2(10f, 0f));
		}
		*/
	}
	void MoveWithMouse()
	{
		Vector3 paddlePos = new Vector3(8.0f, this.transform.position.y, 0.0f);
		mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
		paddlePos.x = Mathf.Clamp(mousePosInBlocks, minX, maxX);
		this.transform.position = paddlePos;
	}
	
	void Autoplay()
	{
		Vector3 paddlePos = new Vector3(0.5f, this.transform.position.y, 0.0f);
		Vector3 ballPos = ball.transform.position;
		paddlePos.x = Mathf.Clamp(ballPos.x, minX, maxX);
		this.transform.position = paddlePos;
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log("Old x: " + ball.GetComponent<Rigidbody2D>().velocity.x);
		float newX = Mathf.Clamp (ball.GetComponent<Rigidbody2D>().velocity.x + mouseDelta.x*0.25f, -10f, 10f);
		ball.GetComponent<Rigidbody2D>().velocity = new Vector2(newX, ball.GetComponent<Rigidbody2D>().velocity.y);
		Debug.Log("mouseDelta x: " + mouseDelta.x);
		Debug.Log("New x: " + newX);
	}
}
