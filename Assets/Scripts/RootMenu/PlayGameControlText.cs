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
		renderer.material.color = Color.white;
	}
	
	void OnMouseUp()
	{
		print ("Load Stasis Down");
		renderer.material.color = Color.green;
		GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().TargetLevel = "Stasis";	
	}
}
