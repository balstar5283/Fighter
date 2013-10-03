using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {
	
	public float movementSpeed = 10.0f;
	public float jumpSpeed = 5.0f;
	public float gravitySpeed = 10f;
	private Vector3 moveDirection;
	private CharacterController controller;


	// Use this for initialization
	void Start () {	
		
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= movementSpeed;
			
			if(Input.GetButton("Jump")) {
				moveDirection.y = jumpSpeed;
			}      
		}
		
		moveDirection.y -= gravitySpeed * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
}
