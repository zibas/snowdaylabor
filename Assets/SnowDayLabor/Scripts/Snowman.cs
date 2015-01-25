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
		private int xChunkCount = 20;
		private int yChunkCount = 20;
		private int chunkWidth;
		private int chunkHeight;
		Texture2D newTex;
		private int currentChunk = 0;
		public TwirlEffect twirl;
		public Vignetting vignetting;
		private int[] columns;

		void Awake ()
		{

				distorted = new RenderTexture (512, 512, 32, RenderTextureFormat.ARGB32);
				distorted.Create ();
				renderCamera.targetTexture = distorted;

				newTex = new Texture2D (distorted.width, distorted.height, TextureFormat.ARGB32, false);
				for (int x = 0; x < newTex.width; x++) {
						for (int y = 0; y < newTex.height; y++) {
								newTex.SetPixel (x, y, Color.clear);
						}
				}
				newTex.Apply ();
				target.material.mainTexture = newTex;

		
				chunkWidth = distorted.width / xChunkCount;
				chunkHeight = distorted.height / yChunkCount;
				
				columns = new int[xChunkCount];
				for (int i = 0; i < xChunkCount; i++) {
						columns [i] = 0;
				}
				

		}

		public void SetQuality (float quality)
		{
				
				twirl.angle = (200f - quality * 2);
				vignetting.chromaticAberration = (100f - quality);
		}

		public void PrepareToBuild ()
		{

				original.gameObject.SetActive (false);
				target.gameObject.SetActive (true);
				twirl.radius = new Vector2 (Random.Range (0.2f, 1f), Random.Range (0.5f, 0.75f)); 
				twirl.center = new Vector2 (Random.Range (0f, 1f), Random.Range (0.3f, 0.9f)); 
		}
	
		void CopyToTarget (RenderTexture rt, int x, int y, int xMax, int yMax)
		{
				RenderTexture currentActiveRT = RenderTexture.active;
				RenderTexture.active = rt;
				
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

						int randColumn = 0;
						bool anyEmptySpaces = true;

						while (anyEmptySpaces) {
								randColumn = Random.Range (0, xChunkCount);
								if (columns [randColumn] < yChunkCount) {
									columns[randColumn]++;
									break;
								}
							anyEmptySpaces = false;
							for (int i = 0; i < xChunkCount; i++) {
								if(columns[i] < yChunkCount){
									anyEmptySpaces = true;
								}
							}
						}
						//Debug.Log("columns: "+columns[0]+", "+columns[1]+", "+columns[2]+", "+columns[3]);
			          
			DoChunk (currentChunk);
			//DoChunk (randColumn + xChunkCount * columns[randColumn]);
			percentChunksComplete = (float)currentChunk / (float)totalChunks * 100f;
						//Debug.Log("Percents: "+percentComplete +", "+percentChunksComplete);
				}
		}

		private void DoChunk (int chunk)
		{

				//Debug.Log ("Chunk: " + chunk + " yMin:" + ((chunk % yChunkCount) * chunkHeight) + " yMax:" + (((chunk + 1) % yChunkCount) * chunkHeight));
				int xStart = (chunk % xChunkCount) * chunkWidth;
				int yStart = Mathf.CeilToInt (chunk / xChunkCount) * chunkHeight;
				//Debug.Log ("Chunk: " + xStart+", "+yStart);

				CopyToTarget (distorted, xStart, yStart, xStart + chunkWidth, yStart + chunkHeight);
				//Debug.Log (newTex.width + ", " + newTex.height);
				target.material.mainTexture = newTex;

		}


}
