using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
	
	public GUIStyle style;
	
	public int maxHealth = 100;
	public int player1Health = 100;
	public int player2Health = 100;
	public GameObject player1Display;
	public GameObject player2Display;
	public AudioClip hitSound;
	public AudioClip hitSound2;
	public AudioClip ouch;
	public AudioClip ow2;
	public int r = 0;
	
	private GameObject timer;
	private Timer t;
	
	// Use this for initialization
	void Start () {
		timer = GameObject.Find ("Timer");
		t = (Timer)timer.GetComponent(typeof(Timer));
		player1Health = maxHealth;
		player2Health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		player1Display.renderer.material.SetTextureOffset("_MainTex", new Vector2(.5f * ((maxHealth - player1Health)/(float)maxHealth), 0));
		player2Display.renderer.material.SetTextureOffset("_MainTex", new Vector2(.5f * ((maxHealth - player2Health)/(float)maxHealth), 0));
	}
	
	public void ApplyDamageToPlayer1(int damage) {
		r = Random.Range(0, 10);
		if(r <= 5) {
				GameObject.Find("Main Camera").GetComponent<AudioManager>().Play(hitSound, new Vector3(0, 0, 0));
			}
			else {
				GameObject.Find("Main Camera").GetComponent<AudioManager>().Play(ouch, new Vector3(0, 0, 0));
			}
		if (t.isRoundOver()) {
			return;
		}
		player1Health-= damage;
		if(player1Health <= 0)
		{
			player1Health = 0;
			t.endGame();
		}
	}
	
	public void ApplyDamageToPlayer2(int damage) {
		r = Random.Range(0, 10);
		if(r <= 5) {
				GameObject.Find("Main Camera").GetComponent<AudioManager>().Play(hitSound2, new Vector3(0, 0, 0));
			}
			else {
				GameObject.Find("Main Camera").GetComponent<AudioManager>().Play(ow2, new Vector3(0, 0, 0));
			}
		if (t.isRoundOver()) {
			return;
		}
		player2Health-= damage;
		
		if(player2Health <= 0)
		{
			player2Health = 0;
			t.endGame();
		}
	}
	
	
	
}
