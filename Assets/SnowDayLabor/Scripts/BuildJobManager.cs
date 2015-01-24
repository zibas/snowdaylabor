using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildJobManager : MonoBehaviour {
	public enum CATEGORIES {MUSIC, SPORTS, SCIFI}
	public Texture2D[] images;

	public BuildJob jobOnDeck;

	public void ResetGame(){
		jobOnDeck = CreateJob ();
	}

	public BuildJob ConsumeJobOnDeck(){
		Debug.Log ("Consuming job on deck");
		BuildJob j = jobOnDeck; 
		jobOnDeck = CreateJob ();
		return j;
	}


	private BuildJob CreateJob(){
		BuildJob j = new BuildJob ();
		j.category = CATEGORIES.SCIFI;
		return j;
	}


}
