using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour {
	
	Vector3 climbDirection = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		
		climbDirection = transform.Find("ladderTop").transform.position - transform.Find("ladderBottom").transform.position;
	}
	
	public Vector3 Climbposition()
	{
		return climbDirection;	
	}
}
