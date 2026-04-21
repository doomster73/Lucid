using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;

//Character controller must be available
[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour {
	
	public bool onLadder
    {
        get { return onladder; }
        set { onladder = value; }
    }
	
	//property to control whether player in light
	
	public int InLightSource {
		get { return inLightSource; }
        set { inLightSource = value; }
	}

    //public enum Directions
    //{
    //    Left = 1,
    //    Right = 2,
    //    Up = 4,
    //    Down = 8
    //};

    public enum Directions
    {
        Left,
        Right,
        Up,
        DOwn
    };

    enum animState
    {
        matchDraw,
        Other
    };
	
	Transform animChild;
	//setup variables
	public float moveSpeed; //public var movement speed
	public float sprintSpeed; //public var sprint speed
	public float crawlSpeed; // public var crawl speed
	public float jumpPower; //public var jump force
	public float gravityPower; //public var gravity force
	public float climbSpeed; //public var for climbing speed
	public float sprintLimit; //public var for time before sprint starts
	
	Transform charTransform; //hold character transform
	Vector3 charMovement; //holds character movement
	float slideMovement;
	Vector3 floorNormal;
	CharacterController charController; // hold character controller object
	
	public Directions playerDirection;
	
	float verticalMovement; //holds the vertical movement for use in jumps
	
	public float horizontal; //hold horizontal movement
	public float vertical; //hold vertical movement
	float rotation; //hold rotation movement
	float sprintTime; //hold sprint timer
	
	bool grab; //checks for object grabbed
	bool pickedUp = false; // checks picked up object
	bool crouching = false; //is player crouching
	
	GameObject fadeCamera; //used for player fade
	bool fadeComplete = false; //player death fade is complete
	public float fadeTime = 3f;
	
	public float ladderHopSpeed = 5.0f; //ladder hop speed
	public bool offLadder = false; //player getting off ladder
	
	int inLightSource = 0; //number of lights colliding
	
	float deathTimer = 0f;
	public float deathTimerLimit = 10f;
	bool playerDying = false;
	bool playerDead = false;
	bool fading = false;
	
	public bool playerReturnToCheckPoint = false; //returns player to last checkpoint
	
	private bool onladder = false; //variable to check if player on ladder.

    animState myAnimstate = animState.Other;
	
	GameObject pushingCube;
	GameObject pickupObj;
	GameObject switchObj;
	
	GameObject theMatch;
	MatchScript mScript;
	public bool holdingMatch = false; //player is holding match
	
	public float candleSphereRollDistance = 5f; //roll distance for candlesphere
	
	GUIText guiInLight;
	
	public GameObject MatchPrefab;
	List<GameObject> MatchList = new List<GameObject>();
	
	TextMesh myText;
	
	Cam gameCam;
	
	// Use this for initialization
	void Start () {
		
		charTransform = transform; //sets local variable to characters current transform
		charController = GetComponent<CharacterController>(); //loads in character controller object
		playerDirection = Directions.Right;
        animChild = transform.FindChild("Boy-test-001-unity");
		myText = GetComponentInChildren<TextMesh>();
		guiInLight = GameObject.Find("GUI Text").GetComponent<GUIText>();
		fadeCamera = GameObject.Find("Main Camera");
		gameCam = Camera.main.GetComponent<Cam>();
		
	}
	
	// Update is called once per frame
	void Update () {
		Jump(); //Processes jump
		Gravity(); // Applies gravity
		ReturnToCheckPoint(); //Returns player to last checkpoint
		GetInputs(); //Get player input
		//Turn (); // Turn player
		Push(); //push cube
		Move(); // move player
		Fire (); //Player fire
		Pickup(); //player picks up object
		//LightObject(); //Lights lightable object
		SwitchLight(); //player switch light
		IsInLight(); //player in light
		PlayerDying(); //player dying
		//PlayerDead(); // Player Dead
		CameraZoom();
	}
	
	/// <summary>
	/// Gets the inputs.
	/// </summary>
	void GetInputs()
	{
		horizontal = Input.GetAxisRaw("Horizontal");
		vertical = Input.GetAxisRaw("Vertical");
	}
	
	void IsInLight()
	{
		if(inLightSource > 0 || holdingMatch )
		{
			guiInLight.text = "Player in Light";
			deathTimer = 0f;
			
			AudioListener.volume = 1.0f;
			
			//need to work out how to fade back up again.
			//fadeCamera.transform.GetComponent<FadeScript>().StartFade(new Color(0,0,0,0) , fadeTime);
		}
		else if(inLightSource < 1 || !holdingMatch)
		{		
			guiInLight.text = "Player in Dark";
			if(!playerDying || !playerDead)
			{
				deathTimer += Time.deltaTime;
				if(Application.loadedLevel == 3)
				{
					//gameCam.audio.volume = 1.0f;
					AudioListener.volume = 1.0f;
				}
				else
				{
					AudioListener.volume -= Time.deltaTime / 3;
				}
				
				//gameCam.audio.volume = 0.0f;
			}
		}
	}
	
	void PlayerDying()
	{
		if(deathTimer > deathTimerLimit && inLightSource < 1 && !holdingMatch && !fadeComplete)
		{
			if(!fading)
			{
				fadeCamera.transform.GetComponent<FadeScript>().StartFade(Color.black, fadeTime);
				fading = true;
			}
			playerDying = true;
			fadeComplete = fadeCamera.transform.GetComponent<FadeScript>().fadeComplete;
			if(fadeComplete)
			{
				PlayerDead();
			}
		}
		else
		{
			if(playerDying)
			{
				fadeCamera.transform.GetComponent<FadeScript>().StartFade(Color.clear, fadeTime);
				fadeComplete = false;
				fading = false;
			}
			playerDying = false;	
			//fadeComplete = fadeCamera.transform.GetComponent<FadeScript>().fadeComplete;
		}
//		if(playerDying)
//		{
//			fadeComplete = fadeCamera.transform.GetComponent<FadeScript>().fadeComplete;	
//		}
	}
	
	void CameraZoom()
	{
		if (Input.GetButtonDown("Zoom"))
		{
			gameCam.IsZooming = true;
		}
		else if (Input.GetButtonUp("Zoom"))
		{
			gameCam.IsZooming = false;
		}
	}
	
	void PlayerDead()
	{
		playerDying = false;
		deathTimer = 0;
		fading = false;
		playerReturnToCheckPoint = true;
        fadeCamera.transform.GetComponent<FadeScript>().fadeComplete = false;
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
		if(horizontal != 0 || vertical != 0)
		{
			if(crouching)
			{				
				if(horizontal > 0)
				{
					playerDirection = Directions.Right;
					if(charTransform.rotation.y > 0)
					{
						charTransform.Rotate(0,180,0);
					}
					charMovement = new Vector3(-1,0,0); //charTransform.TransformDirection(Vector3.left);
                    if (myAnimstate == animState.Other)
                    {
                        animChild.GetComponent<Animation>().CrossFade("crawl",0.2f);
                        

                    }
					
				}
				else if(horizontal < 0)
				{
					playerDirection = Directions.Left;
					if(charTransform.rotation.y < 1)
					{
						charTransform.Rotate(0,180,0);
					}
					charMovement = new Vector3(1,0,0); //charTransform.TransformDirection(Vector3.left);
                    if (myAnimstate == animState.Other)
                    {
                        
                        animChild.GetComponent<Animation>().CrossFade("crawl", 0.2f);
                    }
					
				}
				else
				{
                    if (myAnimstate == animState.Other)
                    {

                        
                        animChild.GetComponent<Animation>().CrossFade("crouch", 0.2f);
                    }
						
				}
				
				/*if(mScript.isLit == true)
				{
					sprintSpeed = 10f;	
					moveSpeed = 10f;
				}
				else
					moveSpeed = 15f; sprintSpeed = 15f;*/
				
			}
			else
			{
				//charController.height = 3.68f;
				//charController.height = 1.05f; //for testing the playertest scene
				
				if(vertical > 0)
				{
					//move character towards direction
					//charMovement = charTransform.TransformDirection(Vector3.left);
					//Debug.Log ("GOING UP");	
					charMovement = new Vector3(0,climbSpeed,0); //charTransform.TransformDirection(Vector3.up);
					if(offLadder)
					{
						Vector3 hop = Vector3.zero;
						// perform off-ladder hop
						if(playerDirection == Directions.Right)
						{
							hop = new Vector3(-1,4,0);
						}
						else
						{
							hop = new Vector3(1,4,0);
						}
						charMovement = (hop * ladderHopSpeed) * Time.deltaTime;
						offLadder = false;	
					}
				}
				else if(vertical < 0)
				{
					//Debug.Log ("GOING DOWN");	
					charMovement = new Vector3(0,-climbSpeed,0);// charTransform.TransformDirection(Vector3.down);
				}
				else if(horizontal > 0 && !onladder)
				{
					
					playerDirection = Directions.Right;
					if(charTransform.rotation.y > 0)
					{
						charTransform.Rotate(0,180,0);
					}
					charMovement = new Vector3(-1,0,0); //charTransform.TransformDirection(Vector3.left);
                    if (holdingMatch)
                    {
                        if (myAnimstate == animState.Other)
                        {
                            
                            animChild.GetComponent<Animation>().CrossFade("walkwithmatch", 0.2f);
                        }
						
						moveSpeed 	= 10f;
						sprintSpeed = 10f;
                        
                    }
                    else
                    {
                        if (myAnimstate == animState.Other)
                        {
                            
                            animChild.GetComponent<Animation>().CrossFade("run", 0.2f);
                        }
						
						moveSpeed 	= 15f;
						sprintSpeed = 15f;
                        
                    }
					if(charController.isGrounded)
					{
						sprintTime += 1 * Time.deltaTime;
					}
					//charTransform.Rotate(0,180,0);
				}
				//normalise the movement to the public move speed
			
			
				else if(horizontal < 1 && !onladder)
				{
					
					playerDirection = Directions.Left;
					if(charTransform.rotation.y < 1)
					{
						charTransform.Rotate(0,180,0);
					}
					charMovement = new Vector3(1,0,0); //charTransform.TransformDirection(Vector3.left);
                    if (holdingMatch)
                    {
                        if (myAnimstate == animState.Other)
                        {
                            
                            animChild.GetComponent<Animation>().CrossFade("walkwithmatch", 0.2f);
                        }
						
						moveSpeed 	= 10f;
						sprintSpeed = 10f;
                    }
                    else
                    {
                        if (myAnimstate == animState.Other)
                        {
                            
                            animChild.GetComponent<Animation>().CrossFade("run", 0.2f);
                        }
						
						moveSpeed 	= 15f;
						sprintSpeed = 15f;

                    }
					
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
//				
			
			}
				
			
	
		}
		else
		{
			//stop the character moving
			charMovement = Vector3.zero;
            if (charController.isGrounded && !crouching)
            {
                if (holdingMatch)
                {
                    if (myAnimstate == animState.Other)
                    {
                        
                        animChild.GetComponent<Animation>().CrossFade("idlewithmatch", 0.05f);
                    }
                }
                else
                {
                    if (myAnimstate == animState.Other)
                    {
                        
                        animChild.GetComponent<Animation>().CrossFade("idle", 0.05f);
                    }
                }
                
            }
            else if(charController.isGrounded && crouching)
            {
                if (myAnimstate == animState.Other)
                {
                    
                    animChild.GetComponent<Animation>().CrossFade("crouch", 0.05f);
                }

            }
			sprintTime = 0f;
		}
		
		
		//
		
		if(sprintTime > sprintLimit && !crouching)
		{
			charMovement = charMovement.normalized * sprintSpeed;
		}
		else if(crouching)
		{
			charMovement = charMovement.normalized * crawlSpeed;
		}
		else
		{
			charMovement = charMovement.normalized * moveSpeed;	
		}
		
		//return player to last checkpoint
		if(playerReturnToCheckPoint)
		{
			Vector3 respawnPos = new Vector3(GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().CurrentCheckPoint.position.x,
				GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().CurrentCheckPoint.position.y, 2.3f);
			
			charTransform.position = respawnPos;
				
			//charTransform.position = GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().CurrentCheckPoint.position;
			
			//charTransform.position.y = GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().CurrentCheckPoint.position.y;
			GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().Matches = GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().CurrentCheckPoint.transform.GetComponent<CheckPointScript>().NumberofMatches;
			inLightSource = 0;
			playerReturnToCheckPoint = false;
			fadeCamera.transform.GetComponent<FadeScript>().SetScreenOverlayColor(Color.clear);
			fadeComplete = false;
		}
		
		//crouch
		if(Input.GetButtonDown("Crouch") && crouching == false)
		{
			crouching = true;
			charController.height = 0.69f;
			//charTransform.Rotate(0,0,90);
		}
		else if(Input.GetButtonDown("Crouch") && crouching == true)
		{
			charTransform.position = new Vector3(charTransform.position.x,charTransform.position.y + 5.0f,charTransform.position.z);
			charController.height = 4f;
			crouching = false;	
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
				
				float PUOx = charController.transform.position.x + 1.0f;
				
				if(playerDirection == Directions.Left)
				{
					PUOx = charController.transform.position.x + 1.0f;
				}
				else if(playerDirection == Directions.Right)
				{
					PUOx = charController.transform.position.x - 1.0f;	
				}
				
				pickupObj.transform.position = new Vector3(PUOx, charController.transform.position.y + 3f, 
					charController.transform.position.z);
				//Debug.Log("YOU HAVE CANDLESHPERE");	
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

        if (!charController.isGrounded && !onladder)
        {
            if (myAnimstate == animState.Other)
            {
                
                animChild.GetComponent<Animation>().CrossFade("jump", 0.2f);
            }

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
	
	void SwitchLight()
	{
		if(Input.GetMouseButtonDown(0) && switchObj)
		{
			//switch
			switchObj.GetComponent<SwitchScript>().SwitchOn = true;
		}
	}
	
	//Pickup Objects
	public void AssignPickup(GameObject obj)
	{
		pickupObj = obj;

        //EditorApplication.isPaused = true;
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
				Debug.Log("You picked up a ...", pickupObj);
				pickupObj.GetComponent<Collider>().enabled = false;
			}
			else
			{
				pickedUp = false;
				//Debug.Log("You dropped up a ...", pickupObj);
				
				Debug.Log("doing");
				pickupObj.GetComponent<Collider>().enabled = true;
			}	
		}
	}
	
	//switches
	public void AssignSwitch(GameObject obj)
	{
		switchObj = obj;
	}
	
	public void UnnassignSwitch()
	{
		switchObj = null;	
	}
	
	/// <summary>
	/// Function to control the character Jump.
	/// </summary>
	void Jump()
	{
        if (Input.GetButtonDown("Jump") && !crouching && !pickedUp && !holdingMatch)
        {
            if (charController.isGrounded || onladder)
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
				verticalMovement = -50;	//50 will make him bounce (any positive value)
			}
		}
	}

    void DrawMatch()
    {
        GameObject oneMatch = Instantiate(MatchPrefab, charTransform.position, charTransform.rotation) as GameObject;
        mScript = oneMatch.GetComponent<MatchScript>();

        MatchList.Add(oneMatch);
        GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().Matches -= 1;

        holdingMatch = true;
    }
	
	/// <summary>
	/// Fires bullet.
	/// </summary>
	void Fire()
	{
        //Debug.Log("Player match : " + holdingMatch.ToString());

		if(Input.GetMouseButtonDown(0) && !pickedUp && !switchObj && !holdingMatch && Time.timeScale == 1 && !crouching)
		{
		    var matches = GameObject.FindGameObjectWithTag("GameStats").GetComponent<GameStats>().Matches;
		    if(matches > 0)
			{
                myAnimstate = animState.matchDraw;
                
                StartCoroutine(SetAnimToOther(0.67f));
                animChild.GetComponent<Animation>().CrossFade("drawmatch", 0.2f);
                Invoke("DrawMatch", 0.67f);
				
			}
		}
		else if(Input.GetMouseButtonDown(0) && !pickedUp && !switchObj && holdingMatch && Time.timeScale == 1)
		{
			mScript.DoThrow();		
		}
		
		//candlesphere throw
		if(Input.GetMouseButtonUp(0) && pickedUp && !switchObj)
		{
			if(pickupObj != null && (pickupObj.tag == "CandleSphereTag" && pickupObj.transform.FindChild("CandleSphereLight").GetComponent<Light>().enabled))
			{
                
//				switch(playerDirection)
//				{
//				case Directions.Left:
//					pickupObj.transform.position = new Vector3(charController.transform.position.x + candleSphereRollDistance,charController.transform.position.y, 
//					charController.transform.position.z);
//					break;
//				case Directions.Right:
//					pickupObj.transform.position = new Vector3(charController.transform.position.x - candleSphereRollDistance,charController.transform.position.y, 
//					charController.transform.position.z);
//					break;
//				}
				
				//MoveObject(pickupObj.transform, pickupObj.transform.position, new Vector3(0,0,0), 10.0f);
				pickedUp = false;
				
				Transform child = pickupObj.transform.FindChild("PlayerCollider");
				
				CandleSphereScript script = child.GetComponent<CandleSphereScript>();
				
//				script.SetPropulsion(new Vector3(-400,0,0));

				pickupObj.GetComponent<Collider>().enabled = true;
				UnnassignPickup();
				Debug.Log("if");
			}
			else
			{
				if(pickupObj.tag == "CandleSphereTag")
				{
					if(!pickupObj.transform.FindChild("CandleSphereLight").GetComponent<Light>().enabled)
					{
						pickupObj.transform.FindChild("CandleSphereLight").GetComponent<Light>().enabled = true;
						pickupObj.transform.GetComponent<RayCastLight>().lightLit = true;
					}
				}
				Debug.Log("else");
			}
         
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
					pickupObj.transform.GetComponent<RayCastLight>().lightLit = true;
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
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "OutOfBounds")
		{
			//Destroy(gameObject);
			//Debug.Log("Dead");
			//Debug.Log("Player Out Of Bounds");
		}
		
		//if(other.tag == "Player")
		//{
			//Debug.Log ("Player Dead");	
		//}
		
	}
	
	void ReturnToCheckPoint()
	{
		if(Input.GetButtonDown("CheckPointReturn"))
		{
			print("return to checkpoint");
			playerReturnToCheckPoint = true;
		}
	}
	
	/// <summary>
	/// Moves the object.
	/// </summary>
	/// <param name='objToMove'>
	/// Object to move.
	/// </param>
	/// <param name='startPos'>
	/// Start position.
	/// </param>
	/// <param name='endPos'>
	/// End position.
	/// </param>
	/// <param name='moveTime'>
	/// Move time.
	/// </param>
	void MoveObject (Transform objToMove, Vector3 startPos, Vector3 endPos, float moveTime)
	{
		float i = 0.0f;
    	float rate = 1.0f / moveTime;
   		while (i < 1.0f) 
		{
        	i += Time.deltaTime * rate;
        	objToMove.position = Vector3.Lerp(startPos, endPos, i);
		}
	}
	
	//public void AssignCandleSphere(GameObject obj)
	//{
//		candleSphere = obj;
//		Debug.Log("You have the candlesphere");
//	}
	
	// this script pushes all rigidbodies that the character touches
	float pushPower = 2.0f;

    /// <summary>
    /// This is the slide function
    /// </summary>
    /// <param name="hit"></param>
    //void OnControllerColliderHit(ControllerColliderHit hit)
    //{
    //    if ((charController.collisionFlags & CollisionFlags.Below) > 0)
    //    {
    //        if (hit.gameObject.tag == "Floor")
    //        {
    //            floorNormal = hit.normal;

    //            charTransform.up = floorNormal;

    //        }
    //    }

    //}

    IEnumerator SetAnimToOther(float delay)
    {
        yield return new WaitForSeconds(delay);
        myAnimstate = animState.Other;

    }
}
