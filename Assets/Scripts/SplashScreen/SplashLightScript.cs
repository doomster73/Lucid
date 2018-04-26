using UnityEngine;
using System.Collections;

public class SplashLightScript : MonoBehaviour {
	
	Light SplashLight;
	
	// Use this for initialization
	void Start () {
		SplashLight = transform.light;
		
		SplashLight.intensity = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	
		SplashLight.intensity += 0.1f * Time.deltaTime;
	}
}
