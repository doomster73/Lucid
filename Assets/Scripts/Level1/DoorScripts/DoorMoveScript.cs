using UnityEngine;
using System.Collections;

public class DoorMoveScript : MonoBehaviour {
	
	Transform doorTransform;	
	
	Vector3 startPosition;
	Vector3 endPosition;
	
	public float moveTime = 1.0f;
	float lerpVal = 0;
	
	private bool doorOpen = false;
	
	public bool DoorOpen
	{
		get {return doorOpen;}
		set {doorOpen = value;}	
	}
	
	bool doorOpened = false;

	// Use this for initialization
	void Start () {
		doorTransform = transform;
		
		startPosition = doorTransform.transform.position;
		
		endPosition = new Vector3(startPosition.x,startPosition.y + 10,startPosition.z);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		//debug
		//if(Input.GetButtonDown("Jump"))
		//{
			//doorOpened = true;	
		//}
		
		if(doorOpen == true)
		{
			doorOpened = true;	
		}
	
		if(doorOpened == true) // && doorOpen == false
		{
			OpenDoor();
			doorOpened = false;
		}
	}
	
	void OpenDoor()
	{
		//doorTransform.transform.position = endPosition;			
		//doorOpen = true;
		
		//float distCovered = (Time.time - startTime) * speed;
        //float fracJourney = distCovered / journeyLength;
		//Vector3 position;
        //position = new Vector3(0, Mathf.Lerp(startPosition.y, endPosition.y, moveTime * Time.deltaTime),0);
		lerpVal += moveTime * Time.deltaTime;
		doorTransform.transform.position = Vector3.Lerp(startPosition, endPosition, lerpVal);
		//doorOpen = true;
	}
}
