using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	
	public GUIStyle style;
	
	public int maxHealth = 100;
	public int player1Health = 100;
	public int player2Health = 100;
	
	private GameObject timer;
	private Timer t;
	
	// Use this for initialization
	void Start () {
		timer = GameObject.Find ("Timer");
		t = (Timer)timer.GetComponent(typeof(Timer));
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Z))
		{
			 gameObject.SendMessage("ApplyDamageToPlayer1", 5.0F);
		}
		if (Input.GetKeyDown (KeyCode.X))
		{
			 gameObject.SendMessage("ApplyDamageToPlayer2", 5.0F);
		}
	
	}
	
	void OnGUI() {
		
		
		GUI.Box (new Rect(20, 20, Screen.width / 3 / (maxHealth/maxHealth), 50),player1Health + "/" + maxHealth);
		
		if(player1Health > 0)
		{
			GUI.Box (new Rect(20, 20, Screen.width / 3 / (maxHealth/ (float)player1Health), 50), "" );
		}
			
		GUI.Box (new Rect(Screen.width-20-Screen.width / 3 / (maxHealth/maxHealth), 20, Screen.width / 3 / (maxHealth/maxHealth), 50),player2Health + "/" + maxHealth);
		
		if(player2Health > 0)
		{
			float offset = Screen.width / 3 / (maxHealth/maxHealth) - Screen.width / 3 / (maxHealth/ (float)player2Health);
			GUI.Box (new Rect( (Screen.width-20-Screen.width / 3 / (maxHealth/maxHealth))+offset, 20, (Screen.width / 3 / (maxHealth/ (float)player2Health)), 50), "");
		}
		
	}
	
	public void ApplyDamageToPlayer1(int damage) {
		
		player1Health-= damage;
		if(player1Health <= 0)
		{
			player1Health = 0;
			t.playerWon(2);
		}
	}
	
	public void ApplyDamageToPlayer2(int damage) {
		
		player2Health-= damage;
		
		if(player2Health <= 0)
		{
			player2Health = 0;
			t.playerWon(1);
		}
	}
	
	
	
}
