using UnityEngine;
using System.Collections;

public class CandleSphereScript : MonoBehaviour {
	
	GameObject playerController;
	
	GameObject candleSphere;

	// Use this for initialization
	void Start () {
		
		candleSphere = transform.gameObject;
		
		//get component of player controller and set it to a variable
		playerController = GameObject.Find("Player");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			playerController.GetComponent<PlayerController>().AssignPickup(candleSphere);	
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
}
