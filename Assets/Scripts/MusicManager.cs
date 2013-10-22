using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {
	
	private static MusicManager instance = null;
	public static MusicManager Instance { get { return instance; }
	}
	
	public AudioClip song;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
		return;
		} 
		
		else {
			instance = this;
		}
		
		GameObject go = GameObject.Find("Game Music"); //Finds the game object called Game Music, if it goes by a different name, change this.
		go.audio.clip = song; //Replaces the old audio with the new one set in the inspector.
		go.audio.Play(); //Plays the audio.

		
		DontDestroyOnLoad(this.gameObject);	
	}
}
