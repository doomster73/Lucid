using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutOfBoundsScript : MonoBehaviour {
	
	Transform checkpointLocation;
	GameObject playerController;
	
	// Use this for initialization
	void Start () {
		playerController = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			checkpointLocation = GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().CurrentCheckPoint;
			playerController.GetComponent<PlayerController>().transform.position = checkpointLocation.position;
		}
		
		
	}
}
