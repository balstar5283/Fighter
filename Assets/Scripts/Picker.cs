using UnityEngine;
using System.Collections;

public class Picker : MonoBehaviour {

	public int maxStep = 2;
	public int currentStep = 0;
	public GameObject controls;
	public GameObject title;
	Timer t;
	public bool showControl = false;
	public bool showMenu = false;
	
	void menu() {
		if (showControl)
			return;
		if (Input.GetKeyDown (KeyCode.DownArrow))
		{
			if(currentStep < maxStep)
			{
				gameObject.transform.Translate(0,-1.5f,0);
				currentStep++;
				Debug.Log(currentStep);
			}
		}	
		else if (Input.GetKeyDown (KeyCode.UpArrow))
		{
			if(currentStep > 0)
			{
				gameObject.transform.Translate(0,1.5f,0);
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
				t.reset ();
				Application.LoadLevel("FightScene");
		        break;
		    case 1:
				t.reset ();
				Application.LoadLevel ("Startup");
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
	GameObject goTimer = GameObject.Find("Timer");
	t = (Timer) goTimer.GetComponent(typeof(Timer));
		//var InitialPosition = transform.position;
		//offset = transform.position.y + transform.localScale.y;
	}
	
	// Update is called once per frame
	void Update () {
		//gameObject.transform.Translate( new Vector3( 0, speed * Input.GetAxis( "Vertical" ) * Time.deltaTime, 0 ), Space.World );
		//step += 0.01f;
		if(showMenu)
		{
			menu();
			checkButtonPress();
		}
	}
}
