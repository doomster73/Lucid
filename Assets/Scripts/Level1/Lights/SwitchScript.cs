using UnityEngine;
using System.Collections;

public class SwitchScript : MonoBehaviour {
	
	private bool switchable = false;
	private bool switchOn = false;
	
	public GameObject lightOne, lightTwo;
	GameObject lightSwitch, playerController;
	
	public bool SwitchOn
    {
        get { return switchOn; }
        set { switchOn = value; }
    }

	// Use this for initialization
	void Start () {
		lightSwitch = transform.gameObject;
		
		//get component of player controller and set it to a variable
		playerController = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
		if(switchable)
		{
			if(switchOn)
			{
				if(lightOne.transform.GetComponent<RayCastLight>().lightLit)
				{
					lightOne.transform.GetComponent<Light>().enabled = false;
					lightTwo.transform.GetComponent<Light>().enabled = true;
					lightOne.transform.GetComponent<RayCastLight>().lightLit = false;
					lightTwo.transform.GetComponent<RayCastLight>().lightLit = true;
					switchOn = false;
				}
				else
				{
					lightOne.transform.GetComponent<Light>().enabled = true;
					lightTwo.transform.GetComponent<Light>().enabled = false;
					lightOne.transform.GetComponent<RayCastLight>().lightLit = true;
					lightTwo.transform.GetComponent<RayCastLight>().lightLit = false;
					switchOn = false;
				}	
			}
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			playerController.GetComponent<PlayerController>().AssignSwitch(lightSwitch);
			switchable = true;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
	
		if(other.tag == "Player")
		{
			if(playerController)
			{
				playerController.GetComponent<PlayerController>().UnnassignSwitch();
				switchable = false;
			}
		}
		
	}
}
