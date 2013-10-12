using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {
	
	public bool isJumping = false;
	public bool isWalking = false;
	// 0 - facing right, 1 - facing left
	public int facing = 0;
	public float xDirection;
	
	public enum WeaponType {
		NONE,
		GUN,
		BAT
	};
	public WeaponType currentWeapon = WeaponType.NONE;
	
	public MovementAnimation characterSprite;
	public MovementAnimation attackSprite;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//Testing script
		updateState(Input.GetAxis("Horizontal1"), true, 0);
	}
	
	public void updateWeapon(WeaponType type) {
		// If it is a bat or a gun then remove attacking arm
		if (type != WeaponType.NONE) {
			characterSprite.hideBackArm();
			if (type == WeaponType.BAT) {
				attackSprite.playAnimation("displayBat");
				attackSprite.gameObject.transform.Translate(new Vector3(0, 1, 0), attackSprite.gameObject.transform.parent);
			}
			else if (type == WeaponType.GUN) {
				attackSprite.playAnimation("displayGun");
			}
		}
	}
	
	public void updateState(float moveDirection, bool isGrounded, int facing) {
		if(moveDirection != 0 && !isWalking){
			isWalking = true;
			xDirection = moveDirection;
			if (this.facing == 0 && moveDirection > 0) {
				characterSprite.playAnimation("backWalk");
			}
			else if (this.facing == 0 && moveDirection < 0){
				characterSprite.playAnimation("walk");
			}
			//Left facing
			else if (this.facing == 1 && moveDirection > 0){
				characterSprite.playAnimation("walk");
			}
			else{
				characterSprite.playAnimation("walk");
			}
		}
		else if (isWalking) {
			//Checks to see if character is still walking in the same direction
			//If so do nothing
			if (isGrounded && this.facing == facing) {
				if (xDirection > 0 && moveDirection > 0) {
					return;
				}
				else if (xDirection < 0 && moveDirection < 0) {
					return;
				}
				//If standing still then play idle
				if (moveDirection == 0) {
					characterSprite.playAnimation("idle");
					isWalking = false;
				}
				//If move direction change then play different animation
				else if ((xDirection > 0 && moveDirection < 0) || (xDirection < 0 && moveDirection > 0)) {
					//Right facing
					if (this.facing == 0 && moveDirection > 0) {
						characterSprite.playAnimation("backWalk");
					}
					else if (this.facing == 0 && moveDirection < 0){
						characterSprite.playAnimation("walk");
					}
					//Left facing
					else if (this.facing == 1 && moveDirection > 0){
						characterSprite.playAnimation("walk");
					}
					else{
						characterSprite.playAnimation("walk");
					}
				}
			}
			
		}
	}
}
