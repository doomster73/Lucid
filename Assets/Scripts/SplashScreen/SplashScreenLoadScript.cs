using UnityEngine;
using System.Collections;

public class SplashScreenLoadScript : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		
		//finding the GameStats object, which is permanent between scenes, and grabbing the TargetLevel variable
		//which is the variable we're using to tell the loading screen it shoudl be leading into
		string loadingLevel = GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().TargetLevel;
		
		//find the game object tagged as stats, temporarily grab it's gamestats and set it
		GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().PlayerHP -= 10;
		
		//starting the co-routine
		StartCoroutine(WaitForLoad(loadingLevel));
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator WaitForLoad(string Level)
	{
		//this syntax means that when this function is called the game will continue once it get to the yield point, and the function 
		//will finish off after the specified time.
		yield return new WaitForSeconds(5);
		
		GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().TargetLevel = "RootMenu";
	}
}
