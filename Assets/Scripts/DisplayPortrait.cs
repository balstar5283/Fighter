using UnityEngine;
using System.Collections;

public class DisplayPortrait : MonoBehaviour {
	public Material p1Portrait;
	public Material p2Portrait;
	public Material drawPortrait;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void displayPortrait(int player) {
		switch (player) {
		case 1:
			renderer.material = p1Portrait;
			break;
		case 2:
			renderer.material = p2Portrait;
			break;
		default:
			renderer.material = drawPortrait;
			break;
		}
	}
}
