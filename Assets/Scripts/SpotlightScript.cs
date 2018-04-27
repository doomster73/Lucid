using UnityEngine;
using System.Collections;

public class SpotlightScript : MonoBehaviour {
	
	Transform spotlightTransform;
	
	Transform fromPosition;    
	Transform toPosition;
	
	public float moveSpeed = 0.5f;  // chase speed
	
	float spotlightAngle; 
	
	// Use this for initialization
	void Start () {
	
		spotlightTransform = GetComponent<Transform>();
		
		fromPosition = GetComponent<Transform>();
		toPosition = GetComponent<Transform>();
		
		fromPosition.position = new Vector3(-0.32f,1.43f,0.38f);
		toPosition.position = new Vector3(0,-10,0);
		
		spotlightTransform.position = new Vector3(-0.32f,1.43f,0.38f);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(spotlightTransform.position.y >= 1 && 1==2)
		{
			//spotlightTransform.rotation = Quaternion.RotateTowards( (new Vector3(moveSpeed,moveSpeed,0) * Time.deltaTime);
			spotlightTransform.rotation = Quaternion.Slerp(fromPosition.rotation, toPosition.rotation, Time.deltaTime * moveSpeed);
		}
		else
		{
			spotlightTransform.rotation = Quaternion.Slerp(toPosition.rotation, toPosition.rotation, Time.deltaTime * moveSpeed);
			//spotlightTransform.rotation = (new Vector3(moveSpeed,-moveSpeed,0) * Time.deltaTime);
		}
		
	
	}
}
