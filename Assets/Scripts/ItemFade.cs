using UnityEngine;
using System.Collections;

public class ItemFade : MonoBehaviour {
	
	public float timer = 0.0f;
	public float timeOut = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;
		
		if(timer >= timeOut) {	
			Destroy(gameObject);
		}
			
	}
}
