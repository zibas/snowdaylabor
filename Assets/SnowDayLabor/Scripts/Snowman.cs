using UnityEngine;
using System.Collections;

public class Snowman : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	public BuildJobManager.CATEGORIES category;
	public string description = "Cute, short name";

	void Awake(){
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
