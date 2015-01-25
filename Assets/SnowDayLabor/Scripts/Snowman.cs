using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour {

	public BuildJobManager.CATEGORIES category;
	public string description = "Cute, short name";

	public SpriteRenderer original;
	public SpriteRenderer recreated;

	void Awake(){

		Texture2D tex = original.sprite.texture;
		//Get hit position (placeholder)
		//Vector2 pixelUV = hit.textureCoord;
		//pixelUV.x *= tex.width;
		//pixelUV.y *= tex.height;


		Texture2D newTex = new Texture2D (tex.width, tex.height, TextureFormat.ARGB32, false);
		newTex.SetPixels32(tex.GetPixels32());
		for(int x = 0; x < 50; x++){
			for(int y = 0; y < 50; y++){

				newTex.SetPixel(x, y, Color.blue);
			}
		}
		//newTex.SetPixel((int) pixelUV.x, (int) pixelUV.y, Color.black);
		newTex.Apply();
		original.sprite = Sprite.Create(newTex, original.sprite.rect, new Vector2(0.5f, 0.5f));


	}



	// Update is called once per frame
	void Update () {
	
	}


}
