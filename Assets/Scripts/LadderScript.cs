using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour {

	GameObject playerController;
	
	// Use this for initialization
	void Start () {
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
			playerController.GetComponent<PlayerController>().onLadder = true;
		}
		
		
			//When the player comes into proximity of the ladder they will latch on. 
			//Once latched forward motion of the player is converted into vertical motion along the ladder.
			//At the extreme bottom or top of the ladder the player can move away from the ladder using the normal controls, or along the ladder.
			//While on the ladder, the player can only move side to side or up and down the ladder.
			//While on the ladder, if the player looks down below a threshold value, then forward movement will climb down instead of up the ladder.
			//While on the ladder, the player can press the jump key to jump off of the ladder.
			//When the player exits the ladder at the top or bottom they will perform a very small hop.
			//Ladders can be at any angle, however under normal settings the player can walk over objects at inclines under 45º, so in these cases a ladder may not be necessary.
			//Ladders can be any size and shape from wide ladders to narrow vines.	
		
		
	}
	

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			playerController.GetComponent<PlayerController>().onLadder = false;
		}	
	}
}
