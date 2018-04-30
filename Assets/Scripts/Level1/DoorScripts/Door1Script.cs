using UnityEngine;
using System.Collections;

public class Door1Script : MonoBehaviour {
	
	Transform TriggerObject, DoorTransform;
		
	GameObject	WallObject;
	
	bool openDoor = false;
	
	// Use this for initialization
	void Start () {
		TriggerObject = transform.parent.transform.Find("MatchTriggerObject");
		DoorTransform = GameObject.Find("Box017").transform;
		WallObject = GameObject.Find("Wall");
	}
	
	// Update is called once per frame
	void Update () {
		openDoor = TriggerObject.GetComponent<CauldronScript>().lightEnabled;
		
		if(openDoor)
		{
			Debug.Log ("Open sesame");
			DoorTransform.GetComponent<DoorMoveScript>().DoorOpen = true;
			Destroy(WallObject);
		}
	}
}
