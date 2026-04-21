using UnityEngine;
using System.Collections;

public class LightBreakScript : MonoBehaviour {
	
	public GameObject lightObject;
	
	void OnTriggerEnter(Collider other)
	{		
		if(other.tag == "FireworkTag")
		{
			if(lightObject.transform.GetComponent<Light>().enabled)
			{
				lightObject.transform.GetComponent<Light>().enabled = false;
			}
			else
			{
				lightObject.transform.GetComponent<Light>().enabled = true;
			}
				
		}	
	}
}
