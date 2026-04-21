using UnityEngine;
using System.Collections;

public class UseLadder : MonoBehaviour {
	
	Transform playerController;
	
	bool inLandingPad = false;
	
	public ArrayList landingPads = new ArrayList();
	
	// Use this for initialization
	void Start () {
		//get component of player controller and set it to a variable
		playerController = transform;
		//onLadder = playerController.GetComponent<PlayerController>().onLadder;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "player")
		{
			if(!landingPads.Contains(other))
			{
				landingPads.Add(other);
			}
			
			inLandingPad = true;
			
		}
		
		if(other.tag == "ladderTop")
		{
			playerController.GetComponent<PlayerController>().offLadder = true;
		}
		
		if(other.tag == "Ladder")
		{
			playerController.GetComponent<PlayerController>().onLadder = true;
		}	
		
	}
	

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "ladderBottom" || other.tag == "ladderTop")
		{
			// remove this landing pad from the list
			landingPads.Remove(other);
		
			// if the list is now empty, set the flag
				if(landingPads.Count == 0)
			{
				inLandingPad = false;	
			}
		}
		
		if(other.tag == "Ladder")
		{
			playerController.GetComponent<PlayerController>().onLadder = false;
		}	
	}
}
