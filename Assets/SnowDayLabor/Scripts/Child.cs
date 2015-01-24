using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Child : MonoBehaviour {

	public BuildJob currentJob;
	
	private float doWorkClock = 0;
	private float doWorkPeriod = 0.2f;

	public int childNumber = 0;

	public Slider[] sliders;

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

	public void OnNeedPermitted(int index){
		sliders [index].value = 0;
	}

	// applies one unit of work to a job
	private void DoWork(){
		float amount = 1;
		float quality = 1;
		//currentJob.Advance (amount, quality);
	}
}
