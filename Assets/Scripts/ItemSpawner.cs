using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
	
	public float timer = 0.0f;
	public float randTime = 0.0f;
	public int itemRand = 0;
	public GameObject gunPrefab;
	public GameObject batPrefab;

	// Use this for initialization
	void Start ()
	{
		randTime = Random.Range(5.0f, 10.0f);
		itemRand = Random.Range(0, 1);
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		
		if (timer >= randTime) {
			timer = 0.0f;
			randTime = Random.Range(10.0f, 15.0f);
			itemRand = Random.Range(1, 10);
			
			if(itemRand > 5) {
				GameObject gun = Instantiate (gunPrefab) as GameObject;
				gun.transform.position = new Vector3 (Random.Range (-14.0f, 14.0f), 12.0f, 0.0f);
			}
			
			else {
				GameObject bat = Instantiate (batPrefab) as GameObject;
				bat.transform.position = new Vector3 (Random.Range (-14.0f, 14.0f), 12.0f, 0.0f);
			}
		}
	}
}
