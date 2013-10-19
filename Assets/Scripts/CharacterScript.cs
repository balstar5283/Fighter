using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {
	
	public float movementSpeed = 10.0f;
	public bool justTouchedPlatform = false;
	public float jumpSpeed = 10.0f;
	public float gravitySpeed = 20f;
	public float maxSpeed = 10.0f;
	public float airFactor = .5f;
	public float pullDown = 50f;
	public int attacksLeft = 3;
	public string horizontalAxis = "Horizontal1";
	public string jumpAxis = "Jump1";
	public int facing;
	public string attackButton = "Fire1";
	public bool isJumping;
	public bool attackDone = true;
	public Transform otherPlayer;
	private string itemName;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	private AnimationController animController;

	// Use this for initialization
	void Start () {	
		animController = GetComponentInChildren<AnimationController>();
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(justTouchedPlatform = true) {
			pullDown = 1f;
		}
		
		if(transform.position.z != 0.0f) {
			transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
		}
		
		if(controller.isGrounded) {
			justTouchedPlatform = false;
			isJumping = false;
			moveDirection = new Vector3(Input.GetAxis(horizontalAxis), 0, 0);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= movementSpeed;
						
			if(Input.GetButtonDown(jumpAxis)) {
				isJumping = true;
				moveDirection.y = jumpSpeed;
			}

		}
			
		else {
			moveDirection.x += Input.GetAxis(horizontalAxis) * airFactor;
		}
		
		//Apply pull down force
		if(!isJumping && justTouchedPlatform == false) {
			pullDown = 50f;
			moveDirection.y -= gravitySpeed * Time.deltaTime * pullDown;
			
		}
		
		else {
			moveDirection.y -= gravitySpeed * Time.deltaTime;
		}
		
		//Move player
		controller.Move(moveDirection * Time.deltaTime);
		
		//Maintain max speed
		if(moveDirection.x > maxSpeed) {
			moveDirection.x = maxSpeed;
		}
		
		if(moveDirection.x < -maxSpeed) {
			moveDirection.x = -maxSpeed;
		}
		
		//Attack!
		if(Input.GetButtonDown(attackButton)) {
			animController.performAttack();
			attackDone = false;
			attacksLeft--;
		}
		
		//Face players at one another
		if (otherPlayer.position.x > transform.position.x) {
			facing = 0;
		}
		else {
			facing = 1;
		}
		
		animController.updateState(moveDirection.x, controller.isGrounded, facing);
		
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.gameObject.tag == "Gun" || hit.gameObject.tag == "Bat") {
			attacksLeft = 3;
			equipItem(hit.transform.gameObject.tag);
			Destroy(hit.gameObject);
		}
		
		if(hit.gameObject.name == "Platform" && controller.isGrounded) {
			isJumping = true;
			justTouchedPlatform = true;
		}
	}
	
	public void playAnimationDone(string type) {
		switch(type) {
		case "punch":
			attackDone = true;
			break;
		case "kick":
			break;
		case "gunFire":
			attackDone = true;
			break;
		case "swingBat":
			attackDone = true;
			break;
		}
	}
	
	void equipItem(string itemName) {
		if(itemName == "Bat") {
			animController.updateWeapon(AnimationController.WeaponType.BAT);
			print (gameObject.name + " got a bat!");
		}
		
		if(itemName == "Gun") {
			animController.updateWeapon(AnimationController.WeaponType.GUN);
			print (gameObject.name + " got a gun!");
		}
		
		
	}
}
