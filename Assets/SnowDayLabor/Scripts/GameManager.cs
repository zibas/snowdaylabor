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

		void Awake ()
		{
				GameManager.instance = this;
		}

		void Update ()
		{

				switch (state) {
				case STATES.INITIALIZING:
						state = STATES.PRE_GAME;
						ui.SetPreGame();
						buildJobManager.ResetGame();
						foreach(Child c in children){
							c.Reset();
						}
						break;
				}

		}

		public void OnStartGame(){
			state = STATES.PLAYING;
			ui.SetPlaying ();
		}

	public void OnGameOver(){
		state = STATES.END_GAME;
		ui.SetGameOver ();

	}
	public void OnRestartGame(){
		state = STATES.INITIALIZING;
	}
}
