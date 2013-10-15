using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	static int gameRound = 1;
	public bool roundShown = false;
	public int time = 4;
	public bool gameOver = false;
	public bool gameStart = true;
	
	// Use this for initialization
	void Start () {
		InvokeRepeating("decrease", (float) 0.0, (float) 1.0);
		roundShown = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(gameOver)
		{
			StartCoroutine( "DelayStart" );
		}
	}
	
	void OnGUI() {
		//GUI.Box (new Rect(10, 10, Screen.width / 2 / (maxHealth/currentHealth), 20), currentHealth + "/" + maxHealth);	
		GUI.Box (new Rect(425, 20, Screen.width / 5, 50), time.ToString());
		
		if(gameOver) {
			GUI.Box (new Rect(425, Screen.height/3, Screen.width / 5, 50), "Game Over");	
		}
		
		if (roundShown)
		{
			//StartCoroutine( "DelayStart" );
			if(gameStart)
			{
				StartCoroutine( "RoundShown" );
				GUI.Box (new Rect(425, Screen.height/3, Screen.width / 5, 50), "Round " + gameRound);	
		
			}
			
			//roundShown = false;
		}
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
	
	IEnumerator DelayStart() {
		yield return new WaitForSeconds( 5 );
		gameRound++;
		Application.LoadLevel("blank");
		gameOver = false;
		roundShown = true;
	}
	
	IEnumerator RoundShown() {
		yield return new WaitForSeconds( 2 );
		roundShown = false;
	}
	



	
	
}