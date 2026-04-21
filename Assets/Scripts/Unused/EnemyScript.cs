using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {
	
	public Transform target;
	public float moveSpeed = 6f;  // chase speed
	public int rotationSpeed = 1;  // speed to turn to the player
	public int maxDistance = 2;  // attack distance
	public int minDistance = 20;  // detection distance
	
	CharacterController charController;
	
	Transform enemyTransform;
	
	public float SpawnTimer;
	float timer;
	
	void Awake()
	{
		enemyTransform = transform;
	}

	// Use this for initialization
	void Start () {
		
		charController = GetComponent<CharacterController>();
		//find the player object
		target = GameObject.FindWithTag("Player").transform;
		maxDistance = 2;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (target) {
        var dist = Vector3.Distance(target.position, transform.position);
		if (dist > minDistance){  // if dist > minDistance: enters idle mode
			Idle(); 
		}
		else
		if (dist <= maxDistance) 
		{  // if dist <= maxDistance: stop and attack
			Debug.Log("Attack!");
		} 
		else {  // if maxDistance < dist < minDistance: chase the player
			Debug.Log("I found you "+ dist);
			enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation,
										   Quaternion.LookRotation(target.position - enemyTransform.position) , 
										   rotationSpeed * Time.deltaTime);
			// Move towards target
			charController.Move(enemyTransform.forward * moveSpeed * Time.deltaTime);
		}
    }
	}
	
	float walkSpeed = 3.0f; 
	float directionTraveltime = 2.0f; 
	float idleTime = 1.5f;
	float rndAngle = 45f;  // enemy will turn +/- rndAngle

	private float timeToNewDirection = 0.0f;
	private float turningTime = 0.0f;
	private float turn;
	
	void Idle () 
	{ 
		// Walk around and pause in random directions unless the player is within range 
		if (Time.time > timeToNewDirection) 
		{ // time to change direction?
			if(Random.value > 0.5)  // choose new direction
				turn = rndAngle;
			else
				turn = -rndAngle;
			turningTime = Time.time + idleTime; // will stop and turn during idleTime...
			timeToNewDirection = turningTime + directionTraveltime;  // and travel during directionTraveltime 
		}
		if (Time.time < turningTime)
		{ // rotate during idleTime...
			transform.Rotate(0, turn*Time.deltaTime/idleTime, 0);
		} 
		else 
		{  // and travel until timeToNewDirection
			charController.SimpleMove(transform.forward * walkSpeed);
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "OutOfBounds")
		{
			Destroy(gameObject);
		}
		
		if(other.tag == "Player")
		{
			Debug.Log ("Player Dead");	
		}
		
		if(other.tag == "PlayerBullet")
		{
			Destroy(gameObject);	
		}
	}
	
	
	
}
