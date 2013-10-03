using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {
	
	public float movementSpeed = 10.0f;
	public float jumpSpeed = 5.0f;
	public float gravitySpeed = 10f;
	public float maxSpeed = 10.0f;
	public float airFactor = .5f;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;


	// Use this for initialization
	void Start () {	
		
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(controller.isGrounded) {
				moveDirection = new Vector3(Input.GetAxis("Horizontal1"), 0, 0);
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= movementSpeed;
				
				if(Input.GetButton("Jump1")) {
					moveDirection.y = jumpSpeed;
				}      
			}
			
			else {
				moveDirection.x += Input.GetAxis("Horizontal1") * airFactor;
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
}
