using UnityEngine;
using System.Collections;

public class CubeDetector : MonoBehaviour 
{
	GameObject playerController;
	
	GameObject cube;
	
	void Start()
	{
		cube = transform.parent.gameObject;
		
		//get component of player controller and set it to a variable
		playerController = GameObject.Find("Player");
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		//if other tag == player
		if(other.tag == "Player")
		{		
			//use playerController variable to call "assign cube function" pass the GameObject of this object's transform.parent
			
			//Debug.Log(playerController.transform.position.x);
			playerController.GetComponent<PlayerController>().AssignPushingBlock(cube);
			
			//playerController.transform.Find("PlayerController")
			
			//Debug.Log("Player hit box");
			
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			//Drop the box
			if(playerController.GetComponent<PlayerController>())
			{
				playerController.GetComponent<PlayerController>().UnnassignPushingBlock();	
//				transform.parent.rigidbody.isKinematic = false;
			}
			
			//playerController
			//Debug.Log("Player left box");	
		}
	}
}
