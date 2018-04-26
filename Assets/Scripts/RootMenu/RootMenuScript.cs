using UnityEngine;
using System.Collections;

public class RootMenuScript : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{		
		
		
	}	
	void Awake()
	{
		//find the game object tagged as stats, temporarily grab it's gamestats and set it
		print ("reducinghealth" + name);
		GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().PlayerHP -= 1;
	}
	
	
}
