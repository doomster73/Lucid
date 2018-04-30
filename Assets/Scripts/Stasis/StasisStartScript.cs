using UnityEngine;
using System.Collections;

public class StasisStartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		string loadingLevel = GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().TargetLevel;
	}
}
