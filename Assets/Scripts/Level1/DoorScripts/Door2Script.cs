using UnityEngine;
using System.Collections;

public class Door2Script : MonoBehaviour {
	
	Transform TriggerObject, DoorTransform;
	
	bool openDoor = false;
	
	// Use this for initialization
	void Start () {
		TriggerObject = transform.parent.transform.Find("MatchTriggerObject");
		DoorTransform = GameObject.Find("Box031").transform;
	}
	
	// Update is called once per frame
	void Update () {
		openDoor = TriggerObject.GetComponent<CauldronScript>().lightEnabled;
		
		if(openDoor)
		{
			Debug.Log ("Open sesame");
			DoorTransform.GetComponent<DoorMoveScript>().DoorOpen = true;
		}
	}
}