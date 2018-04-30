using UnityEngine;
using System.Collections;

public class PlayGameControlText : MonoBehaviour {
	
	string loadingLevel;
	
	public Color omeColor;
	
	// Use this for initialization
	void Start () {
		loadingLevel = GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().TargetLevel;
		//omeColor = new Color(10,128, 166, 255);
	}
	
	void OnMouseEnter()
	{
		GetComponent<Renderer>().material.color = Color.red;	
	}
	
	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = Color.white;
	}
	
	void OnMouseUp()
	{
		print ("Load Stasis Down");
		//renderer.material.color = Color.green;
		//GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().TargetLevel = "Stasis";
		Application.LoadLevel(2);
	}
}
