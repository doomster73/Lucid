using UnityEngine;
using System.Collections;

public class RayCastLight : MonoBehaviour {
	
	Transform source;
	
	public RaycastHit hit;
	
	Vector3 dir;

	GameObject player;
	
	public float rayDistance = 20f;
	
	bool wasInLight = false;
	
	public bool lightLit; //initial state of light
	
	public int layerMask = 0;
	
	public float maximumAcceptableAngle;
	
	
	PlayerController playerCont;
	

	// Use this for initialization
	void Start () {

		player =  GameObject.FindWithTag("Player");
		
		source = transform;
		
		dir = (player.transform.position - source.position).normalized;
		
		playerCont = player.GetComponent<PlayerController>();
		
		layerMask = 1 << LayerMask.NameToLayer("Default");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		dir = (player.transform.position - source.position).normalized;
		

		Vector3 playerRelation = transform.InverseTransformPoint(player.transform.position);
		

		
		if (Mathf.Abs(Mathf.Atan2(playerRelation.y, playerRelation.x )  * Mathf.Rad2Deg + 90) < maximumAcceptableAngle)
		{
			
			if (Physics.Raycast(source.position, dir, out hit, rayDistance,layerMask))
			{
				if(hit.collider.gameObject.name == "Player")
				{
					if(lightLit)
					{
						if(!wasInLight)
						{
							playerCont.InLightSource++;
							wasInLight = true;
						}
					}	
				}
				else
				{
					if(wasInLight)
					{
						playerCont.InLightSource--;
						wasInLight = false;
					}
				}
			}
		}
		else if(wasInLight)
		{
			playerCont.InLightSource--;
			wasInLight = false;
		}
		

		
        Debug.DrawRay (transform.position, dir * rayDistance , Color.cyan);    
		//Debug.DrawLine (transform.position,player.transform.position , Color.red); 
	}
}
