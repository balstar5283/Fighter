using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public AudioSource Play(AudioClip clip, Vector3 point)
    {
        return Play(clip, point, 1f, 1f);
    }
	
	public AudioSource Play(AudioClip clip, Vector3 point, float volume, float pitch)
    {
        //Create an empty game object
        GameObject go = new GameObject("Audio: " + clip.name);
        go.transform.position = point;
 
        //Create the source
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
		source.pan = 0.0f;
		source.panLevel = 0.0f;
        source.pitch = pitch;
        source.Play();
        Destroy(go, clip.length);
        return source;
    }
}
