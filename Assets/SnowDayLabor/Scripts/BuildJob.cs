﻿using UnityEngine;
using System.Collections;

public class BuildJob {

	public BuildJobManager.CATEGORIES category;
	public Snowman snowman;
	private float percentComplete = 0;
	private float quality = 100;

	public Texture2D image;
		
	public void Advance(float amount, float quality){
		if (GameManager.instance.state == GameManager.STATES.PLAYING) {
						percentComplete += amount;
						
						this.quality = (this.quality + quality) / 2;
					//	Debug.Log (this);
						snowman.SetQuality(quality);
						snowman.DoNextChunk(percentComplete);
				}
	}

	public bool IsComplete(){
		return percentComplete >= 100;
	}

	public void Sell(){
		if (GameManager.instance.state == GameManager.STATES.PLAYING && snowman != null) {
						GameManager.instance.ChangeScoreBy ((int)quality * 2);
						UnityEngine.Object.Destroy (snowman.gameObject);
				}
	}


	public int GetValue(){
		return (int)quality * 2;
	}

	public override string ToString ()
	{
		return string.Format ("[BuildJob] "+percentComplete+"% - quality: "+quality);
	}

}
