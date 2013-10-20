﻿using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {
	
	public bool isJumping = false;
	public bool isWalking = false;
	public bool setWeapon = false;
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
	/*
	//Test variables
	public int tempDirection = 0;
	public bool tempGrounded = true;
	*/
	// Use this for initialization
	void Start () {
		updateFacing();
		attackSprite.playAnimation("blank");
		characterSprite.playAnimation("idle");
	}
	
	// Update is called once per frame
	void Update () {
		/*
		//Testing script
		updateState(Input.GetAxis("Horizontal1"), tempGrounded, tempDirection);
		if(Input.GetKeyDown(KeyCode.J)){
			if (tempDirection == 0) {
				tempDirection = 1;
			}
			else 
				tempDirection = 0;
		}
		if(Input.GetKeyDown(KeyCode.K)) {
			tempGrounded = !tempGrounded;
		}
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			updateWeapon(WeaponType.NONE);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			updateWeapon(WeaponType.GUN);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3)) {
			updateWeapon(WeaponType.BAT);
		}
		*/
			
			
	}
	
	public void updateWeapon(WeaponType type) {
		setWeapon = true;
		currentWeapon = type;
	}
	
	public void updateState(float moveDirection, bool isGrounded, int facing) {

		//Player has landed
		if (isJumping && isGrounded) {
			isJumping = false;
			addJumpOffset();
			SendMessageUpwards("playAnimationDone", characterSprite.currentAnimationString);
			characterSprite.playAnimation("idle");
			if (characterSprite.kickHitBox != null) {
				characterSprite.kickHitBox.disableAttack();
			}
		}
		//Player stands still and jump
		else if (!isJumping && !isGrounded) {
			isJumping = true;
			addJumpOffset();
			isWalking = false;
			characterSprite.playAnimation("jump");
		}
		
		//If player starts moving, play walk animation
		else if (moveDirection != 0 && !isWalking && isGrounded){
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
			//Player has jumped
			if (!isGrounded) {
				isWalking = false;
				isJumping = true;
				characterSprite.playAnimation("jump");
			}
			else if (isGrounded && this.facing == facing) {
				//If standing still then play idle
				if (moveDirection == 0) {
					characterSprite.playAnimation("idle");
					isWalking = false;
				}
				//If move direction change then play different animation
				else if ((xDirection > 0 && moveDirection < 0) || (xDirection < 0 && moveDirection > 0)) {
					playWalkDirection(this.facing, moveDirection);
				}
			}
			//If facing changes
			else if(isGrounded && this.facing != facing) {
				if (moveDirection == 0) {
					characterSprite.playAnimation("idle");
					isWalking = false;
				}
				else if (moveDirection != 0) {
					playWalkDirection(facing, moveDirection);
				}
			}
			
		}
		
		//Update weapon
		if (setWeapon) {
			setWeapon = false;
			// If it is a bat or a gun then remove attacking arm
			if (currentWeapon != WeaponType.NONE) {
				characterSprite.hideBackArm();
				if (currentWeapon == WeaponType.BAT) {
					attackSprite.playAnimation("displayBat");
					attackSprite.gameObject.transform.localPosition = new Vector3(0, 1.3f, 0);
				}
				else if (currentWeapon == WeaponType.GUN) {
					attackSprite.playAnimation("displayGun");
					attackSprite.gameObject.transform.localPosition = new Vector3(0, 0, 0);
				}
			}
			else {
				attackSprite.playAnimation("blank");
				attackSprite.gameObject.transform.localPosition = new Vector3(0, 0, 0);
				characterSprite.showBackArm();
			}
			if (isJumping)
				addJumpOffset();
		}
		//Updates facing
		if (this.facing != facing) {
			this.facing = facing;
			updateFacing();
		}
	}
	
	public void performAttack() {
		switch (currentWeapon) {
		case WeaponType.NONE:
			if (!isJumping) {
				characterSprite.hideBackArm();
				attackSprite.playAnimation("punch");
			}
			else {
				characterSprite.playAnimation("kick");
			}
			break;
		case WeaponType.BAT:
			attackSprite.playAnimation("swingBat");
			break;
		case WeaponType.GUN:
			attackSprite.playAnimation("gunFire");
			break;
		}
	}
	
	void updateFacing() {
		if (facing == 0) {
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else {
			transform.localScale = new Vector3(1, 1, 1);
		}
	}
	void playWalkDirection(int facing, float moveDirection) {
		//Right facing
		if (facing == 0 && moveDirection > 0) {
			characterSprite.playAnimation("backWalk");
		}
		else if (facing == 0 && moveDirection < 0){
			characterSprite.playAnimation("walk");
		}
		//Left facing
		else if (facing == 1 && moveDirection > 0){
			characterSprite.playAnimation("walk");
		}
		else{
			characterSprite.playAnimation("walk");
		}
	}
	
	void addJumpOffset() {
		if (isJumping) {
			attackSprite.transform.position -= new Vector3(0, .3f, 0);
		}
		else {
			attackSprite.transform.position += new Vector3(0, .3f, 0);
		}
	}
	
	public void playAnimationDone(string animation) {
		if (animation == "punch") {
			characterSprite.showBackArm();
		}
		else if (animation == "gunFire") {
			attackSprite.playAnimation("displayGun");
		}
		else if (animation == "swingBat") {
			attackSprite.playAnimation("displayBat");
		}
	}
}
