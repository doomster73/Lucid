using UnityEngine;
using System.Collections;

public class SimpleMove : MonoBehaviour {
	
	public float mySpeed = 5.0f;
	
	Transform myTransform;

	// Use this for initialization
	void Start () {
		
		myTransform = transform;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		myTransform.Translate(new Vector3(0,0,mySpeed) * Time.deltaTime);
	}
}
