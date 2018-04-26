using UnityEngine;
using System.Collections;

public class LightColliderScript : MonoBehaviour {
	
	string debugText = "";
	
	public GUIText dText;
	
	public bool lightLit;

	// Use this for initialization
	void Start () {
		//debugText = GameObject.Find("DarknessIndicator").GetComponent(GUIText);
		
		//dText = tr
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider lightCollider)
	{
		if(lightCollider.tag == "Player" && lightLit)
		{
			//lightEnabled = true;
				
			//Debug.Log("Player in Light");
			debugText = "LIGHTTRIGGERED";
			dText.text = "LIGHTTRIGGERED";
		}
		else
		{
			//Debug.Log("Player in Darkness");
			debugText = "DARK";
			dText.text = "DARK";
		}
	}
	
	void OnTriggerExit(Collider lightCollider)
	{
		if(lightCollider.tag == "Player")
		{
			//lightEnabled = true;
				
			//Debug.Log("Player in Darkness");
			debugText = "DARKEXITED";
			dText.text = "DARKEXITED";
		}
	}
	
	void OnGUI()	
	{
		//GUI.Label(new Rect (100,100,200,200),debugText);
	}
}
