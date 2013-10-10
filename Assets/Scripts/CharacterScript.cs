using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {
	
	public float movementSpeed = 10.0f;
	public float jumpSpeed = 10.0f;
	public float gravitySpeed = 20f;
	public float maxSpeed = 10.0f;
	public float airFactor = .5f;
	public string horizontalAxis = "Horizontal1";
	public string jumpAxis = "Jump1";
	private string itemName;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;


	// Use this for initialization
	void Start () {	
		
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis(horizontalAxis), 0, 0);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= movementSpeed;
						
			if(Input.GetButtonDown(jumpAxis)) {
				moveDirection.y = jumpSpeed;

			}
		}
			
		else {
			moveDirection.x += Input.GetAxis(horizontalAxis) * airFactor;
		}

		moveDirection.y -= gravitySpeed * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		
		if(moveDirection.x > maxSpeed) {
			moveDirection.x = maxSpeed;
		}
		
		if(moveDirection.x < -maxSpeed) {
			moveDirection.x = -maxSpeed;
		}	
		
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		if(hit.gameObject.tag == "Gun" || hit.gameObject.tag == "Bat") {
			equipItem(hit.transform.gameObject.tag);
			Destroy(hit.gameObject);
		}
	}

	
	void equipItem(string itemName) {
		if(itemName == "Bat") {
			//Item logic goes here
			print (gameObject.name + " got a bat!");
		}
		
		if(itemName == "Gun") {
			print (gameObject.name + " got a gun!");
		}
		
	}
	
	void changeHitBox(string itemName) {
	}
}
