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
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		
		pullDown = 1.0f;
		isNotGrounded = true;
		
		//Fix z position
		if(transform.position.z != 0.0f) {
			transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
		}

		if(controller.isGrounded) {
			isNotGrounded = false;
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
		if(!isJumping && !isNotGrounded) {
			pullDown = 50f;
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
		if(Input.GetButtonDown(attackButton)) {
			animController.performAttack();
			attackDone = false;
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
			controller.Move(new Vector3(0, -30, 0));
		}
		
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.gameObject.tag == "Gun" || hit.gameObject.tag == "Bat") {
			attacksLeft = 3;
			equipItem(hit.transform.gameObject.tag);
			Destroy(hit.gameObject);
		}
		
		if(hit.gameObject.name == "Platform" && controller.isGrounded) {
			isJumping = true;
			isNotGrounded = true;
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
		case "swingBat":
			attackDone = true;
			--attacksLeft;
			if(attacksLeft == 0) {
				equipItem("None");
			}
			break;
		}
	}
	
	void equipItem(string itemName) {
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
}
