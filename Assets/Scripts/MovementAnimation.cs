using UnityEngine;
using System.Collections;

public class MovementAnimation : MonoBehaviour {
	
	public int currentFrameCount;
	public int currentFrameIndex;
	public Vector3[] currentAnimation;
	public string currentAnimationString;
	
	//Used to reference the gameobject which needs to be notified of events
	public GameObject callback;
	
	public bool hideAttackArm = false;
	//Used to send message for attack
	public bool isAttack = false;
	public int attackFrame = 0;
	/*
	 * Animation data for walk, idle, jump
	 */
	// XOffset, YOffset, Framecount
	public Vector3[] walk = {
		new Vector3(.5f, .375f, 7),
		new Vector3(.625f, .375f, 7),
		new Vector3(.75f, .375f, 7),
		new Vector3(.875f, .375f, 7)
	};
	public Vector3[] idle = {
		new Vector3(0, .375f, 15),
		new Vector3(.125f, .375f, 15),
		new Vector3(.250f, .375f, 15)
	};
	public Vector3[] jump = {
		new Vector3(0, .125f, 5),
		new Vector3(.125f, .125f, 1000)
	};
	public Vector3[] victory = {
		new Vector3(.375f, .125f, 1000)
	};
	public Vector3[] damage = {
		new Vector3(0, .625f, 20)
	};
	public Vector3[] defeat = {
		new Vector3(.25f, 0, 40),
		new Vector3(.375f, 0, 1000)
	};
	
	/*
	 * Animation data for punch, gunshot, jump kicking, and swinging bat
	 */
	public Vector3[] punch = {
		new Vector3(0, .875f, 5),
		new Vector3(.125f, .875f, 5),
		new Vector3(.250f, .875f, 9),
		new Vector3(.125f, .875f, 5),
		new Vector3(0, .875f, 5)
	};
	public Vector3[] kick = {
		new Vector3(.25f, .125f, 1000)
	};
	public Vector3[] displayGun = {
		new Vector3(.375f, .875f, 1000)
	};
	public Vector3[] gunFire = {
		new Vector3(.5f, .875f, 5),
		new Vector3(.625f, .875f, 5),
		new Vector3(.75f, .875f, 5)
	};
	public Vector3[] displayBat = {
		new Vector3(0, .75f, 1000)
	};
	public Vector3[] swingBat = {
		new Vector3(.125f, .75f, 7),
		new Vector3(.25f, .75f, 7),
		new Vector3(.375f, .75f, 7)
	};
	public Vector3[] blank = {
		new Vector3(.875f, .875f, 1000)
	};
	
	
	public enum PlayType {LOOP, PLAYONCE, CLAMP, ZIGZAG};
	public PlayType currentPlaytype;
	//Use for Zigzag playtype
	public bool reverseAnimation = false;
	
	// Use this for initialization
	void Start () {
		//playAnimation("idle");
	}
	
	// Update is called once per frame
	void Update () {

		//Reverse animation
		if(reverseAnimation) {
			
			if(currentFrameCount >= currentAnimation[currentFrameIndex].z){
			currentFrameIndex--;
			if(currentFrameIndex < 0) {
				switch (currentPlaytype) {
				case PlayType.LOOP:
					currentFrameIndex = currentAnimation.Length - 1;
					break;
				case PlayType.ZIGZAG:
					currentFrameIndex = 0;
					reverseAnimation = false;
					break;
				case PlayType.PLAYONCE:
					playAnimation("blank");
					reverseAnimation = false;
					break;
				default:
					currentFrameIndex++;
					break;
				}
			}
			currentFrameCount = 0;
			updateFrame();
			}
			currentFrameCount++;
			
			return;
		}
		//Regular animation
		if (currentFrameCount >= currentAnimation[currentFrameIndex].z){
			currentFrameIndex++;
			if(currentFrameIndex >= currentAnimation.Length) {
				switch (currentPlaytype) {
				case PlayType.LOOP:
					currentFrameIndex = 0;
					break;
				case PlayType.ZIGZAG:
					currentFrameIndex = currentAnimation.Length -1;
					reverseAnimation = true;
					break;
				case PlayType.PLAYONCE:
					playAnimation("blank");
					break;
				case PlayType.CLAMP:
					currentFrameIndex--;
					break;
				default:
					currentFrameIndex--;
					break;
				}
			}
			currentFrameCount = 0;
			updateFrame();
		}
		currentFrameCount++;
		
	}
	
	public void playAnimation(string animationType) {
		reverseAnimation = false;
		isAttack = false;
		currentFrameCount = 0;
		currentFrameIndex = 0;
		currentAnimationString = animationType;
		switch (animationType) {
		case "walk":
			currentAnimation = walk;
			currentPlaytype = PlayType.LOOP;
			break;
		case "backWalk":
			currentAnimation = walk;
			reverseAnimation = true;
			currentPlaytype = PlayType.LOOP;
			break;
		case "idle":
			currentAnimation = idle;
			currentPlaytype = PlayType.ZIGZAG;
			break;
		case "jump":
			currentAnimation = jump;
			currentPlaytype = PlayType.CLAMP;
			break;
		case "damage":
			currentAnimation = damage;
			currentPlaytype = PlayType.PLAYONCE;
			break;
		case "victory":
			currentAnimation = victory;
			currentPlaytype = PlayType.CLAMP;
			break;
		case "defeat":
			currentAnimation = defeat;
			currentPlaytype = PlayType.CLAMP;
			break;
		case "punch":
			currentAnimation = punch;
			currentPlaytype = PlayType.PLAYONCE;
			isAttack = true;
			attackFrame = 2;
			break;
		case "kick":
			currentAnimation = kick;
			currentPlaytype = PlayType.CLAMP;
			isAttack = true;
			attackFrame = 0;
			break;
		case "gunFire":
			currentAnimation = gunFire;
			currentPlaytype = PlayType.PLAYONCE;
			isAttack = true;
			attackFrame = 0;
			break;
		case "swingBat":
			currentAnimation = swingBat;
			currentPlaytype = PlayType.PLAYONCE;
			isAttack = true;
			attackFrame = 0;
			break;
		case "blank":
			SendMessageUpwards("playAnimationDone", currentAnimationString);
			currentAnimation = blank;
			currentPlaytype = PlayType.CLAMP;
			break;
		case "displayGun":
			currentAnimation = displayGun;
			currentPlaytype = PlayType.CLAMP;
			break;
		case "displayBat":
			currentAnimation = displayBat;
			currentPlaytype = PlayType.CLAMP;
			break;
		default:
			currentAnimation = idle;
			currentPlaytype = PlayType.ZIGZAG;
			break;
		}
		
		updateFrame();
	}
	
	void updateFrame(){
		Vector2 offset = new Vector2(
			currentAnimation[currentFrameIndex].x,
			currentAnimation[currentFrameIndex].y);
		if (hideAttackArm) {
			offset.y -= .125f;
		}
		
		if (isAttack && (currentFrameIndex == attackFrame)) {
			//Send message to activate attackFrame
			SendMessageUpwards("doAttack", currentAnimationString);
			isAttack = false;
		}
		renderer.material.SetTextureOffset("_MainTex", offset);
	}
	
	public void faceLeft() {
		if (gameObject.transform.localScale.x < 0){
			gameObject.transform.localScale = 
				new Vector3(-gameObject.transform.localScale.x,
					gameObject.transform.localScale.y,
					gameObject.transform.localScale.z);
		}
	}
	
	public void faceRight() {
		if (gameObject.transform.localScale.x > 0){
			gameObject.transform.localScale = 
				new Vector3(-gameObject.transform.localScale.x,
					gameObject.transform.localScale.y,
					gameObject.transform.localScale.z);
			
		}
	}
	
	public void showBackArm() {
		hideAttackArm = false;
	}
	
	public void hideBackArm() {
		hideAttackArm = true;
	}
}
