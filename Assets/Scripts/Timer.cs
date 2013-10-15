using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	
	public int maxRound = 3;
	public static int currentRound = 1;
	
	private int time = 15;
	public bool gameOver = false;
	
	private GameObject roundAnimation;
	private RoundAnimation ra;
	
	private GameObject healthBar;
	private HealthBar hb;
	
	
	// Use this for initialization
	void Start () {
		roundAnimation = GameObject.Find ("RoundDisplay");
		ra = (RoundAnimation)roundAnimation.GetComponent(typeof(RoundAnimation));
		
		healthBar = GameObject.Find("HealthBar");
		hb = (HealthBar) healthBar.GetComponent(typeof(HealthBar));
		
	
		InvokeRepeating("decrease", (float) 0.0, (float) 1.0);

		
		if(currentRound == 1)
		{
			StartCoroutine( "RoundShown" );
			ra.setAnimation(0,3);
			ra.renderer.enabled = true;
		}
		else if (currentRound == 2)
		{
			StartCoroutine( "RoundShown" );
			ra.setAnimation(1,3);
			ra.renderer.enabled = true;
		}
		else if (currentRound == 3)
		{
			StartCoroutine( "RoundShown" );
			ra.setAnimation(0,2);
			ra.renderer.enabled = true;
		}
		else 
		{
			
			
		}
	
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(gameOver)
		{
			if(hb.player1Health == hb.player2Health)
			{
				StartCoroutine( "RoundShown" );
				ra.setAnimation(0,0);
				ra.renderer.enabled = true;
				StartCoroutine( "RoundStart" );
			}
			else if (hb.player1Health > hb.player2Health)
			{
				StartCoroutine( "RoundShown" );
				ra.setAnimation(0,1);
				ra.renderer.enabled = true;
				StartCoroutine( "RoundStart" );
	
			}
			else if (hb.player2Health > hb.player1Health)
			{
				StartCoroutine( "RoundShown" );
				ra.setAnimation(1,1);
				ra.renderer.enabled = true;
				StartCoroutine( "RoundStart" );
	
			}
		}
	}
	
	void OnGUI() {
		GUI.Box (new Rect((Screen.width/2) - 75 , 20, 150, 50), time.ToString());
	}
	
	public void playerWon(int player)
	{
		
		CancelInvoke("decrease");
		StartCoroutine( "RoundShown" );
		
		if(player == 1)
		{
			ra.setAnimation(0,1);
		}
		if(player == 2)
		{
			ra.setAnimation(1,1);

		}
		
		ra.renderer.enabled = true;
		StartCoroutine( "RoundStart" );

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
	
	IEnumerator RoundShown() {
		
		yield return new WaitForSeconds( 3 );
		ra.renderer.enabled = false;
	}
	
	
	IEnumerator RoundStart() {
		
		yield return new WaitForSeconds( 2 );
		gameOver = false;
		currentRound++;
		Application.LoadLevel("blank");
		
	}
	



}