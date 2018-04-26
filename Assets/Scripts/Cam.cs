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
	
	public Vector3 cameraStart;
	
	//float startFieldOfView = 100f;
	float startFieldOfView = 43f;
	
	//float fieldOfView = 26.3f;
	float fieldOfView = 43.0f;
		

	// Use this for initialization
	void Start () {
	
		//set the initial location
		playerObject = GameObject.FindGameObjectWithTag("Player");
		playerTransform = playerObject.transform;
		
		cameraTransform = GetComponent<Transform>();
		
		//cameraStart = new Vector3(-5f,1f,-1.5f);
		cameraTransform.position = cameraStart;
		
		cameraTransform.camera.fieldOfView = startFieldOfView;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(playerTransform)
		{
			cameraTransform.LookAt(playerTransform.position);
			cameraTransform.position = playerTransform.position + offset;
		}
		
		//cameraTransform.Translate(new Vector3(moveSpeed,0,0) * Time.deltaTime);
		//if(cameraTransform.camera.fieldOfView > fieldOfView)
		//{
			//cameraTransform.camera.fieldOfView -= (zoomSpeed * Time.deltaTime);
		//}
		
		//Debug.Log(cameraTransform.camera.fieldOfView);
			
	}
}
