using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Child : MonoBehaviour
{

		public BuildJob currentJob;
		private float doWorkClock = 0;
		private float doWorkPeriod = 0.2f;
		public int childNumber = 0;
		public Slider[] sliders;
		public Transform snowmanMount;
		public Need[] needs;

		// Use this for initialization
		void Start ()
		{
	
		}

		public void Reset ()
		{

				foreach (Need n in needs) {
						n.Reset ();
				}
		}
	

		// Update is called once per frame
		void Update ()
		{

				doWorkClock += Time.deltaTime;
				if (doWorkClock >= doWorkPeriod) {
						DoWork ();
				}

		}

		// Triggered by player hitting the button to the right of a slider for a need
		public void OnNeedPermitted (int index)
		{
				needs [index].Reset ();
		}


		// Triggered by player hitting the take next job button
		public void OnTakeJob ()
		{
				if (currentJob == null) {
						// TODO: see if they're happy or not
						GameManager.instance.audio.PlayHappyToTakeJob ();
						GameManager.instance.ChangeScoreBy (-100);
						currentJob = GameManager.instance.buildJobManager.ConsumeJobOnDeck ();
						currentJob.snowman.transform.position = snowmanMount.transform.position;
						currentJob.snowman.transform.parent = snowmanMount.transform;	
				}
		}

		public void OnGameOver ()
		{

				if (currentJob != null) {
						currentJob.Sell ();
				}
		}


		/* Audio calls to make:

			GameManager.instance.audio.PlayGenerallyHappy();
			GameManager.instance.audio.PlayGenerallyNegative();
		*/

		// applies one unit of work to a job
		private void DoWork ()
		{
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
