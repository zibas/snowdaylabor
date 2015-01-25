using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildJobManager : MonoBehaviour {
	public enum CATEGORIES {MUSIC, SPORTS, SCIFI}
	public GameObject [] snowmenPrefabs;

	public BuildJob jobOnDeck;

	private int jobCount = 0;



	public void ClearDeck(){
		Debug.Log ("Clearing deck" + jobOnDeck);
		if (jobOnDeck != null) {
			Destroy(jobOnDeck.snowman.gameObject);
			jobOnDeck = null;
			GameManager.instance.ui.nextJobDescription.text = "";

		}
	}

	public void StartGame(){
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

	
		Snowman s = ((GameObject) GameObject.Instantiate (snowmenPrefabs [Random.Range (0, snowmenPrefabs.Length)])).GetComponent<Snowman>();
		s.transform.localScale = Vector3.one;
		s.transform.position = GameManager.instance.nextJobPreviewMount.position;
		jobOnDeck.category = s.category;
		jobOnDeck.snowman = s;

		GameManager.instance.ui.nextJobDescription.text = "Next: "+ s.description;
	}
	

}
