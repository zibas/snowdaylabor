using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Child : MonoBehaviour
{

		public BuildJob currentJob;
		private float doWorkClock = 0;
		private float doWorkPeriod = 0.2f;
		private BuildJobManager.CATEGORIES preference;
		public int childNumber = 0;
		public Slider[] sliders;
		public Transform snowmanMount;
		public Need[] needs;
		public AnimationCurve energy;
		public AnimationCurve motivation;
		public Animator animator;

		// Use this for initialization
		void Start ()
		{
	
				System.Array values = System.Enum.GetValues (typeof(BuildJobManager.CATEGORIES));
				preference = (BuildJobManager.CATEGORIES)values.GetValue (Random.Range (0, values.Length));
		}

		public void Reset ()
		{

				animator.SetBool ("Run", false);
				animator.gameObject.SetActive (false);

				foreach (Need n in needs) {
						n.Reset ();
				}
		}

		public void OnGameStart ()
		{
				animator.SetBool ("Run", false);
				animator.gameObject.SetActive (true);
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
						GameManager.instance.ChangeScoreBy (-100);
						currentJob = GameManager.instance.buildJobManager.ConsumeJobOnDeck ();
						currentJob.snowman.PrepareToBuild();
						currentJob.snowman.transform.position = snowmanMount.transform.position;
						currentJob.snowman.transform.parent = snowmanMount.transform;
						currentJob.snowman.transform.localScale = new Vector3(6, 6, 6);
						animator.SetBool ("Run", true);

			if(currentJob.category == preference)
			{
				GameManager.instance.audio.PlayHappyToTakeJob();
			}
			else
			{
				GameManager.instance.audio.PlaySadToTakeJob();
			}

				}
		}

		public void OnGameOver ()
		{

				if (currentJob != null) {
						currentJob.Sell ();
				}
		}


		// applies one unit of work to a job
		private void DoWork ()
		{
				float amount = 0.1f;
				float quality = 100.0f;

				if (currentJob != null) {
						if (!currentJob.IsComplete ()) {
								
								amount *= motivation.Evaluate (needs [0].mySlider.value);
								amount *= energy.Evaluate (needs [1].mySlider.value);
								quality *= motivation.Evaluate (needs [2].mySlider.value);

								if (currentJob.category == preference) {
									amount *= 5f;
									quality += 50f;
									quality = Mathf.Min(quality, 100); // Don't let quality get over 100
								}
									
								currentJob.Advance (amount, quality);
						}
						if (currentJob.IsComplete ()) {
								animator.SetBool ("Run", false);
								currentJob.Sell ();
								currentJob = null;
						}
				}
		}
}
