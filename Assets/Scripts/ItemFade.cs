using UnityEngine;
using System.Collections;

public class ItemFade : MonoBehaviour {
	
	public float timer = 0.0f;
	public float flickerTime = 0.25f;
	public float timer2 = 0.0f;
	public float timeOut = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		timer += Time.deltaTime;
		
		if(timer >= timeOut - 3.0f) {
			timer2 += Time.deltaTime;
			
			if(timer2 >= flickerTime) {
				timer2 = 0;
				gameObject.renderer.enabled = !gameObject.renderer.enabled;
			}
		}
		
		
		if(timer >= timeOut) {	
			Destroy(gameObject);
		}
			
	}
}
