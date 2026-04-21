using UnityEngine;
using System.Collections;

public class CandleSphereScript : MonoBehaviour {
	
	GameObject playerController;
	
	GameObject candleSphere;
	
	Vector3 myPropulsion;
	
	enum State
	{
		none,
		moving
	};
	
	State myState;

	// Use this for initialization
	void Start () {
		
		//candleSphere = transform.parent.gameObject;
		candleSphere=this.gameObject;
		
		//get component of player controller and set it to a variable
		playerController = GameObject.Find("Player");
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			playerController.GetComponent<PlayerController>().AssignPickup(candleSphere);
			myState = State.none;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
	
		if(other.tag == "Player")
		{
			if(playerController)
			{
				playerController.GetComponent<PlayerController>().UnnassignPickup();
			}
		}
		
	}
	
	public void SetPropulsion(Vector3 propulsion)
	{
		//Debug.Log("I'm being called");
		myState = State.moving;
		myPropulsion = propulsion;
		transform.parent.GetComponent<Rigidbody>().AddForce(myPropulsion, ForceMode.Force);
	}
}
