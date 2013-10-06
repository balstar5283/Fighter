using UnityEngine;
using System.Collections;

public class WalkTest : MonoBehaviour {
	public float xOffset = .375f;
	public float yOffset = .875f;
	public float offset = .125f;
	public int frameCount = 10;
	public int currentFrame = 0;
	public Material characterSheet;
	// Use this for initialization
	void Start () {
		characterSheet = renderer.material;
		characterSheet.SetTextureOffset("_MainTex", new Vector2(xOffset, yOffset));
	}
	
	// Update is called once per frame
	void Update () {
		//change frame
		if(currentFrame >= frameCount) {
			Vector2 currentOffset = characterSheet.GetTextureOffset("_MainTex");
			if(currentOffset.x < .875f) {
				currentOffset.x += offset;
			}
			else {
				currentOffset.x =xOffset;
			}
			characterSheet.SetTextureOffset("_MainTex", currentOffset);
			currentFrame = 0;
		}
		else {
			currentFrame++;
		}
	}
}
