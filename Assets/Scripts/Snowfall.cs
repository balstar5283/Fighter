using UnityEngine;
using System.Collections;

public class Snowfall : MonoBehaviour {
	public float xSpeed = .1f, ySpeed =-.01f;
	public float min = -.15f, max = .15f;
	public float xDuration;
	public float currentDuration;
	// Use this for initialization
	void Start () {
		xSpeed = Random.Range(min, max);
		currentDuration = 0;
	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.mainTextureOffset += new Vector2(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime);
		currentDuration += Time.deltaTime;
		if (currentDuration >= xDuration) {
			currentDuration = 0;
			xDuration = Random.Range(2f, 5f);
			xSpeed = Random.Range(min, max);
		}
	}
}
