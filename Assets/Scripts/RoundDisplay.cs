﻿using UnityEngine;
using System.Collections;

public class RoundDisplay : MonoBehaviour {

	public float growthFactor = .01f;
	public float width;
	public float height;
	public Vector2 textureOffset = new Vector2(0, 0);
	
	public bool showMessage;
	public bool hideMessage;
	public bool showFight;
	
	public float displayTime = .5f;
	public float currentDisplayTime = 0;
	// Use this for initialization
	void Start () {
		showFight = false;
		hideMessage = false;
		showFight = false;
		showMessage = false;
		width = transform.localScale.x;
		height = transform.localScale.z;
		
		//Hides plane.
		transform.localScale = new Vector3(0, 1, 0);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown(KeyCode.Keypad1)) {
			displayRound(1);
			//showRound = true;
			
		}
		if(showMessage) {
			//Display round
			if (transform.localScale.x <= width) {
				transform.localScale += new Vector3(width * growthFactor, 0, height * growthFactor);
			}
			//Round displayed, count
			else {
				currentDisplayTime += Time.deltaTime;
			}
			
			if (currentDisplayTime >= displayTime) {
				showMessage = false;
				hideMessage = true;
			}
		}
		else if(hideMessage) {
			if (transform.localScale.x >= 0) {
				transform.localScale -= new Vector3(width * growthFactor, 0, height * growthFactor);
			}
			else {
				transform.localScale = new Vector3(0, 1, 0);
				hideMessage = false;
				//Determine if the fight screen has to be shown next
				if(showFight) {
					showFight = false;
					displayFight();
				}
			}
		}
		
	}
	
	/**
	 * Shows round
	 * @param round The round number to display. Should only range from 1-3 otherwise it will show round 1.
	 */
	public void displayRound(int round) {
		currentDisplayTime = 0;
		showFight = true;
		showMessage = true;
		switch (round) {
		case 1:
			textureOffset.x = 0;
			textureOffset.y = .75f;
			break;
		case 2:
			textureOffset.x = .5f;
			textureOffset.y = .75f;
			break;
		case 3:
			textureOffset.x = 0;
			textureOffset.y = .5f;
			break;
		default:
			textureOffset.x = 0;
			textureOffset.y = .75f;
			break;
		}
		renderer.material.SetTextureOffset("_MainTex", textureOffset);
	}
	
	public void displayFight() {
		currentDisplayTime = 0;
		showMessage = true;
		textureOffset.x = 0.5f;
		textureOffset.y = 0.5f;
		renderer.material.SetTextureOffset("_MainTex", textureOffset);
	}
	
	/**
	 * Display winner
	 * @param winner Show which player won. Takes values 1 or 2. Any other value will return draw.
	 */
	public void displayWinner(int winner) {
		currentDisplayTime = 0;
		showMessage = true;
		switch (winner) {
		case 1:
			textureOffset.x = 0;
			textureOffset.y = .25f;
			break;
		case 2:
			textureOffset.x = .5f;
			textureOffset.y = .25f;
			break;
		default:
			textureOffset.x = 0;
			textureOffset.y = 0;
			break;
		}
		renderer.material.SetTextureOffset("_MainTex", textureOffset);
	}
	
	public void displayTimeUp() {
		currentDisplayTime = 0;
		showMessage = true;
		textureOffset.x = 0.5f;
		textureOffset.y = 0;
		renderer.material.SetTextureOffset("_MainTex", textureOffset);
	}
}
