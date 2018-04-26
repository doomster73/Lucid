using UnityEngine;
using System.Collections;

public class StasisStartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().TargetLevel = "Level1";
		
		string loadingLevel = GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().TargetLevel;
		
	}
	
	// Update is called once per frame
	void Update () {
	
		
	}
}
