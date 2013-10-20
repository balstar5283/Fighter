using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	
	public int maxRound = 3;
	public static int currentRound = 1;
	
	public int time = 99 ;
	public bool roundOver = false;
	public bool gameOver = false;
	
	public TextMesh countDownDisplay;
	private GameObject roundAnimation;
	private RoundDisplay rd;
	
	private GameObject healthBar;
	private HealthBar hb;
	
	private static GameObject [] beerPoint;
	private static int player1Beer = 0;
	private static int player2Beer = 0;
    
	
	// Use this for initialization
	void Start () {
		roundAnimation = GameObject.Find ("RoundDisplay");
		rd = (RoundDisplay)roundAnimation.GetComponent(typeof(RoundDisplay));
		
		healthBar = GameObject.Find("HealthBar");
		hb = (HealthBar) healthBar.GetComponent(typeof(HealthBar));
		StartCoroutine( "delayStart" );	

	
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
			
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		countDownDisplay.text = time.ToString();
		if(gameOver)
		{
			GameObject player1 = GameObject.Find("Player 1");
			GameObject player2 = GameObject.Find("Player 2");
			player1.GetComponent<CharacterScript>().beginMovement = false;
			player2.GetComponent<CharacterScript>().beginMovement = false;
			if(hb.player1Health == hb.player2Health)
			{
				StartCoroutine( "showWinner", 0);
			}
			else if (hb.player1Health > hb.player2Health)
			{
				StartCoroutine( "showWinner", 1);
			}
			else if (hb.player2Health > hb.player1Health)
			{
				StartCoroutine( "showWinner", 2);
			}
			gameOver = false;
			roundOver = true;
		}
	}
	
	void OnGUI() {
		//GUI.Box (new Rect((Screen.width/2) - 75 , 20, 150, 50), time.ToString());
	}
	
	public void checkWhoWon()
	{
		Destroy(GameObject.Find ("Spawner"));
		GameObject player1 = GameObject.Find("Player 1");
		GameObject player2 = GameObject.Find("Player 2");
		if(hb.player1Health == hb.player2Health)
		{
			player1Beer++;
			player2Beer++;
			player1.GetComponentInChildren<AnimationController>().playDefeat();
			player2.GetComponentInChildren<AnimationController>().playDefeat();
		}
		else if (hb.player1Health > hb.player2Health)
		{
			player1Beer++;
			player1.GetComponentInChildren<AnimationController>().playWinner();
			player2.GetComponentInChildren<AnimationController>().playDefeat();
		}
		else if (hb.player2Health > hb.player1Health)
		{
			player2Beer++;
			player1.GetComponentInChildren<AnimationController>().playDefeat();
			player2.GetComponentInChildren<AnimationController>().playWinner();
		}

	}
	
	public void endGame()
	{
		gameOver = true;
	}
	
	public void startTimer() {
		StartCoroutine("decrease");
		GameObject.Find("Player 1").GetComponent<CharacterScript>().startMovement();
		GameObject.Find("Player 2").GetComponent<CharacterScript>().startMovement();
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
				rd.displayTimeUp();
				gameOver = true;
			}

		}
	}
	
	IEnumerator showWinner(int player) {
		StopCoroutine ("decrease");
		yield return new WaitForSeconds( 1f );
		rd.renderer.enabled = true;
		checkWhoWon();
		displayPlayerPoints();
		yield return new WaitForSeconds( 1f );
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
		yield return new WaitForSeconds (2);
		if (player1Beer >= 2 || player2Beer >= 2) {
			//TODO: Show menu
		}
		else {
			Application.LoadLevel("FightScene");
		}
	}
	
	IEnumerator delayStart() {
		yield return new WaitForSeconds(1.5f);
		rd.renderer.enabled = true;
		rd.displayRound(currentRound);
		
	}
	
	public void reset() {
		currentRound = 0;
		player1Beer = 0;
		player2Beer = 0;
	}
	
	public bool isRoundOver() {
		return roundOver;
	}
	
	void displayPlayerPoints()
	{
		if(player1Beer >= 1)
			beerPoint[0].renderer.enabled = true;
		if(player1Beer >= 2)
		{
			beerPoint[1].renderer.enabled = true;
			//gameOver = true;
		}
		if(player2Beer >= 1)
			beerPoint[2].renderer.enabled = true;
		if(player2Beer >= 2)
		{
			beerPoint[3].renderer.enabled = true;
			//gameOver = true;
		}
	}

}