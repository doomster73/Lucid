using UnityEngine;
using System.Collections;

public class stasisLevelLoadScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump"))
		{
			//find the game object tagged as stats, temporarily grab its GameStats component and set it
			GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().TargetLevel = "PrototypeScene1";
		}
	
	}
}
