using UnityEngine;
using System.Collections;

public class TitleScroll : MonoBehaviour {
	
	public float speed = .1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.mainTextureOffset += new Vector2(speed, 0);
	}
}
