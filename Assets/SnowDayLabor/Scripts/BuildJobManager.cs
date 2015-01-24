using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildJobManager : MonoBehaviour {
	public enum CATEGORIES {MUSIC, SPORTS, SCIFI}
	public Texture2D[] images;

	public BuildJob jobOnDeck;

	private int jobCount = 0;

	public void ResetGame(){
		UpdateJobOnDeck ();
	}

	public BuildJob ConsumeJobOnDeck(){
		Debug.Log ("Consuming job on deck");
		BuildJob j = jobOnDeck; 
		UpdateJobOnDeck ();

		return j;
	}


	private void UpdateJobOnDeck(){
		jobCount++;
		jobOnDeck= new BuildJob ();
		jobOnDeck.category = CATEGORIES.SCIFI;
		GameManager.instance.ui.nextJobDescription.text = "Snow man: " + jobCount + " " + jobOnDeck.category;
	}


}
