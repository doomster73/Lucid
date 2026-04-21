using UnityEngine;
using System.Collections;

public class MenuBackground : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI()
	{
		GetComponent<Renderer>().material.color = Color.gray;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
