using UnityEngine;
using System.Collections;

public class FireworkMoveScript : MonoBehaviour {
	
	public enum fireworkTypes
	{
		Vertical,
		Horizontal,
		Slerping
	}
	
	public GameObject fireworkTransform;
	
	public GameObject fireworkEndObject = null;
	public float travelDistance = 0;
	
	Vector3 startPosition;
	Vector3 endPosition;
	
	public fireworkTypes FireworkType = fireworkTypes.Vertical;
	
	public float moveTime = 0.1f;
	float lerpVal = 0;
	
	float Timer = 5.0f;
	
	private bool fireWorkMove = false;
	
	public bool FireworkMove
	{
		get {return fireWorkMove;}
		set {fireWorkMove = value;}	
	}
	
	bool fireworkMoved = false;
	
	//slerp
	public Transform sunrise;
    public Transform sunset;
    public float journeyTime = 0.2F;
    private float startTime;
	float fracComplete;
	
	// Use this for initialization
	void Start () {
		//fireworkTransform = transform;
		
		startPosition = fireworkTransform.transform.position;
		
		if(FireworkType == fireworkTypes.Vertical)
		{
			endPosition = new Vector3(startPosition.x,startPosition.y + travelDistance,startPosition.z);
		}
		else if(FireworkType == fireworkTypes.Horizontal)
		{
			endPosition = new Vector3(startPosition.x - travelDistance,startPosition.y ,startPosition.z);
		}
		else
		{
			endPosition = new Vector3(0,0,0);	
		}
		
		
		startTime = Time.time;
		
		sunrise = fireworkTransform.transform;
		sunset = fireworkEndObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(fireWorkMove == true)
		{
			fireworkMoved = true;	
		}
	
		if(fireworkMoved == true) // && doorOpen == false
		{
			if(FireworkType == fireworkTypes.Vertical || FireworkType == fireworkTypes.Horizontal)
			{
				FireWorkGo();
			}
			else if(FireworkType == fireworkTypes.Slerping)
			{
				FireWorkGoSlerp();
			}
			
			//fireworkMoved = false;
		}
		
		if(lerpVal > 1)
		{
			fireworkMoved = false;	
		}
		
		Timer -= Time.deltaTime;

		if(Timer <= 0)
		{
			fireworkTransform.transform.Find("FireworkLight").GetComponent<Light>().enabled = false;
		}
	}
	
	void FireWorkGo()
	{
		lerpVal += moveTime * Time.deltaTime;
		fireworkTransform.transform.position = Vector3.Lerp(startPosition, endPosition, lerpVal);
	}
	
	void FireWorkGoSlerp()
	{
		Vector3 center = (sunrise.position + sunset.position) * 0.5F;
        center -= new Vector3(0, 1, 0);
        Vector3 riseRelCenter = sunrise.position - center;
        Vector3 setRelCenter = sunset.position - center;
        fracComplete = ((Time.time - startTime) / journeyTime) * Time.deltaTime;
       	fireworkTransform.transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        fireworkTransform.transform.position += center;	
	}
	
	void OnTriggerEnter(Collider matchCollider)
	{
		if(matchCollider.tag == "Match")
		{
			fireworkMoved = true;
			
			fireworkTransform.transform.Find("FireworkLight").GetComponent<Light>().enabled = true;
	
		}
	}
}
