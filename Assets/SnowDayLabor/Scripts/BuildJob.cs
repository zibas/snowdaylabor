using UnityEngine;
using System.Collections;

public class BuildJob {

	public BuildJobManager.CATEGORIES category;

	private float percentComplete = 0;
	private float quality = 100;

	public Texture2D image;
		
	public void Advance(float amount, float quality){
		percentComplete += amount;
		quality = (this.quality + quality) / 2;
		Debug.Log (this);
	}

	public bool IsComplete(){
		return percentComplete >= 100;
	}

	public void Sell(){
		GameManager.instance.ChangeScoreBy((int)quality * 2);
	}

	public override string ToString ()
	{
		return string.Format ("[BuildJob] "+percentComplete+"% - quality: "+quality);
	}

}
