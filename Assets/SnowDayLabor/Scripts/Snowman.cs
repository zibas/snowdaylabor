using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour
{

		public BuildJobManager.CATEGORIES category;
		public string description = "Cute, short name";
		public SpriteRenderer original;
		public SpriteRenderer recreated;
		private int xChunkCount = 10;
		private int yChunkCount = 10;
		private int chunkWidth;
		private int chunkHeight;
		Texture2D newTex;

		void Awake ()
		{

				Texture2D tex = original.sprite.texture;
				//Get hit position (placeholder)
				//Vector2 pixelUV = hit.textureCoord;
				//pixelUV.x *= tex.width;
				//pixelUV.y *= tex.height;


				newTex = new Texture2D (tex.width, tex.height, TextureFormat.ARGB32, false);

				for (int x = 0; x < newTex.width; x++) {
						for (int y = 0; y < newTex.height; y++) {
								newTex.SetPixel (x, y, Color.clear);
						}
				}

				chunkWidth = tex.width / xChunkCount;
				chunkHeight = tex.height / yChunkCount;

				for (int i = 0; i < 20; i++) {
						DoChunk (i);
				}
				newTex.Apply ();
				original.sprite = Sprite.Create (newTex, original.sprite.rect, new Vector2 (0.5f, 0.5f));

		}

		private void DoChunk (int chunk)
		{
				for (int x = (chunk%xChunkCount) * chunkWidth; x < ((chunk+1)%xChunkCount) * chunkWidth; x++) {
					for (int y = (chunk%yChunkCount) * chunkHeight; y < ((chunk+1)%yChunkCount) * chunkHeight; y++) {
						Color c = original.sprite.texture.GetPixel (x, y);
									c.r += Random.Range (-10, 10);
								newTex.SetPixel (x, y, c);
					}
				}
				//newTex.SetPixel((int) pixelUV.x, (int) pixelUV.y, Color.black);
				
		}


		// Update is called once per frame
		void Update ()
		{
	
		}


}
