﻿using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour
{
	


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnCollisionEnter(Collision c) {
		if(c.gameObject.name == "Player 1" || c.gameObject.name == "Player 2") {
			Destroy (gameObject);
		}
	}
}
