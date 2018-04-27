using UnityEngine;
using System.Collections;

public class PlayGameControlText : MonoBehaviour {
	
	string loadingLevel;
	
	// Use this for initialization
	void Start () {
		loadingLevel = GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().TargetLevel;
	}
	
	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = Color.white;
	}
	
	void OnMouseUp()
	{
		print ("Load Stasis Down");
		GetComponent<Renderer>().material.color = Color.green;
		GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().TargetLevel = "Stasis";	
	}
}
