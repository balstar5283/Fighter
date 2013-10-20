using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour {
	
	public int damage = 10;
	public bool isActive = false;
	public Transform target;
	
	public int attackFrameCount;
	public int currentAttackCount;
	
	// Use this for initialization
	void Start () {
	
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			if (attackFrameCount > 0) {
				currentAttackCount ++;
				if (attackFrameCount <= currentAttackCount) {
					disableAttack();
				}
			}
		}
	}
	
	public void attack(int attackCount) {
		attackFrameCount = attackCount;
		currentAttackCount = 0;
		isActive = true;
	}
	
	public void disableAttack() {
		isActive = false;
	}
	
	void OnTriggerEnter(Collider c) {
		if (c.gameObject.name == target.gameObject.name && isActive) {
			if (target.gameObject.name == "Player 1") {
				GameObject.Find("HealthBar").SendMessage("ApplyDamageToPlayer1", damage);
				print ("Hit Player 1");
				isActive = false;
			}
			else {
				GameObject.Find("HealthBar").SendMessage("ApplyDamageToPlayer2", damage);
				print ("Hit Player 2");
				isActive = false;
			}
		}
		/*
		if(gameObject.transform.parent.parent.parent.name == "Player 1" && c.gameObject.name == "Player 2" && isActive == true) {
			GameObject.Find("HealthBar").SendMessage("ApplyDamageToPlayer2", damage);
			print ("Hit Player 2");
			isActive = false;
		}
		if(gameObject.transform.parent.parent.parent.name == "Player 2" && c.gameObject.name == "Player 1" && isActive == true) {
			GameObject.Find("HealthBar").SendMessage("ApplyDamageToPlayer1", damage);
			print ("Hit Player 1");
			isActive = false;
		}*/
	}
	
	void OnTriggerStay(Collider c) {
		if (c.gameObject.name == target.gameObject.name && isActive) {
			if (target.gameObject.name == "Player 1") {
				GameObject.Find("HealthBar").SendMessage("ApplyDamageToPlayer1", damage);
				print ("Hit Player 1");
				isActive = false;
			}
			else {
				GameObject.Find("HealthBar").SendMessage("ApplyDamageToPlayer2", damage);
				print ("Hit Player 2");
				isActive = false;
			}
		}
		/*
		if(gameObject.transform.parent.parent.parent.name == "Player 1" && c.gameObject.name == "Player 2" && isActive == true) {
			GameObject.Find("HealthBar").SendMessage("ApplyDamageToPlayer2", damage);
			print ("Hit Player 2");
			isActive = false;
		}
		if(gameObject.transform.parent.parent.parent.name == "Player 2" && c.gameObject.name == "Player 1" && isActive == true) {
			GameObject.Find("HealthBar").SendMessage("ApplyDamageToPlayer1", damage);
			print ("Hit Player 1");
			isActive = false;
		}*/
	}
}
