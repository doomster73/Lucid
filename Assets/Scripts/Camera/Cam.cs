using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {
	
	//setup variables
	Transform playerTransform;  //the character transform
	Transform cameraTransform; // the camera transform
	
	public float moveSpeed = 0.5f;  // chase speed
	public float zoomSpeed = 1f;
	
	GameObject playerObject; //object to hold the player
	
	public Vector3 offset;  //offset for camera
	
	public Vector3 standardOffset;  //offset for camera
	public Vector3 ZoomedOffset;  //offset for camera
	
	public Vector3 cameraStart;
	
	//float startFieldOfView = 100f;
	float startFieldOfView = 43f;
	
	//float fieldOfView = 26.3f;
	float fieldOfView = 43.0f;
	
	bool isZooming = false;
	
	public bool IsZooming
	{
		get {return isZooming;}
		set {isZooming = value;}
	}
		
	float zoomingLerpVal;
	
	// Use this for initialization
	void Start () {
	
		//set the initial location
		playerObject = GameObject.FindGameObjectWithTag("Player");
		playerTransform = playerObject.transform;
		
		cameraTransform = GetComponent<Transform>();
		
		//cameraStart = new Vector3(0,50,0);
		cameraTransform.position = cameraStart;
		
		cameraTransform.GetComponent<Camera>().fieldOfView = startFieldOfView;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 playerOffset = new Vector3(20, 20, 0);
		
		if(playerTransform)
		{
			cameraTransform.LookAt(playerTransform.position);
			cameraTransform.position = playerTransform.position + offset;
		}
		
		Zooming();
		
		//cameraTransform.Translate(new Vector3(moveSpeed,0,0) * Time.deltaTime);
		//if(cameraTransform.camera.fieldOfView > fieldOfView)
		//{
			//cameraTransform.camera.fieldOfView -= (zoomSpeed * Time.deltaTime);
		//}
		
		//Debug.Log(cameraTransform.camera.fieldOfView);
			
	}
	
	void Zooming()
	{
		if (isZooming)
		{
			zoomingLerpVal = Mathf.MoveTowards(zoomingLerpVal, 1, Time.deltaTime);
			
			offset = Vector3.Lerp(standardOffset, ZoomedOffset, zoomingLerpVal);
			
		}
		else
		{
			zoomingLerpVal = Mathf.MoveTowards(zoomingLerpVal, 0, Time.deltaTime);
			
			offset = Vector3.Lerp(standardOffset, ZoomedOffset, zoomingLerpVal);		
		}
	}
}
