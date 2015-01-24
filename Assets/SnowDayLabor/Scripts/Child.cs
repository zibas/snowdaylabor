using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Child : MonoBehaviour {

	public BuildJob currentJob;
	
	private float doWorkClock = 0;
	private float doWorkPeriod = 0.2f;

	public int childNumber = 0;

	public Slider[] sliders;

	public Transform snowmanMount;

	// Use this for initialization
	void Start () {
		
	}

	public void Reset(){


		foreach (Slider s in sliders) {
			s.value = 0;
		}
	}
	

	// Update is called once per frame
	void Update () {

		doWorkClock += Time.deltaTime;
		if (doWorkClock >= doWorkPeriod) {
			DoWork();
		}

		foreach (Slider s in sliders) {
			float speed = 0.01f;
			s.value += Time.deltaTime * speed;
		}

	}

	// Triggered by player hitting the button to the right of a slider for a need
	public void OnNeedPermitted(int index){
		sliders [index].value = 0;
	}


	// Triggered by player hitting the take next job button
	public void OnTakeJob(){
		if (currentJob == null) {
						GameManager.instance.ChangeScoreBy (-100);
						currentJob = GameManager.instance.buildJobManager.ConsumeJobOnDeck ();
						currentJob.snowman.transform.position = snowmanMount.transform.position;
						currentJob.snowman.transform.parent = snowmanMount.transform;
				}
	}

	public void OnGameOver(){

		if (currentJob != null) {
			currentJob.Sell();
		}
	}


	// applies one unit of work to a job
	private void DoWork(){
		float amount = 1;
		float quality = 100;
		if (currentJob != null) {
						if (!currentJob.IsComplete ()) {
								currentJob.Advance (amount, quality);
						}
						if (currentJob.IsComplete ()) {
								currentJob.Sell ();
								currentJob = null;
						}
				}
	}
}
