using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	
	public GUIStyle style;
	
	public int maxHealth = 100;
	public int player1Health = 100;
	public int player2Health = 100;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Z))
		{
			 gameObject.SendMessage("ApplyDamageToPlayer1", 5.0F);
		}
	
	}
	
	void OnGUI() {
		
		
		GUI.Box (new Rect(20, 20, Screen.width / 3 / (maxHealth/maxHealth), 50),"");
		GUI.Box (new Rect(Screen.width-20-Screen.width / 3 / (maxHealth/maxHealth), 20, Screen.width / 3 / (maxHealth/maxHealth), 50),"");
		
		GUI.Box (new Rect(20, 20, Screen.width / 3 / (maxHealth/ (float)player1Health), 50), player1Health + "/" + maxHealth);
		//GUI.Box (new Rect(Screen.width-20-Screen.width / 3 / (maxHealth/ (float)player2Health), 20, Screen.width / 3 / (maxHealth/maxHealth), 50),player2Health + "/" + maxHealth);
		//GUI.Box (new Rect(Screen.width-20-Screen.width / 3 / (maxHealth/ (float)player2Health) - (Screen.width / 3 / (maxHealth/maxHealth)), 20, Screen.width / 3 / (maxHealth/maxHealth), 50),player2Health + "/" + maxHealth);
		
		
		//GUI.Box ();

	}
	
	void ApplyDamageToPlayer1(int damage) {
		player1Health-= damage;
		Debug.Log ( Screen.width / 2 /(maxHealth/player1Health));
	}
	
	void ApplyDamageToPlayer2(int damage) {
		player2Health-= damage;
	}
	
	
	
}
