using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour 
{
	string targetLevel = "PrototypeScene1";
	
	//string targetLevel = "RootMenu";
	
	int playerHP = 100;
	
	public int PlayerHP
	{
		get {return playerHP;}
		set {playerHP = value;}
	}
	

	public string TargetLevel
	{
		get {return targetLevel;}
		
		//When Target is set, rather than just setting the variable we also load the loading scene level straight away
		set{
			targetLevel = value;
			Application.LoadLevel(value);
		}
	}
	void Awake()
	{
		//marking this object to not be removed between scenes, this way it can store permanent information
		GameObject.DontDestroyOnLoad(gameObject);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
