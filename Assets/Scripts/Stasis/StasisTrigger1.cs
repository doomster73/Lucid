using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StasisTrigger1 : MonoBehaviour {
	
	void OnTriggerEnter(Collider other)
	{
		Debug.Log("Player Entered Trigger");
		if(other.tag == "Player")
		{
			Debug.Log("Player Entered Trigger");
			SceneManager.LoadScene(3);
		}
		
		
	}
}
