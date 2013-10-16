using UnityEngine;
using System.Collections;

public class Hover : MonoBehaviour {
	
	public int maxStep = 3;
	public int currentStep = 0;
	public GameObject controls;
	public GameObject title;

	
	void menu() {
				if (Input.GetKeyDown (KeyCode.DownArrow))
			{
				if(currentStep < maxStep)
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
			if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return)) {
					switch (currentStep) {
				    case 0:
						Application.LoadLevel(Application.loadedLevel+1);
				        break;
				    case 1:
						title.transform.position = new Vector3(0f,0f,-300.0f);
						GameObject.Find("playerControls").transform.position = new Vector3(3.13250f,.896870f,-0.10f);
				        break;
					case 2:
						title.transform.position = new Vector3(-2.1165960f,3.4482140f,-6.1329750f);
						GameObject.Find("playerControls").transform.position = new Vector3(3.13250f,.896870f,-100.0f);
				        break;
					case 3:
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
