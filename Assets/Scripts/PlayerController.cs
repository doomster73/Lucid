using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Character controller must be available
[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour {
	
	public bool onLadder
    {
        get { return onladder; }
        set { onladder = value; }
    }
	
	public enum Directions
	{
			Left,
			Right,
			Up,
			Down
	}
	
	Transform animChild;
	//setup variables
	public float moveSpeed; //public var movement speed
	public float sprintSpeed; //public var sprint speed
	public float jumpPower; //public var jump force
	public float gravityPower; //public var gravity force
	public float climbSpeed; //public var for climbing speed
	public float sprintLimit; //public var for time before sprint starts
	
	Transform charTransform; //hold character transform
	Vector3 charMovement; //holds character movement
	CharacterController charController; // hold character controller object
	
	public Directions playerDirection;
	
	float verticalMovement; //holds the vertical movement for use in jumps
	
	float horizontal; //hold horizontal movement
	float vertical; //hold vertical movement
	float rotation; //hold rotation movement
	float sprintTime; //hold sprint timer
	
	bool grab; //checks for object grabbed
	bool pickedUp = false; // checks picked up object
	
	private bool onladder = false; //variable to check if player on ladder.
	
	GameObject pushingCube;
	GameObject pickupObj;
	
	public GameObject MatchPrefab;
	List<GameObject> MatchList = new List<GameObject>();
	
	TextMesh myText;
	
	// Use this for initialization
	void Start () {
		
		charTransform = transform; //sets local variable to characters current transform
		charController = GetComponent<CharacterController>(); //loads in character controller object
		playerDirection = Directions.Right;
		animChild = transform.FindChild("Character 001");
		myText = GetComponentInChildren<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		Jump(); //Processes jump
		Gravity(); // Applies gravity
		GetInputs(); //Get player input
		//Turn (); // Turn player
		Push(); //push cube
		Move(); // move player
		Fire (); //Player fire
		Pickup(); //player picks up object
		LightObject(); //Lights lightable object
		
	}
	
	/// <summary>
	/// Gets the inputs.
	/// </summary>
	void GetInputs()
	{
		horizontal = Input.GetAxisRaw("Horizontal");
		vertical = Input.GetAxisRaw("Vertical");
	}
	
	/// <summary>
	/// Controls the player turning
	/// </summary>
	void Turn()
	{
		if(Input.GetButton("TurnRight"))
		{
			//turns character to the right
			rotation += 0.1f;	
		}
		else if(Input.GetButton("TurnLeft"))
		{
			//turns character to the left
			rotation += -0.1f;
		}
		else
		{	
			rotation = 0;
		}
		
		charTransform.Rotate(0,rotation,0);
		
	}
	
	/// <summary>
	/// Function to control the character movement
	/// </summary>
	void Move()
	{
		if(Input.GetButton("Vertical"))
		{
			
		}
		else
		{
			
		}
		
		if(onladder)
		{
			//myText.text = "On Ladder";
			
		}
		else
		{
			//myText.text = "Not on Ladder";
			
		}
		
		if(horizontal != 0 || vertical != 0)
		{
			
			if(vertical > 0)
			{
				//move character towards direction
				//charMovement = charTransform.TransformDirection(Vector3.left);
				//Debug.Log ("GOING UP");	
				charMovement = new Vector3(0,climbSpeed,0); //charTransform.TransformDirection(Vector3.up);
			}
			else if(vertical < 0)
			{
				//Debug.Log ("GOING DOWN");	
				charMovement = new Vector3(0,-climbSpeed,0);// charTransform.TransformDirection(Vector3.down);
			}
			
			
			//normalise the movement to the public move speed
			
			else if(horizontal > 0)
			{
				
				playerDirection = Directions.Right;
				if(charTransform.rotation.y > 0)
				{
					charTransform.Rotate(0,180,0);
				}
				charMovement = new Vector3(-1,0,0); //charTransform.TransformDirection(Vector3.left);
				animChild.GetComponent<Animation>().CrossFade("Walk",0.2f);
				if(charController.isGrounded)
				{
					sprintTime += 1 * Time.deltaTime;
				}
				//charTransform.Rotate(0,180,0);
			}
			else if(horizontal < 1)
			{
				
				playerDirection = Directions.Left;
				if(charTransform.rotation.y < 1)
				{
					charTransform.Rotate(0,180,0);
				}
				charMovement = new Vector3(1,0,0); //charTransform.TransformDirection(Vector3.left);
				animChild.GetComponent<Animation>().CrossFade("Walk",0.2f);
				if(charController.isGrounded)
				{
					sprintTime += 1 * Time.deltaTime;	
				}
			}
			
//			//if pushing cube isn't null and "interact cube button is down
//			if(grab)
//			{
//				Debug.Log("YOU HAVE THE CUBE");
//				//if movement is away from cube set tug animation
//				
//				//if movement is towards cube set shove animation
//				
//				//temporarily nerf move speed
//				
//				//move character and "pushing cube" by the same amount
//				charMovement = charMovement.normalized * moveSpeed;
//				
//				if(pushingCube)
//				{
//					pushingCube.rigidbody.isKinematic = false;
//				}
//			
//				
//				
//				//pushingCube.transform.position += charMovement.normalized * moveSpeed;
//			}
//			//else do normal movement
//			else
//			{
//				if(pushingCube)
//				{
//					pushingCube.rigidbody.isKinematic = true;	
//				}
//				charMovement = charMovement.normalized * moveSpeed;
//			}
				
			//Debug.Log(charTransform.rotation.y);
		}
		else
		{
			//stop the character moving
			charMovement = Vector3.zero;	
			animChild.GetComponent<Animation>().Stop();
			sprintTime = 0f;
		}
		
		//
		
		if(sprintTime > sprintLimit)
		{
			charMovement = charMovement.normalized * sprintSpeed;
		}
		else
		{
			charMovement = charMovement.normalized * moveSpeed;
		}
			
		
		//apply any vertical movement
		if(!onladder)
		{
			charMovement = new Vector3(charMovement.x, verticalMovement, charMovement.z);
		}
		
		//if have picked up object
		if(pickupObj && pickedUp)
		{
			if(pickupObj.tag == "CandleSphereTag")
			{
				pickupObj.GetComponent<Rigidbody>().position = charController.transform.position;
				Debug.Log("YOU HAVE CANDLESHPERE");	
			}
		}
		
		if (grab)
		{
			//myText.text = "grabbing";
		}
		else
		{
			//myText.text = "NOOOO";
		}
		
		//move the character
		charController.Move(charMovement * Time.deltaTime);
		
		if (grab && charMovement.x != 0)
			pushingCube.GetComponent<Rigidbody>().AddForce(charMovement * 2, ForceMode.Impulse);
	}
	
	void Push()
	{				
		//pushingCube.transform.position = transform.position;
		
		if(pushingCube && Input.GetButton("Grab"))
		{		
			if (pushingCube)
			{
				grab = true;	
//				pushingCube.rigidbody.isKinematic = false;
			}		
		}
		else
		{
			grab = false;	
			if (pushingCube)
			{
//				pushingCube.rigidbody.isKinematic = true;
			}
		}
	}
	
	public void AssignPickup(GameObject obj)
	{
		pickupObj = obj;
	}
	
	public void UnnassignPickup()
	{
		pickupObj = null;	
	}
	
	void Pickup()
	{
		if(Input.GetMouseButtonDown(1) && pickupObj)
		{
			if(!pickedUp)
			{
				pickedUp = true;
				Debug.Log("You picked up a ...");
			}
			else
			{
				pickedUp = false;
				Debug.Log("You dropped up a ...");
			}	
		}
	}
	
	/// <summary>
	/// Function to control the character Jump.
	/// </summary>
	void Jump()
	{
		if(Input.GetButtonDown("Jump"))
		{
			if(charController.isGrounded || onladder)
			{
				verticalMovement = jumpPower;
				onladder = false;
			}
		}
	}
	
	/// <summary>
	/// Applies gravity to the character
	/// </summary>
	void Gravity()
	{
		//if character is not already on the ground
		if(!charController.isGrounded && !onladder)
		{
			verticalMovement -= gravityPower * Time.deltaTime;
			if(verticalMovement < -50)
			{
				verticalMovement = 50;	
			}
		}
	}
	
	/// <summary>
	/// Fires bullet.
	/// </summary>
	void Fire()
	{
		if(Input.GetMouseButtonDown(0) && !pickedUp)
		{
			//Debug.Log("Fire !!!");
			GameObject oneMatch;
			//Quaternion playerFireRotation = charTransform.rotation * Quaternion.Euler(0,180,0);
			//Quaternion playerFireRotation = charTransform.rotation * Quaternion.Euler(0,0,0);
			//oneMatch = Instantiate(MatchPrefab,charTransform.position,playerFireRotation) as GameObject;
			//oneMatch = Instantiate(MatchPrefab,charTransform.position,Quaternion.identity) as GameObject;
			oneMatch = Instantiate(MatchPrefab,charTransform.position,charTransform.rotation) as GameObject;
			MatchList.Add(oneMatch);
		}
	}
	
	void LightObject()
	{
		if(pickupObj && pickedUp)
		{
			if(pickupObj.tag == "CandleSphereTag")
			{
				if(!pickupObj.transform.FindChild("CandleSphereLight").GetComponent<Light>().enabled && Input.GetMouseButtonDown(0))
				{
					pickupObj.transform.FindChild("CandleSphereLight").GetComponent<Light>().enabled = true;
				}
			}
		}
	}
	
	public void AssignPushingBlock(GameObject obj)
	{
		pushingCube = obj;
		Debug.Log("You can interact with the box");
	}
	
	public void UnnassignPushingBlock()
	{
		if(pushingCube)
		{
			pushingCube = null;	
			Debug.Log("You dropped the box");
		}
	}
	
	//public void AssignCandleSphere(GameObject obj)
	//{
//		candleSphere = obj;
//		Debug.Log("You have the candlesphere");
//	}
	
	// this script pushes all rigidbodies that the character touches
	float pushPower = 2.0f; 
	
//	void OnControllerColliderHit (ControllerColliderHit hit)
//	{  	
//		
//		if(hit.collider.tag == "Cube" )
//		{
//			Debug.Log("Push cube");
//			
//			Rigidbody body = hit.collider.attachedRigidbody;     // no rigidbody    
//		
//			if (body == null || body.isKinematic) 
//			{ return; }
//			
//		    // We dont want to push objects below us    
//			if (hit.moveDirection.y < -0.3) 
//			{ return; }     
//		
//			// Calculate push direction from move direction,
//		    // we only push objects to the sides never up and down    
//		
//			Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);    
//			// If you know how fast your character is trying to move,    
//			// then you can also multiply the push velocity by that.
//		
//		    // Apply the push    
//			body.velocity = pushDir * pushPower;
//		}
//		
//	}
}
