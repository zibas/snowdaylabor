using UnityEngine;
using System.Collections;

public class Child : MonoBehaviour {

	public BuildJob currentJob;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// applies one unit of work to a job
	private void DoWork(){
		float amount = 1;
		float quality = 1;

		//currentJob.Advance (amount, quality);

	}
}
