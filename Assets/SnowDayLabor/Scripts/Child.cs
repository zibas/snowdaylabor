using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Child : MonoBehaviour {

	public BuildJob currentJob;
	
	private float doWorkClock = 0;
	private float doWorkPeriod = 0.2f;

	public int childNumber = 0;

	public Slider[] sliders;

	public Need[] needs; 

	// Use this for initialization
	void Start () {
	
	}

	public void Reset(){

		foreach (Need n in needs) {
			n.Reset ();
		}
	}
	

	// Update is called once per frame
	void Update () {

		doWorkClock += Time.deltaTime;
		if (doWorkClock >= doWorkPeriod) {
			DoWork();
		}

	}

	// Triggered by player hitting the button to the right of a slider for a need
	public void OnNeedPermitted(int index){
		needs[index].Reset ();
	}


	// Triggered by player hitting the take next job button
	public void OnTakeJob(){
		currentJob = GameManager.instance.buildJobManager.ConsumeJobOnDeck ();
	}


	// applies one unit of work to a job
	private void DoWork(){
		float amount = 1;
		float quality = 1;
		if (currentJob != null) {
						if (!currentJob.IsComplete ()) {
								currentJob.Advance (amount, quality);
						}
						if (currentJob.IsComplete ()) {
								currentJob.Sell ();
						}
				}
	}
}
