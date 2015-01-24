using UnityEngine;
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
						buildJobManager.ResetGame ();
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
				secondsGameHasRun = 0;
				ui.SetPlaying ();
		}

		public void OnGameOver ()
		{
				state = STATES.END_GAME;
				ui.SetGameOver ();

		}

		public void OnRestartGame ()
		{
				state = STATES.INITIALIZING;
		}
	
		public void ChangeScoreBy (int amount)
		{
				score += amount;
				ui.scoreField.text = score.ToString ("C");
		}
}
