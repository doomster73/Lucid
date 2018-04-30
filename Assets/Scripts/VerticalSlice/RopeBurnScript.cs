using UnityEngine;
using System.Collections;

public class RopeBurnScript : MonoBehaviour {
	
	GameObject box;

	// Use this for initialization
	void Start () {
		box = GameObject.Find("MoveableBox");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter(Collider matchCollider)
	{
		
		if(matchCollider.tag == "Match")
		{Debug.Log("rope here");
			box.transform.GetComponent<Rigidbody>().isKinematic = false;
			box.transform.GetComponent<Rigidbody>().useGravity = true;
			
//			{
//				
//				Cauldron.Find("PlayerTriggerObject").GetComponent<RayCastLight>().lightLit = true;
//			}	
		}
	}
}
