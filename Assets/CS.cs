using UnityEngine;
using System.Collections;

public class CS : MonoBehaviour 
{
	Vector3 roll;
	float push;
	
	// Use this for initialization
	void Start () 
	{
		roll = new Vector3(10, 0, 0);
	}
	
	void OnTriggerEnter()
	{
		push = 10;
	}
	
	void OnTriggerExit()
	{
		push = 0;	
	}
	// Update is called once per frame
	void Update () 
	{
		gameObject.GetComponent<Rigidbody>().AddForce(roll);
	}
}
