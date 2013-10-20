using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {
	
	public float movementSpeed = 10.0f;
	public bool beginMovement = false;
	public bool isNotGrounded = false;
	public float jumpSpeed = 10.0f;
	public float gravitySpeed = 20f;
	public float maxSpeed = 10.0f;
	public float airFactor = .5f;
	public float pullDown = 50f;
	public int attacksLeft = 0;
	public string downButton = "Down1";
	public string horizontalAxis = "Horizontal1";
	public string jumpAxis = "Jump1";
	public int facing;
	public string attackButton = "Fire1";
	public bool isJumping;
	public bool attackDone = true;
	public bool touchingPlatform;
	public bool isTakingDamage = false;
	public Transform otherPlayer;
	private string itemName;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	private AnimationController animController;

	// Use this for initialization
	void Start () {	
		animController = GetComponentInChildren<AnimationController>();
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		isNotGrounded = true;
		
		//Fix z position
		if(transform.position.z != 0.0f) {
			transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
		}
		
		if(Input.GetButtonDown(downButton) && isNotGrounded && isJumping && touchingPlatform) {
			gameObject.transform.Translate(new Vector3(0f, -.5f, 0f));
		}
		if (!isTakingDamage) {
			if(controller.isGrounded) {
				isNotGrounded = false;
				isJumping = false;
				moveDirection = new Vector3(Input.GetAxis(horizontalAxis), 0, 0);
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= movementSpeed;
							
				if(Input.GetButtonDown(jumpAxis)) {
					touchingPlatform = false;
					isJumping = true;
					moveDirection.y = jumpSpeed;
				}
			}
				
			else {
				moveDirection.x += Input.GetAxis(horizontalAxis) * airFactor;
			}
		}
		else {
			isTakingDamage = false;
		}
		
		//Apply pull down force
		if(!isJumping && !isNotGrounded) {
			moveDirection.y -= gravitySpeed * Time.deltaTime * pullDown;		
		}
		
		else {
			moveDirection.y -= gravitySpeed * Time.deltaTime;
		}
		
		//Maintain max speed
		if(moveDirection.x > maxSpeed) {
			moveDirection.x = maxSpeed;
		}
		
		if(moveDirection.x < -maxSpeed) {
			moveDirection.x = -maxSpeed;
		}
		
		//Attack!
		if(Input.GetButtonDown(attackButton) && attackDone && beginMovement) {

			attackDone = false;
			animController.performAttack();
		}
		
		//Face players at one another
		if (otherPlayer.position.x > transform.position.x) {
			facing = 0;
		}
		else {
			facing = 1;
		}
		
		//Move player
		if(beginMovement) {
			animController.updateState(moveDirection.x, controller.isGrounded, facing);
			controller.Move(moveDirection * Time.deltaTime);
		}
		
		else{
			animController.updateState(0, controller.isGrounded, facing);
			controller.Move(new Vector3(0, -gravitySpeed * Time.deltaTime, 0));
		}
		
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.gameObject.tag == "Gun" || hit.gameObject.tag == "Bat") {
			attacksLeft = 3;
			equipItem(hit.transform.gameObject.tag);
			Destroy(hit.gameObject);
		}
		
		if(hit.gameObject.name == "Platform" && controller.isGrounded ) {
			touchingPlatform = true;
			isJumping = true;
			isNotGrounded = true;
		}
		else {
			touchingPlatform = false;
		}
	}
	
	public void playAnimationDone(string type) {
		switch(type) {
		case "punch":
			attackDone = true;	
			break;
		case "kick":
			attackDone = true;
			break;
		case "gunFire":
		case "swingBat":
			attackDone = true;
			--attacksLeft;
			if(attacksLeft <= 0) {
				equipItem("None");
			}
			break;
		}
	}
	
	public void equipItem(string itemName) {

		attackDone = true;

		switch(itemName) {
		case "Bat":
			animController.updateWeapon(AnimationController.WeaponType.BAT);
			print (gameObject.name + " got a bat!");
			break;
		
		case "Gun":
			animController.updateWeapon(AnimationController.WeaponType.GUN);
			print (gameObject.name + " got a gun!");
			break;
		
		case "None":
			animController.updateWeapon(AnimationController.WeaponType.NONE);
			break;
		}
	}
	
	public void startMovement() {
		beginMovement = true;
	}
	
	public void knockBack() {
		isTakingDamage = true;
		//Facing Right
		if (facing == 0) {
			if(controller.isGrounded) {
				moveDirection = new Vector3(-5f, 10, 0);
			}
			else {
				moveDirection = new Vector3(-5f, 10, 0);
			}
		}
		else {
			if(controller.isGrounded) {
				moveDirection = new Vector3(5f, 10, 0);
			}
			else {
				moveDirection = new Vector3(5f, 10, 0);
			}
		}
	}
}
