using UnityEngine;
using System.Collections;

public class SkipIntro : MonoBehaviour {
	public float duration = 31;
	public float currentTime = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime >= duration || Input.GetKeyDown(KeyCode.Return)) {
			Application.LoadLevel("Startup");
		}
	}
}
