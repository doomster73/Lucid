using UnityEngine;
using System.Collections;

public class EndLevelScript : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Player Entered Trigger");
		if(other.tag == "Player")
		{
			Debug.Log("Player Entered Trigger");
			GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().TargetLevel = "RootMenu";
		}
		
		
	}
}
