using UnityEngine;
using System.Collections;

public class BulletTravel : Hitbox {
	public int facing;
	public float speed = 10;
	
	//public Transform target;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (facing == 0) {
			transform.Translate(new Vector3(speed*Time.deltaTime, 0, 0));
		}
		else  {
			transform.Translate(new Vector3(-speed*Time.deltaTime, 0, 0));
		}
		
		if(gameObject.transform.position.x < -20 || gameObject.transform.position.x > 20) {
			Destroy (gameObject);
		}
	}
	
	public void setTarget(Transform t, int direction) {
		facing = direction;
		target = t;
		attack(-1);
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
			Destroy(gameObject);
		}
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
			Destroy(gameObject);
		}

	}
}
