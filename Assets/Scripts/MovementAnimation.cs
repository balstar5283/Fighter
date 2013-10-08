using UnityEngine;
using System.Collections;

public class MovementAnimation : MonoBehaviour {
	
	public int currentFrameCount;
	public int currentFrameIndex;
	public float characterDirectionOffset = 0;
	public Vector3[] currentAnimation;
	
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
	
	public enum PlayType {LOOP, PLAYONCE, CLAMP, ZIGZAG};
	public PlayType currentPlaytype;
	//Use for Zigzag playtype
	public bool reverseAnimation = false;
	
	// Use this for initialization
	void Start () {
		playIdle();
		//faceRight();
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
		if(currentFrameCount >= currentAnimation[currentFrameIndex].z){
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
	
	public void playWalk(){
		currentFrameCount = 0;
		currentFrameIndex = 0;
		currentAnimation = walk;
		currentPlaytype = PlayType.LOOP;
		updateFrame();
	}
	
	public void playIdle() {
		currentFrameCount = 0;
		currentFrameIndex = 0;
		currentAnimation = idle;
		currentPlaytype = PlayType.ZIGZAG;
		updateFrame();
		
	}
	
	void updateFrame(){
		renderer.material.SetTextureOffset("_MainTex",
			new Vector2(currentAnimation[currentFrameIndex].x,
						currentAnimation[currentFrameIndex].y + characterDirectionOffset));
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
}
