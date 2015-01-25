using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Child : MonoBehaviour
{

		public BuildJob currentJob;
		private float doWorkClock = 0;
		private float doWorkPeriod = 0.2f;
		private float onBreakClock = 0;
		private float onBreakPeriod = 5.0f;
		private BuildJobManager.CATEGORIES preference;
		public int childNumber = 0;
		public Slider[] sliders;
		public Transform snowmanMount;
		public Need[] needs;
		public AnimationCurve energy;
		public AnimationCurve motivation;
		public Animator animator;
		public Button takeJobButton;
		public Text saleAmount;
		private bool isDoingSellRoutine = false;
		private bool hasGrumbledRecently = false;
		private bool onBreak = false;


		// Use this for initialization
		void Start ()
		{
	
				System.Array values = System.Enum.GetValues (typeof(BuildJobManager.CATEGORIES));
				preference = (BuildJobManager.CATEGORIES)values.GetValue (Random.Range (0, values.Length));

		}

		public void Reset ()
		{

				animator.SetTrigger ("Idle");
				animator.gameObject.SetActive (false);
				saleAmount.gameObject.SetActive (false);
				foreach (Need n in needs) {
						n.Reset ();
				}
		}

		public void OnGameStart ()
		{
				animator.SetTrigger ("Idle");
				animator.gameObject.SetActive (true);
				takeJobButton.gameObject.SetActive (true);
				//this is an override that's necessary so it can re-set
				needs [0].decay = 0.05f;
				needs [1].decay = 0.01f;
				needs [2].decay = 0.02f;

		}

		// Update is called once per frame
		void Update ()
		{

			doWorkClock += Time.deltaTime;

			if (onBreak == true)
			{
				onBreakClock += Time.deltaTime;
				if (onBreakClock >= onBreakPeriod)
				{
					onBreak = false;
					onBreakClock = 0;
					animator.gameObject.SetActive (true);
				}
			}

			//should I grumble from low motivation?
			if (needs [0].mySlider.value == 0.0f) 
			{
				if(hasGrumbledRecently == false)
				{
					GameManager.instance.audio.PlayGenerallyNegative (childNumber);
					hasGrumbledRecently = true;
				}
			}
			else
			{
				hasGrumbledRecently = false;
			}

			//am I doing the peepee dance because my bladder is at zero?
			if (needs [2].mySlider.value == 0.0f)
			{
				animator.SetTrigger ("Dance");
			}
			else 
			{
				//am I resting because my Energy is at zero?
				if (needs [1].mySlider.value == 0.0f)
				{
					animator.SetTrigger ("Rest");
				}
				else
				{
					if (currentJob == null)
					{
						animator.SetTrigger ("Idle");
					}
					else
					{
						//I'm working! Am I happy with this snowman or not?
						if (currentJob.category == preference)
						{
							animator.SetTrigger ("BuildHappy");
						} 
						else 
						{
							animator.SetTrigger ("BuildNeutral");
						}
				
						if (doWorkClock >= doWorkPeriod && onBreak == false)
						{
							DoWork ();
						}
					}
				}
			}
		}

		// Triggered by player hitting the button to the right of a slider for a need
		public void OnNeedPermitted (int index)
		{
			if (onBreak == false)
			{
				needs [index].Reset ();
				onBreak = true;
				animator.gameObject.SetActive (false);
			}
		}


		// Triggered by player hitting the take next job button
		public void OnTakeJob ()
		{
				if (currentJob == null) {
						takeJobButton.gameObject.SetActive (false);
						GameManager.instance.ChangeScoreBy (-100);
						currentJob = GameManager.instance.buildJobManager.ConsumeJobOnDeck ();
						currentJob.snowman.PrepareToBuild ();
						currentJob.snowman.transform.position = snowmanMount.transform.position;
						currentJob.snowman.transform.parent = snowmanMount.transform;
						currentJob.snowman.transform.localScale = new Vector3 (6, 6, 6);

						if (currentJob.category == preference) {
								GameManager.instance.audio.PlayHappyToTakeJob (childNumber);
								animator.SetTrigger ("BuildHappy");
								// if they like the job, their motivation doesn't decay as fast
								needs [0].decay -= 0.02f;
								// but make sure their decay never hits or goes below zero
								if (needs[0].decay <= 0.0f) { needs [0].decay =0.01f;}
						} else {
								GameManager.instance.audio.PlaySadToTakeJob (childNumber);
								animator.SetTrigger ("BuildNeutral");
								// their motivation decays faster for each snowman they make that they arent interested in
								needs [0].decay += 0.02f;
								// but make sure their decay never passes .2
								if (needs[0].decay > 0.2f) { needs [0].decay =0.9f;}
						}

				}
		}

		public void OnGameOver ()
		{
		Debug.Log ("child game over: " + currentJob);
				if (currentJob != null) {
						Destroy (currentJob.snowman.gameObject);
						currentJob = null;
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
										quality = Mathf.Min (quality, 100); // Don't let quality get over 100
								}
									
								currentJob.Advance (amount, quality);
						} else if (currentJob.IsComplete () && !isDoingSellRoutine) {
								isDoingSellRoutine = true;

								StartCoroutine (SellRoutine ());
						}
				}
		}

		IEnumerator SellRoutine ()
		{
				GameManager.instance.audio.PlaySellSnowman (childNumber);	
				animator.SetTrigger ("Idle");
				saleAmount.gameObject.SetActive (true);
				saleAmount.text = currentJob.GetValue ().ToString ("C0");
				yield return new WaitForSeconds (3);
				currentJob.Sell ();

				currentJob = null;
				takeJobButton.gameObject.SetActive (true);
				isDoingSellRoutine = false;
				saleAmount.gameObject.SetActive (false);

		
		
		}
}
