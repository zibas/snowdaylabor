using UnityEngine;
using System.Collections;

public class BuildJob {

	public BuildJobManager.CATEGORIES category;

	private float percentComplete = 0;

	public Texture2D image;
		
	public void Advance(float amount, float quality){

	}

	public bool IsComplete(){
		return percentComplete >= 100;
	}

	public void Sell(){

	}

}
