using UnityEngine;
using System.Collections;

public class CameraStart : MonoBehaviour {
	
	int zoom = 20;
	int normal = 45;
	
	float smooth = 500;
	
	public Camera camera;
	
	// Use this for initialization
	void Start () {
		
		Camera.main.fieldOfView = 0;
		camera.fieldOfView = Mathf.Lerp(camera.fieldOfView,normal,Time.deltaTime*smooth);
		
	}
	
	// Update is called once per frame
	void Update () {
	
		//camera.fieldOfView = Mathf.Lerp(camera.fieldOfView,zoom,Time.deltaTime*smooth);
		if (Camera.main.fieldOfView>1)

    Camera.main.fieldOfView -=2;

    if (Camera.main.orthographicSize>=1)

         Camera.main.orthographicSize -=0.5f;


	}
}
