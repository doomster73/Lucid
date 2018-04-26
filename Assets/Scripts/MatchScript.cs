using UnityEngine;
using System.Collections;

public class MatchScript : MonoBehaviour {
	
	public enum Directions
	{
			Left,
			Right,
			Up,
			Down
	}
	
	Transform matchTransform;
	Transform target;
	
	float verticalMovement; //holds the vertical movement for use in jumps
	
	Vector3 matchMovement;
	
	public float throwPower = 20f; //public var jump force
	float gravityPower = 30f; //public var gravity force
	
	public float Speed = 60f;
	float Timer = 5.0f;
	
	Transform player;
	
	// Use this for initialization
	void Start () {
		verticalMovement = throwPower;
		matchTransform = transform;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		
		//target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		Timer -= Time.deltaTime;

		if(Timer <= 0)
		{
			Destroy(gameObject);
			//Debug.Log("Finished");
		}
		else
		{
			matchTransform.position += matchTransform.TransformDirection(-Speed ,verticalMovement,0) * Time.deltaTime;
			Gravity();
		}
	}
	
	void Throw()
	{
			verticalMovement = throwPower;	
	}
	
	void Gravity()
	{
		//if character is not already on the ground
			verticalMovement -= gravityPower * Time.deltaTime;
			if(verticalMovement < -50)
			{
				verticalMovement = -50;	
			}

	}
	
	void OnTriggerEnter(Collider triggerCollider)
	{
		if(triggerCollider.tag == "MatchColliderTag")
		{
			//Debug.Log("Hit Cauldron : " + triggerCollider.collider.name + " Name : " + triggerCollider.gameObject.name);
			Destroy(gameObject);
			
			
			//GameObject cauldron = GameObject.FindGameObjectWithTag("CauldronLight");
			//Debug.Log("Light Type : " + cauldron.light.type);
		}
	}
}
