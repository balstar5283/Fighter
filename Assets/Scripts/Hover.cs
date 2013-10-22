using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {
	
	public int maxStep = 2;
	public int currentStep = 0;
	public GameObject controls;
	public GameObject title;
	
	public bool showControl = false;
	
	void menu() {
		if (showControl)
			return;
		if (Input.GetKeyDown (KeyCode.DownArrow))
		{
			if(currentStep <= maxStep)
			{
				gameObject.transform.Translate(0,-0.25f,0);
				currentStep++;
			Debug.Log(currentStep);
			}
		}	
		else if (Input.GetKeyDown (KeyCode.UpArrow))
		{
			if(currentStep > 0)
			{
				gameObject.transform.Translate(0,+0.25f,0);
				currentStep--;
				Debug.Log(currentStep);

			}
		}
	}
	
	void checkButtonPress() { 
		if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) {
			if (showControl) {
				GameObject.Find("playerControls").transform.position += new Vector3(0,0,-10.0f);
				showControl = false;
				return;
			}
			switch (currentStep) {
		    case 0:
				Application.LoadLevel("FightScene");
		        break;
		    case 1:
				showControl = true;
				GameObject.Find("playerControls").transform.position -= new Vector3(0,0,-10.0f);
		        break;
			case 2:
				Application.Quit();
		        break;
		    default:
		        Debug.Log("Do Nothing");
		        break;
			}
		}
	}
							
			
	void Start () {
	//controls = GameObject.Find("controls");
	title = GameObject.Find("title");
		//var InitialPosition = transform.position;
		//offset = transform.position.y + transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
		//gameObject.transform.Translate( new Vector3( 0, speed * Input.GetAxis( "Vertical" ) * Time.deltaTime, 0 ), Space.World );
		//step += 0.01f;
		menu();
		checkButtonPress();
		
	}
}
