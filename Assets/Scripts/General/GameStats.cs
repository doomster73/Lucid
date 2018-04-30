using UnityEngine;
using System.Collections;

public class GameStats : MonoBehaviour 
{
	string targetLevel = "PrototypeScene1";
	
	Transform currentCheckPointTransform;
	
	GUIText matchMonitor;
	
	public GUITexture mTexture;
	public Texture2D  matchTexture;
	public Texture2D  burnTexture;
	public Texture2D  matchBox;
	bool noMatches = false;
	int matchNum = 0;
	
	
	//string targetLevel = "RootMenu";
	
	int matches = 0;
	
	public int Matches
	{
		get {return matches;}
		set {matches = value;}
	}
	
	int playerHP = 100;
	
	public int PlayerHP
	{
		get {return playerHP;}
		set {playerHP = value;}
	}
	
	public Transform CurrentCheckPoint
	{
		get {return currentCheckPointTransform;}
		set {currentCheckPointTransform = value;}	
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
	void Start () 
	{
		
		if(GameObject.Find("Matches"))
		{
			matchMonitor = GameObject.Find("Matches").GetComponent<GUIText>();
		}
		
		matchNum = matches;		
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(matchMonitor)
		{
			matchMonitor.text = "Matches Left: " + matches.ToString();
		}
		
		if(matchNum == 0)
		{
			noMatches = true;	
		}
				
	}
	
	void OnGUI()
	{
		
		/*for(matchNum = matches; !noMatches; matchNum--)
		{
			
		}*/
		
		
		if(Application.loadedLevelName == "Hospital")
		{
			if(matches > 0)
				GUI.Label(new Rect(100, 480, 100, 200), matchTexture);
			else
				GUI.Label(new Rect(100, 480, 100, 200), burnTexture);
			
			if(matches > 1)
				GUI.Label(new Rect(130, 480, 100, 200), matchTexture);
			else
				GUI.Label(new Rect(130, 480, 100, 200), burnTexture);
			
			if(matches > 2)
				GUI.Label(new Rect(160, 480, 100, 200), matchTexture);
			else
				GUI.Label(new Rect(160, 480, 100, 200), burnTexture);
			
			if(matches > 3)
				GUI.Label(new Rect(190, 480, 100, 200), matchTexture);
			else
				GUI.Label(new Rect(190, 480, 100, 200), burnTexture);
			
			if(matches > 4)
				GUI.Label(new Rect(220, 480, 100, 200), matchTexture);
			else
				GUI.Label(new Rect(220, 480, 100, 200), burnTexture);
			
			if(matches > 5)
				GUI.Label(new Rect(250, 480, 100, 200), matchTexture);
			else
				GUI.Label(new Rect(250, 480, 100, 200), burnTexture);
			
			if(matches > 6)
				GUI.Label(new Rect(280, 480, 100, 200), matchTexture);
			else
				GUI.Label(new Rect(280, 480, 100, 200), burnTexture);
			
			if(matches > 7)
				GUI.Label(new Rect(310, 480, 100, 200), matchTexture);
			else
				GUI.Label(new Rect(310, 480, 100, 200), burnTexture);
		}
		
		//GUI.Label(new Rect(100, 570, 800, 1100), matchBox);
	}
}
