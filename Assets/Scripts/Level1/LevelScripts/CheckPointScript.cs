using UnityEngine;
using System.Collections;

public class CheckPointScript : MonoBehaviour {
	
	public string CheckPointName = "";
	public int NumberofMatches = 0;
	
	Transform CheckPointLocation;
	
	bool matchesUpdated = false;

	// Use this for initialization
	void Start () {
		CheckPointLocation = transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().CurrentCheckPoint = CheckPointLocation;
			if(matchesUpdated == false)
			{
				GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().Matches = CheckPointLocation.GetComponent<CheckPointScript>().NumberofMatches;	
				matchesUpdated = true;
			}
		}	
	}
}
