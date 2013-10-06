using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	public int time = 99;
	public bool gameOver = false;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("decrease", (float) 0.0, (float) 1.0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnGUI() {
		//GUI.Box (new Rect(10, 10, Screen.width / 2 / (maxHealth/currentHealth), 20), currentHealth + "/" + maxHealth);	
		GUI.Box (new Rect(Screen.width / 3, 10, Screen.width / 4, 20), time.ToString());
		
		if(gameOver)
			GUI.Box (new Rect(Screen.width / 3, 50, Screen.width / 4, 20), "Game Over");	
		
	}
	
	void decrease()
	{
		
		if(time > 0)
		{
			time--;
		}
		else
		{
			gameOver = true;
		}
	}
	
	
}