using UnityEngine;
using System.Collections;

public class AnimationTest : MonoBehaviour {
	public string horizontalAxis = "Horizontal";
	public string verticalAxis = "Vertical";
	public float offset = .125f;
	public float offsetLimit;
	
	public Material characterSheet;
	// Use this for initialization
	void Start () {
		characterSheet = renderer.material;
		offsetLimit = 1 - offset;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown(horizontalAxis)){
			float xOffset = characterSheet.mainTextureOffset.x;
			float yOffset = characterSheet.mainTextureOffset.y;
			if(Input.GetAxis(horizontalAxis) > 0) {
				if(characterSheet.mainTextureOffset.x < offsetLimit) {
					xOffset += offset;
					characterSheet.SetTextureOffset("_MainTex", new Vector2(xOffset, yOffset));
				}
			}
			else {
				if(characterSheet.mainTextureOffset.x > 0) {
					xOffset -= offset;
					characterSheet.SetTextureOffset("_MainTex", new Vector2(xOffset, yOffset));
				}
			}
		}
		
		if(Input.GetButtonDown(verticalAxis)){
			float xOffset = characterSheet.mainTextureOffset.x;
			float yOffset = characterSheet.mainTextureOffset.y;
			if(Input.GetAxis(verticalAxis) > 0) {
				if(characterSheet.mainTextureOffset.y < offsetLimit) {
					yOffset += offset;
					characterSheet.SetTextureOffset("_MainTex", new Vector2(xOffset, yOffset));
				}
			}
			else {
				if(characterSheet.mainTextureOffset.y > 0) {
					yOffset -= offset;
					characterSheet.SetTextureOffset("_MainTex", new Vector2(xOffset, yOffset));
				}
			}
		}
		
	}
}
