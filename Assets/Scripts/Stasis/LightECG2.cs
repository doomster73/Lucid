using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightECG2 : MonoBehaviour 
{
	public List<Transform> points = new List<Transform>();
	List<float> lerpXPoints = new List<float>();
	
	public float ASDJASDJALSDJ;
    //tracks where the object is on its X position
	float primaryLerp;

    //For Y Position of the ECG
    //---------------------------------------------------------------
    //current point in the list that it's moving towards on the Y
    int lerpingToPoint = 0;
    //Y position of the point it's coming from
    float lastYPoint;
    //Y position of the point it's lerping towards
    float nextYPoint;
    //multiplier to the primary lerp to get it to target Y in necessary Time, as each Y point is really a fraction of the primary lerp
    float currentYLerpMultiplier;
	
    float currentYLerp;
	
    //--------------------------------------------------------------

    //X position of the first point
    float startingX;
    //X position of the last point
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
        //setting first and last points X position to be the start and end X position
		startingX = points[0].position.x;
		endingX = points[points.Count-1].position.x;
		
        //Adding an inversed lerp for every Transform within the Points list, to another list which will store those inverse points
        //if there are 3 Transforms in the Points list, X positions of 3, 5 & 9. Then the lerpXPoints list will store [0], [.333f], [1] because 5 is 1/3 of the way between 3 and 9
        //if the list were 3, 5, 9, 13. Then the lerpXPoints list will store [0], [.2f], [.6f], [1] because 5 & 9 are 20% and 60% between 3 and 13. (Hope that makes sense)
		for (int i = 0; i < points.Count; i++)
		{
			lerpXPoints.Add(0);
			
			lerpXPoints[i] = Mathf.InverseLerp(startingX, endingX, points[i].position.x);
		}

        //first location we're lerping to is point 1 from 0, so we set to that
        lerpingToPoint = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
        //changing the primary lerping value by delta time / 5 (so this value will go from 0 to 1 in 5 seconds)
        primaryLerp += Time.deltaTime / 15;
		currentYLerp += (Time.deltaTime / 15) * currentYLerpMultiplier;
		
        //if primaryLerp is > the X point of the point to which we're currently lerping, we know we're past it and should be lerping to the point after that
		if (primaryLerp > lerpXPoints[lerpingToPoint])
		{
			lerpingToPoint++;

            //if the point we're lerping to is longer than the list, we know we're now at the end, so we start again
            if (lerpingToPoint > lerpXPoints.Count - 1)
            {
                lerpingToPoint = 1;
                primaryLerp = 0;
            }
			
			currentYLerp = 0;
            currentYLerpMultiplier = CalculateYLerpMultiplier();

            lastYPoint = points[lerpingToPoint-1].position.y;
            nextYPoint = points[lerpingToPoint].position.y;
		}

		Vector3 position;
		//print(currentYLerpMultiplier + ", " + lastYPoint + ", " + nextYPoint);
		position = new Vector3(Mathf.Lerp(startingX, endingX, primaryLerp), Mathf.Lerp(lastYPoint, nextYPoint, currentYLerp), 0);
		
		myTransform.position = position;
	}

    float CalculateYLerpMultiplier()
    {
        float yLerp;

        //We want to know the difference in normalised value between the point it's moving from, to the point it's moving to.
        float difference = lerpXPoints[lerpingToPoint] - lerpXPoints[lerpingToPoint - 1];

        //by dividing 1 by the difference, we know how much we need to multiply the Y movement by, to know it will reach one over the time required, to reach the
        //correct Y value
        yLerp = 1 / difference;

        return yLerp;
    }
}
