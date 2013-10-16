using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	
	public int maxRound = 3;
	public static int currentRound = 1;
	
	private int time = 99 ;
	public bool gameOver = false;
	
	private GameObject roundAnimation;
	private RoundDisplay rd;
	
	private GameObject healthBar;
	private HealthBar hb;
	
	
	// Use this for initialization
	void Start () {
		roundAnimation = GameObject.Find ("RoundDisplay");
		rd = (RoundDisplay)roundAnimation.GetComponent(typeof(RoundDisplay));
		
		healthBar = GameObject.Find("HealthBar");
		hb = (HealthBar) healthBar.GetComponent(typeof(HealthBar));

		StartCoroutine( "delayStart" );	
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(gameOver)
		{
			if(hb.player1Health == hb.player2Health)
			{
				StartCoroutine( "RoundShown", 0);
				
			}
			else if (hb.player1Health > hb.player2Health)
			{
				StartCoroutine( "RoundShown", 1);
				
			}
			else if (hb.player2Health > hb.player1Health)
			{
				StartCoroutine( "RoundShown", 2);
			}
		}
	}
	
	void OnGUI() {
		GUI.Box (new Rect((Screen.width/2) - 75 , 20, 150, 50), time.ToString());
	}
	
	public void playerWon(int player)
	{
		gameOver = true;
	}
	
	public void startTimer() {
		StartCoroutine("decrease");
	}
	
	IEnumerator decrease()
	{
		while (!gameOver) {
			yield return new WaitForSeconds(1.0f);
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
	
	IEnumerator RoundShown(int player) {
		StopCoroutine ("decrease");
		yield return new WaitForSeconds( 3 );
		rd.renderer.enabled = false;
		
		if (player == 0) {
			rd.displayDraw();
		}
		else if (player == 1 || player == 2) {
			rd.displayWinner(player);
		}
		
		gameOver = false;
		currentRound++;
		if (currentRound > maxRound) {
			currentRound = maxRound;
		}
	}
	
	IEnumerator delayStart() {
		yield return new WaitForSeconds(1.5f);
		rd.renderer.enabled = true;
		rd.displayRound(currentRound);
		
	}
	
	public void reset() {
		currentRound = 0;
	}

}