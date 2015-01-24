using UnityEngine;
using System.Collections;

public class BuildJob : MonoBehaviour {

	public enum CATEGORY {MUSIC, SPORTS, SCIFI}
	public CATEGORY state = CATEGORY.MUSIC;


	private float percentComplete = 0;


		
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
