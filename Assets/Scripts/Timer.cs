using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	
	public int maxRound = 3;
	public static int currentRound = 1;
	
	private int time = 10;
	public bool roundOver = false;
	public bool gameOver = false;
	
	private GameObject roundAnimation;
	private RoundAnimation ra;
	
	private GameObject healthBar;
	private HealthBar hb;
	
	private static GameObject [] beerPoint;
	private static int player1Beer = 0;
	private static int player2Beer = 0;
    
	
	// Use this for initialization
	void Start () {
		roundAnimation = GameObject.Find ("RoundDisplay");
		ra = (RoundAnimation)roundAnimation.GetComponent(typeof(RoundAnimation));
		
		healthBar = GameObject.Find("HealthBar");
		hb = (HealthBar) healthBar.GetComponent(typeof(HealthBar));
		
		beerPoint = new GameObject [4];
		beerPoint[0] = GameObject.Find("Beer1");
		beerPoint[1] = GameObject.Find("Beer2");
		beerPoint[2] = GameObject.Find("Beer3");
		beerPoint[3] = GameObject.Find("Beer4");
		
		beerPoint[0].renderer.enabled = false;
		beerPoint[1].renderer.enabled = false;
		beerPoint[2].renderer.enabled = false;
		beerPoint[3].renderer.enabled = false;
		
		displayPlayerPoints();
	
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
			CancelInvoke("decrease");
			
		}
	
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(gameOver)
		{
			CancelInvoke("decrease");
			if (player1Beer == 2 && player2Beer == 2)
			{
				ra.setAnimation(0,0);
			}
			if (player1Beer == 2)
			{
				ra.setAnimation(0,1);
			}
			else if (player2Beer == 2)
			{
				ra.setAnimation(1,1);
			}
			
			
		}
		if(roundOver)
		{
			if(hb.player1Health == hb.player2Health)
			{
				StartCoroutine( "RoundShown");
				ra.setAnimation(0,0);
				ra.renderer.enabled = true;
				StartCoroutine( "RoundStart" );
			}
			else if (hb.player1Health > hb.player2Health)
			{
				StartCoroutine( "RoundShown",1 );
				ra.setAnimation(0,1);
				ra.renderer.enabled = true;
				StartCoroutine( "RoundStart" );
	
			}
			else if (hb.player2Health > hb.player1Health)
			{
				StartCoroutine( "RoundShown",2 );
				ra.setAnimation(1,1);
				ra.renderer.enabled = true;
				StartCoroutine( "RoundStart" );
	
			}

		}
	}
	
	void OnGUI() {
		GUI.Box (new Rect((Screen.width/2) - 75 , 20, 150, 50), time.ToString());
	}
	
	public void checkWhoWon()
	{


			if(hb.player1Health == hb.player2Health)
			{
				player1Beer++;
				player2Beer++;
			}
			else if (hb.player1Health > hb.player2Health)
			{
				player1Beer++;
	
			}
			else if (hb.player2Health > hb.player1Health)
			{
				player2Beer++;
			}

	}
	
	public void playerWon(int player)
	{
		
		CancelInvoke("decrease");
		StartCoroutine( "RoundShown" );
		
		if(player == 1)
		{
			//player1Beer++;
			displayPlayerPoints();
			ra.setAnimation(0,1);

		}
		if(player == 2)
		{
			//player2Beer++;
			displayPlayerPoints();
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
			roundOver = true;
		}
	}
	
	IEnumerator RoundShown() {
		
		yield return new WaitForSeconds( 3 );
		ra.renderer.enabled = false;
		
	}
	
	
	IEnumerator RoundStart() {
		
		yield return new WaitForSeconds( 2 );
		roundOver = false;
		currentRound++;
		Application.LoadLevel("blank");
		checkWhoWon();
		
	}
	
	void displayPlayerPoints()
	{
		if(player1Beer >= 1)
			beerPoint[0].renderer.enabled = true;
		if(player1Beer >= 2)
		{
			beerPoint[1].renderer.enabled = true;
			gameOver = true;
		}
		if(player2Beer >= 1)
			beerPoint[2].renderer.enabled = true;
		if(player2Beer >= 2)
		{
			beerPoint[3].renderer.enabled = true;
			gameOver = true;
		}
	}




}