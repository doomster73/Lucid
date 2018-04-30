using UnityEngine;
using System.Collections;
//using UnityEditor;

public class MatchScript : MonoBehaviour {
	
	public enum Directions
	{
		Left,
		Right,
		Up,
		Down
	};
	
	Transform matchTransform;
	Transform target;
	
	float verticalMovement; //holds the vertical movement for use in jumps
	
	Vector3 matchMovement;
	
	public float throwPower = 100f; //public var jump force
	float gravityPower = 30f; //public var gravity force
	
	public float Speed = 60f;
	float Timer = 10.0f;
	
	Transform player;
    public bool isLit = false;
    public bool thrown = false;
	
	public GameObject thePlayer;
	public PlayerController PCscript;

    Directions throwDirection = Directions.Right;	

    public Directions mDirection;
	
	// Use this for initialization
	void Start () {
		verticalMovement = throwPower;
		matchTransform = transform;
		player = GameObject.FindGameObjectWithTag("Player").transform;
		thePlayer = GameObject.Find("Player");
		PCscript = thePlayer.GetComponent<PlayerController>();

	}
	
	// Update is called once per frame
	void Update () 
	{
        if (isLit)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                Destroy(gameObject);
                isLit = false;
                PCscript.holdingMatch = false;
            }
        }

        if (thrown)
        {
            Throw();
        }
        else
        {
            MoveMatch();
        }
		
	}
	
	void MoveMatch()
	{
            
		float HPx = 0;
		float HPy = player.position.y + 2.8f;
		mDirection = (Directions)PCscript.playerDirection;
			
		if(PCscript.horizontal > 0)
		{
			if(mDirection == Directions.Right)
			{
				HPx = player.position.x - 1.0f;
			}
			else if(mDirection == Directions.Left)
			{
				HPx = player.position.x + 1.0f;
			}
		}
		else if(PCscript.horizontal < 1)
		{
			if(mDirection == Directions.Right)
			{
				HPx = player.position.x - 1.5f;
			}
			else if(mDirection == Directions.Left)
			{
				HPx = player.position.x + 1.5f;
			}
		}
			Vector3 HoldingPos = new Vector3(HPx, HPy,player.position.z);
				
            matchTransform.position = HoldingPos;

            isLit = true;
		
	}
	
	void Throw()
	{
		if(throwDirection == Directions.Right)
		{
			matchTransform.position += matchTransform.TransformDirection(-Speed ,verticalMovement,0) * Time.deltaTime;
			Gravity();
		}
		else if(throwDirection == Directions.Left)
		{
			matchTransform.position += matchTransform.TransformDirection(-Speed ,verticalMovement,0) * Time.deltaTime;
			Gravity();
		}
		
	}
	
	public void DoThrow()
	{
		throwDirection = (Directions)PCscript.playerDirection;
        thrown = true;
        PCscript.holdingMatch = false;
	}
	
	void Gravity()
	{
		//if match is not already on the ground
			verticalMovement -= gravityPower * Time.deltaTime;
			if(verticalMovement < -50)
			{
				verticalMovement = -50;	
			}

	}
	
	void OnTriggerEnter(Collider triggerCollider)
	{

        if (triggerCollider.tag == "MatchColliderTag") //|| triggerCollider.tag == "Level"
		{
			Destroy(gameObject);
		}
	}
}
