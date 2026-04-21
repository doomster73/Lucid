using UnityEngine;
using System.Collections;

public class ElevatorScript : MonoBehaviour 
{
	public enum Axis
	{
		x,
		y
	}
	
	public float yTop;
	public float yBottom;
	public bool isUp;
	//Vector3 eVelocity;
	//public float yForce;
	//public float Speed;
	//public Axis orbAxis;
	
	// Use this for initialization
	void Start () 
	{
		isUp = false;
		//yTop 	= -45.0f;
		//yBottom = -68.0f;
		//yForce = -2.0f;
		//Speed = 10.0f;
		//eVelocity = new Vector3(0, yForce, 0);
		
	}
	
	// Update is called once per frame
	void Update () 
	{		
		//gameObject.rigidbody.AddForce(eVelocity);
		/*if(orbAxis == Axis.y)
		{
			if(gameObject.transform.position.y <= -10.0f)
				gameObject.rigidbody.AddForce(0, 10, 0);
			
			if(gameObject.transform.position.y >= 10.0f)
				gameObject.rigidbody.AddForce(0, -10, 0);
		}
		else if(orbAxis == Axis.x)
		{
			if(gameObject.transform.position.x <= -5.0f)
				gameObject.rigidbody.AddForce(Speed, 0, 0);
			
			if(gameObject.transform.position.x >= 5.0f)
				gameObject.rigidbody.AddForce(-Speed, 0, 0);
		}*/
		
		
		if(gameObject.transform.position.y <= yBottom)
			gameObject.GetComponent<Rigidbody>().AddForce(0, 90, 0);
			
		if(gameObject.transform.position.y >= yTop)
			gameObject.GetComponent<Rigidbody>().AddForce(0, -90, 0);
		
		/*if(isUp == false)
		{
			direction = elDirection.down;	
		}
		else if(isUp == true)
		{
			direction = elDirection.up;	
		}
		
		if(direction == elDirection.up)
		{
			gameObject.rigidbody.AddForce(0, 2, 0);	
		}
		else if(direction == elDirection.down)
		{
			gameObject.rigidbody.AddForce(0, -2, 0);	
		}*/
		
	}
	
	/*void OnTriggerEnter()
	{
		if(yForce == 2.0f)
			yForce = -2.0f;
		else if(yForce == -2.0f)
			yForce = 2.0f;
		/*if(isUp == true)
			yForce = 2.0f;
		else if(isUp == false)
			yForce = -2.0f;
	}*/
}
