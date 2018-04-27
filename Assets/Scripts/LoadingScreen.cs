using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour {
	
	void Start()
	{
		//finding the GameStats object, which is permanent between scenes, and grabbing the TargetLevel variable
		//which is the variable we're using to tell the loading screen which scene it should be leading into
		string loadingLevel = GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().TargetLevel;
		//starting the co-routine
		StartCoroutine(WaitForLoad(loadingLevel));
	}
	
	IEnumerator WaitForLoad(string level)
	{
		print("YO");
		//this syntax means that when this function is called the game will continue once it gets to this yield point, and the function will 
		//finish itself off after the specified time, in this case 3 seconds
		yield return new WaitForSeconds(3);
		print("YO2");
		
		Application.LoadLevel(level);
	}
}
