using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour 
{
	Light myLight;
	public float maxLight;
	public float minLight;
	public float currentTarget;
	
	float myNumber = 0;
	float myRate = 2;
	
	float myLerp = 0;
	
	bool movingForward = true;
	
	// Use this for initialization
	void Start () 
	{
		//Get light component and set to myLight
	}
	
	// Update is called once per frame
	void Update () 
	{
		//LerpBox();
		
		//use Mathf.MoveTowards, and shift the mylight.intensity variable towards the value of "currentTarget" variable
		
		//when myLight.intensity is the same as CurrentTarget set Current target to be a random value between minLight and maxLight
	}
	
	void LerpBox()
	{
		myNumber = Mathf.MoveTowards(myNumber, 20, myRate * Time.deltaTime);
		
		print(myNumber);
		
		if (movingForward)
			myLerp += Time.deltaTime;
		else
			myLerp -= Time.deltaTime;
			
		
		if (myLerp >= 1 && movingForward)
			movingForward = false;
		else if (myLerp <= 0 && !movingForward)
			movingForward = true;
		
		print(myLerp);
		
		transform.position = Vector3.Lerp(new Vector3(20,0,0), new Vector3(40,20,5), myLerp);
	}
}
