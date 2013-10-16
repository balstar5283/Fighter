﻿using UnityEngine;
using System.Collections;

public class RoundAnimation : MonoBehaviour {
	public float growthFactor = .01f;
	public float width;
	public float height;
	public Vector2 textureOffset = new Vector2(0, 0);
	public bool showRound;
	// Use this for initialization
	void Start () {
		showRound = false;
		width = transform.localScale.x;
		height = transform.localScale.z;
		
		transform.localScale = new Vector3(0, 1, 0);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown(KeyCode.Keypad1)) {
			showRound = true;
			renderer.material.SetTextureOffset("_MainTex", textureOffset);
		}
		if(showRound){
			if (transform.localScale.x < width){
				transform.localScale += new Vector3(width * growthFactor, 0, height * growthFactor);
			}
			else {
				showRound = false;
			}
		}
		
	}
	
	public void setAnimation(int x, int y)
	{
		float offsetX = (float)( x * 0.5 );
		float offsetY = (float) (y * 0.25);
		showRound = true;
		renderer.material.SetTextureOffset("_MainTex", textureOffset);
		renderer.material.SetTextureOffset("_MainTex", new Vector2(offsetX, offsetY));
		transform.localScale += new Vector3(0 * growthFactor, 0, 0 * growthFactor);
		
	}
}
