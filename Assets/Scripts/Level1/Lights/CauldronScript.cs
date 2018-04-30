using UnityEngine;
using System.Collections;

public class CauldronScript : MonoBehaviour {
	
	public Transform lightPoint, Cauldron;
	
	ParticleEmitter cauldronFire;
	
	public bool lightEnabled = false;
	bool lightOn;
	
	float lightIntensity = 4.88f;
	float lightTime = 0.0f;

	// Use this for initialization
	void Start () 
	{
		if(lightPoint)
		{
			cauldronFire = lightPoint.transform.Find("CauldronFire").transform.Find("InnerCore").GetComponent<ParticleEmitter>();
			lightPoint.GetComponent<Light>().enabled = true;
			lightPoint.GetComponent<Light>().intensity = 0;
			cauldronFire.emit = false;
			//lightPoint.transform.Find("CauldronFire").transform.Find("OuterCore").particleEmitter.emit = false;
		}
		
		if(Cauldron && Cauldron.Find("PlayerTriggerObject").GetComponent<RayCastLight>())
		{
			lightOn = Cauldron.Find("PlayerTriggerObject").GetComponent<RayCastLight>().lightLit;	
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(lightEnabled)
		{
			lightPoint.GetComponent<Light>().intensity += 0.01f;
			cauldronFire.emit = true;
			//lightPoint.transform.Find("CauldronFire").transform.Find("OuterCore").particleEmitter.emit = true;
		}
	
	}
	
	
	
	
	void OnTriggerEnter(Collider matchCollider)
	{
		if(matchCollider.tag == "Match" || matchCollider.tag == "FireworkTag")
		{
			lightEnabled = true;
			
			//set light lit flag to true
			if(Cauldron)
			{
				lightOn = true;
				Cauldron.Find("PlayerTriggerObject").GetComponent<RayCastLight>().lightLit = true;
			}	
		}
	}
}
