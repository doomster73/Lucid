using UnityEngine;
using System.Collections;

public class MovingDoor : MonoBehaviour {
	
	
	public GameObject Door;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter()
	{
		Door.GetComponent<Rigidbody>().useGravity = true;
		Door.GetComponent<Rigidbody>().freezeRotation = false;
	}
}
