using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
	
	public bool paused = false;
	
	private int buttonWidth 	= 600;
	private int buttonHeight 	= 50;
	private int groupWidth 		= 700;
	private int groupHeight 	= 400;
	
	public Font pauseFont;
	public GUIStyle pauseStyle = new GUIStyle();
	bool cont = true;
	
	public GameObject thePlayer;
	public PlayerController PCscript;
	
	public Color fontColour;
	
	
	
	// Use this for initialization
	void Start () 
	{
		Time.timeScale = 1;
		pauseStyle.font = pauseFont;
		
		//pauseFont = pauseStyle.font;
		
		//fontColour = Color.white;
		
		//pauseStyle.font.material.color = fontColour;
		pauseFont.material.color = fontColour;
		fontColour = Color.white;
		thePlayer = GameObject.Find("Player");
		PCscript = thePlayer.GetComponent<PlayerController>();
		
		
		
		//onGUI();
	}
	
	void OnGUI ()
	{
			
		if(paused)
		{
			//GUI.TextField(new Rect(0,0,500,500), "Continue Dream");
			
			GUI.BeginGroup(new Rect(((Screen.width*0.5f) - (groupWidth*0.5f)), 
				((Screen.height*0.5f) - (groupHeight*0.5f)),groupWidth, groupHeight));
			if(GUI.Button(new Rect(100, 0, buttonWidth, buttonHeight), "Continue Dream", pauseStyle))
			{
				pauseFont.material.color = Color.white;
				
				
								
				paused = false;	
				
			}
			
			if(GUI.Button (new Rect(20, 100, buttonWidth, buttonHeight), "Return to Checkpoint", pauseStyle))
			{
				paused = false;
				PCscript.playerReturnToCheckPoint = true;
				
			}
			if(GUI.Button (new Rect(120, 200, buttonWidth, buttonHeight), "Restart Dream", pauseStyle))
			{
				paused = false;
				Application.LoadLevel(Application.loadedLevel);	
			}
			if(GUI.Button (new Rect(180, 300, buttonWidth, buttonHeight), "End Dream", pauseStyle))
			{
				paused = false;
				Application.LoadLevel(2);	
			}
			
			
			GUI.EndGroup();
			
			/*if (GUI.Button (new Rect (10,10,150,100), "I am a button")) 
			{
				print ("You clicked the button!");
			}*/
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown("escape"))
		{
			paused = togglePaused();	
		}
		
		if(!paused)
		{
			Time.timeScale = 1;	
		}
		
		
	}
	
	void OnMouseEnter()
	{
		fontColour = Color.red;
		
	}
	
	void OnMouseExit()
	{
		fontColour = Color.white;	
	}
		
	bool togglePaused()
	{
		if(Time.timeScale == 1)
		{
			Time.timeScale = 0;
			return true;
		}
		else
		{
			Time.timeScale = 1;
			return false;
		}
	}
		
}
