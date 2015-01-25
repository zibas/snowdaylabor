using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour
{

		public BuildJobManager.CATEGORIES category;
		public string description = "Cute, short name";
		public Renderer original;
		public Renderer target;
		public RenderTexture distorted;
		public Camera renderCamera;
		private int xChunkCount = 5;
		private int yChunkCount = 20;
		private int chunkWidth;
		private int chunkHeight;
		Texture2D newTex;
		private int currentChunk = 0;
	public TwirlEffect twirl;
	public Vignetting vignetting;


	void Awake ()
		{

					distorted = new RenderTexture(256, 256, 32, RenderTextureFormat.ARGB32);
					distorted.Create();
					renderCamera.targetTexture = distorted;

				newTex = new Texture2D (distorted.width, distorted.height, TextureFormat.ARGB32, false);
		
				for (int x = 0; x < newTex.width; x++) {
						for (int y = 0; y < newTex.height; y++) {
								newTex.SetPixel (x, y, Color.clear);
						}
				}
				newTex.Apply ();
				/*
				Texture2D tex = original.sprite.texture;
				//Get hit position (placeholder)
				//Vector2 pixelUV = hit.textureCoord;
				//pixelUV.x *= tex.width;
				//pixelUV.y *= tex.height;

				original.enabled = false;
				recreated.enabled = true;


				for (int x = 0; x < newTex.width; x++) {
						for (int y = 0; y < newTex.height; y++) {
								newTex.SetPixel (x, y, Color.clear);
						}
				}



				Debug.Log ("Chunk count: " + xChunkCount + ", " + yChunkCount);
				Debug.Log ("Chunk dimensions: " + chunkWidth + ", " + chunkHeight);

				for (int i = 0; i < 1; i++) {
						DoChunk (i);
				}

		DoChunk (45);
		DoChunk (70);
		newTex.Apply ();
				recreated.sprite = Sprite.Create (newTex, original.sprite.rect, new Vector2 (0.5f, 0.5f));
*/

				chunkWidth = distorted.width / xChunkCount;
				chunkHeight = distorted.height / yChunkCount;
				//DoChunk (1);
				//DoChunk (45);
				//DoChunk (70);

				

		}

		public void SetQuality (float quality)
		{
				//fish.strengthX = -(100f - quality) / 10f;
				//glow.glowIntensity = (100f - quality) / 10f;
				twirl.angle = (100f - quality);
				vignetting.chromaticAberration = (100f - quality);
		}

		public void PrepareToBuild ()
		{
				original.gameObject.SetActive (false);
				target.gameObject.SetActive (true);
				twirl.radius = new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f)); 
				twirl.center = new Vector2 (Random.Range (-1f, 1f), Random.Range (-1f, 1f)); 
			}
	
		void CopyToTarget (RenderTexture rt, int x, int y, int xMax, int yMax)
		{
				RenderTexture currentActiveRT = RenderTexture.active;
				RenderTexture.active = rt;
				//Debug.Log ("x,y,xmax,ymax: " + x + ", " + y + ", " + xMax + ", " + yMax);
				//Debug.Log ("rect: " + x + ", " + (newTex.height - y) + ", " + (xMax - x) + ", " + (yMax - y));

				//tex.ReadPixels(new Rect(0, 0, 200, 100), 0, 0);
				//tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
				newTex.ReadPixels (new Rect (x, newTex.height - y, xMax - x, yMax - y), x, y);
				newTex.Apply ();
				RenderTexture.active = currentActiveRT;
		}

		public void DoNextChunk (float percentComplete)
		{
				int totalChunks = xChunkCount * yChunkCount;
				float percentChunksComplete = (float)currentChunk / (float)totalChunks * 100f;

				while (percentComplete > percentChunksComplete) {
						currentChunk++;
						DoChunk (currentChunk);
						percentChunksComplete = (float)currentChunk / (float)totalChunks * 100f;
						//Debug.Log("Percents: "+percentComplete +", "+percentChunksComplete);
				}
		}

		private void DoChunk (int chunk)
		{

				//StartCoroutine (DoChunkRoutine (chunk));
				//Debug.Log ("Chunk: " + chunk + " yMin:" + ((chunk % yChunkCount) * chunkHeight) + " yMax:" + (((chunk + 1) % yChunkCount) * chunkHeight));
				int xStart = (chunk % xChunkCount) * chunkWidth;
				int yStart = Mathf.CeilToInt (chunk / xChunkCount) * chunkHeight;
				//Debug.Log ("Chunk: " + xStart+", "+yStart);

				CopyToTarget (distorted, xStart, yStart, xStart + chunkWidth, yStart + chunkHeight);
				//Debug.Log (newTex.width + ", " + newTex.height);
				target.material.mainTexture = newTex;
		}

		private IEnumerator DoChunkRoutine (int chunk)
		{

				yield return null;//new WaitForSeconds (0.1f);

				int xStart = (chunk % xChunkCount) * chunkWidth;
				int yStart = Mathf.CeilToInt (chunk / xChunkCount) * chunkHeight;
				CopyToTarget (distorted, xStart, yStart, xStart + chunkWidth, yStart + chunkHeight);
				Debug.Log (newTex.width + ", " + newTex.height);
				target.material.mainTexture = newTex;

				/*
				Color c = Color.clear;
				for (int x = (chunk%xChunkCount) * chunkWidth; x < ((chunk+1)%xChunkCount) * chunkWidth; x++) {
						Debug.Log (x);
						for (int y = (chunk%yChunkCount) * chunkHeight; y < ((chunk+1)%yChunkCount) * chunkHeight; y++) {
								//Debug.Log(y);
							//	yield return null;
								c = original.sprite.texture.GetPixel (x, y);
								c.r += Random.Range (-10, 10);
								newTex.SetPixel (x, y, c);
					
						}
			yield return null;

					newTex.Apply ();
					recreated.sprite = Sprite.Create (newTex, original.sprite.rect, new Vector2 (0.5f, 0.5f));
				}
				//newTex.SetPixel((int) pixelUV.x, (int) pixelUV.y, Color.black);
*/
		}


		// Update is called once per frame

		void Update ()
		{
	
		}


}
