using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class TitleMusic : MonoBehaviour {
	
	public float delay = 2f;
	

	// Use this for initialization
	void Start () {
		audio.PlayDelayed(delay);
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
