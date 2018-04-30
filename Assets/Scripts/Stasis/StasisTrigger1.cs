using UnityEngine;
using System.Collections;

public class StasisTrigger1 : MonoBehaviour {
	
	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Player Entered Trigger");
		if(other.tag == "Player")
		{
			Debug.Log("Player Entered Trigger");
			Application.LoadLevel(3);
		}
		
		
	}
}
