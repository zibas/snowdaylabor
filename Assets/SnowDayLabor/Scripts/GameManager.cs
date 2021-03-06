﻿using UnityEngine;
using System.Collections;

// This is a Singleton for top level actions and information
public class GameManager : MonoBehaviour
{

		public enum STATES
		{
				INITIALIZING,
				PRE_GAME,
				PLAYING,
				END_GAME
		}
		public STATES state = STATES.INITIALIZING;
		public static GameManager instance;
		public Child[] children;
		public UI ui;
		public BuildJobManager buildJobManager;
		public int score = 0;
		public float secondsPerGame = 20;
		public float secondsGameHasRun = 0;
		public Transform nextJobPreviewMount;
		public AudioManager audio;

		void Awake ()
		{
				GameManager.instance = this;
		}

		void Update ()
		{

				switch (state) {
				case STATES.INITIALIZING:
						state = STATES.PRE_GAME;
						ui.SetPreGame ();
						buildJobManager.ClearDeck ();
						ui.timeSlider.minValue = 0;
						ui.timeSlider.maxValue = secondsPerGame;
						foreach (Child c in children) {
								c.Reset ();
						}
						break;
				case STATES.PLAYING:
						secondsGameHasRun += Time.deltaTime;
						ui.timeSlider.value = secondsPerGame - secondsGameHasRun;
						if (secondsGameHasRun >= secondsPerGame) {
								OnGameOver ();
						}
						break;
				}


		}

		public void OnStartGame ()
		{
				state = STATES.PLAYING;
				ui.scoreField.text = "$0.00";

				Debug.Log ("Switching to " + state);
				foreach (Child c in children) {
						c.OnGameStart ();
				}
				secondsGameHasRun = 0;
				ui.SetPlaying ();
				buildJobManager.StartGame ();
				audio.PlayStartGame ();
				audio.StartBackgroundMusic ();
		}

		public void OnGameOver ()
		{
				foreach (Child c in children) {
					c.OnGameOver ();
				}
				state = STATES.END_GAME;
				Debug.Log ("Switching to " + state);

				
				ui.SetGameOver ();
				audio.PlayEndGame ();
				audio.StopBackgroundMusic ();

		}

		public void OnRestartGame ()
		{
				state = STATES.INITIALIZING;
				Debug.Log ("Switching to " + state);

		}
	
		public void ChangeScoreBy (int amount)
		{
				score += amount;
				ui.scoreField.text = score.ToString ("C");
		}
}
