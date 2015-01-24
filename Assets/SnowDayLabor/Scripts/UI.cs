using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Button startButton;
	public Button restartButton;
	public GameObject childDetailsPanel;
	public Text scoreField;
	


	// Use this for initialization
	void Start () {

	}



	public void SetPreGame(){
		startButton.gameObject.SetActive (true);
		restartButton.gameObject.SetActive (false);
		childDetailsPanel.gameObject.SetActive (false);
		scoreField.gameObject.SetActive (false);
	}

	public void SetPlaying(){
		startButton.gameObject.SetActive (false);
		restartButton.gameObject.SetActive (false);
		childDetailsPanel.gameObject.SetActive (true);
		scoreField.gameObject.SetActive (true);
	}

	public void SetGameOver(){
		startButton.gameObject.SetActive (false);
		restartButton.gameObject.SetActive (true);
		childDetailsPanel.gameObject.SetActive (true);
		scoreField.gameObject.SetActive (true);
	}


	// Update is called once per frame
	void Update () {
	
	}
}
