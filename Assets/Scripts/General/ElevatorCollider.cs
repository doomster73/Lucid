using UnityEngine;
using System.Collections;

public class ElevatorCollider : MonoBehaviour {
	
	public PlayerController pcScript;
	public GameObject thePlayer;
	// Use this for initialization
	void Start () 
	{
		pcScript = thePlayer.GetComponent<PlayerController>();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			pcScript.InLightSource ++;	
		}
		
		/*if(collider.gameObject.transform == pcScript)
		{
			pcScript.InLightSource = 1;	
		}*/
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			pcScript.InLightSource --;	
		}	
	}
	
	/*void OnTriggerEnter()
	{		
		if(isTop == true)
			eScript.yForce = -2.0f;
		else if(isTop == false)
			eScript.yForce = 2.0f;
	}*/
}
