using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightECG : MonoBehaviour 
{
	public List<Transform> points = new List<Transform>();
	List<float> lerpPoints = new List<float>();
	
	int lerpingToPoint = 0;
	float lastLerp;
	float primaryLerp;
	float startingX;
	float endingX;
	Transform myTransform;
	
	// Use this for initialization
	void Start () 
	{
		myTransform = transform;
		CalculateLerpPoints();	
	}
	
	void CalculateLerpPoints()
	{
		startingX = points[0].position.x;
		endingX = points[points.Count-1].position.x;
		
		for (int i = 0; i < points.Count; i++)
		{
			lerpPoints.Add(0);
			
			lerpPoints[i] = Mathf.InverseLerp(points[0].position.x, points[points.Count - 1].position.x, points[i].position.x);
		}
		//starting X = xposition of first point in list
		//ending X = xposition of last point in list;
		
		//use inverse lerp to find out where in the lerp each points x is, an example being that if a point were at exactly half way, it would be .5
	}
	
	// Update is called once per frame
	void Update () 
	{
		primaryLerp += Time.deltaTime / 5;
		
		if (primaryLerp > 1)
		{
			lerpingToPoint = 0;
			primaryLerp = 0;
		}
		
		if (primaryLerp > lerpPoints[lerpingToPoint])
		{
			lerpingToPoint++;
		}
		
		Vector3 position;
		position = new Vector3(Mathf.Lerp(startingX, endingX, primaryLerp),myTransform.position.y,0);
		//position = new Vector3(myTransform.position.x, Mathf.Lerp(myTransform.position.y, points[lerpingToPoint].position.y, some lerping value needed), 0);
		myTransform.position = position;
	}
}
