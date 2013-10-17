using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {
	
	//public int row = 3;
	//public float speed = 1;
	// Use this for initialization
	public int maxStep = 2;
	public int currentStep = 0;
	
	void Start () {
		
		//var InitialPosition = transform.position;
		//offset = transform.position.y + transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
		//gameObject.transform.Translate( new Vector3( 0, speed * Input.GetAxis( "Vertical" ) * Time.deltaTime, 0 ), Space.World );
		//step += 0.01f;

		if (Input.GetKeyDown (KeyCode.DownArrow))
		{
				
			if(currentStep < maxStep)
			{
				gameObject.transform.Translate(0,-0.4f,0);
				currentStep++;
			}
		}	
		else if (Input.GetKeyDown (KeyCode.UpArrow))
		{
				
			if(currentStep > 0)
			{
				gameObject.transform.Translate(0,+0.4f,0);
				currentStep--;
			}
		}
		else if (Input.GetKeyDown (KeyCode.Return))
		{
				
			if (currentStep == 0)
			{
				Application.LoadLevel("blank");
			}
			else if( currentStep == 1)
			{
				
			}
			else if (currentStep == 2)
			{
				Application.Quit();
			}
		}
		
	}
}
