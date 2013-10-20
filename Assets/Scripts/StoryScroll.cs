using UnityEngine;
using System.Collections;

public class StoryScroll : MonoBehaviour {
	public float speed = .01f;
	
	public string text = 
		"On a cold winter night, Hobo Jim" + "\n" +
		"and Hobo Bill were out finding " + "\n" +
		"sanctuary from the frigid winds. " + "\n" +
		"A discarded bottle of booze laid " + "\n" +
		"partially exposed by a nearby trash " + "\n" +
		"bin. To their amazement, a sizeable " + "\n" +
		"amount of the golden nectar still " + "\n" +
		"remained in the glass vessel. However," + "\n" +
		"their eyes met and they could tell " + "\n" +
		"from each other's gaze that the " + "\n" +
		"other one will not relinquish their" + "\n" +
		"claim on the prized treasure...";
	// Use this for initialization
	void Start () {
		GetComponent<TextMesh>().text = text;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, speed*Time.deltaTime, 0);
//		transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
	}
}
