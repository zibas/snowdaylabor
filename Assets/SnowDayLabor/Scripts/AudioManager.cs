using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
	
	public bool muteSFX = false;
	public bool muteMusic = false;


	private List<AudioClip[]> happyToTakeJobClips = new List<AudioClip[]>();
	private List<AudioClip[]>  sadToTakeJobClips = new List<AudioClip[]>();
	private List<AudioClip[]>  finishedSnowmanClips = new List<AudioClip[]>();
	private List<AudioClip[]>  generallyNegativeClips = new List<AudioClip[]>();

	//Kid 1
	public AudioClip[] happyToTakeJobClips1;
	public AudioClip[] sadToTakeJobClips1;
	public AudioClip[] finishedSnowmanClips1;
	public AudioClip[] generallyNegativeClips1;

	//Kid 2
	public AudioClip[] happyToTakeJobClips2;
	public AudioClip[] sadToTakeJobClips2;
	public AudioClip[] finishedSnowmanClips2;
	public AudioClip[] generallyNegativeClips2;

	//Kid 3

	public AudioClip[] happyToTakeJobClips3;
	public AudioClip[] sadToTakeJobClips3;
	public AudioClip[] finishedSnowmanClips3;
	public AudioClip[] generallyNegativeClips3;


	public AudioClip[] sellSnowmanClips;
	 public AudioClip[] generallyHappyClips;
	public AudioClip[] startGameClips;
	public AudioClip[] endGameClips;

	public AudioSource[] audioSourcePool;
	
	public AudioSource backgroundMusicSource;

	
	int audioPoolMax = 20;
	
	public void SetMuteSFX (bool newBool)
	{
		muteSFX = newBool;
		PlayerPrefs.SetInt ("muteSFX", muteSFX ? 1 : 0);

	}
	
	public void SetMuteMusic (bool newBool)
	{
		muteMusic = newBool;

	}
	
	public void Awake ()
	{

		happyToTakeJobClips.Add(happyToTakeJobClips1);
		happyToTakeJobClips.Add(happyToTakeJobClips2);
		happyToTakeJobClips.Add(happyToTakeJobClips3);
	
		sadToTakeJobClips.Add(sadToTakeJobClips1);
		sadToTakeJobClips.Add(sadToTakeJobClips2);
		sadToTakeJobClips.Add(sadToTakeJobClips3);
		finishedSnowmanClips.Add(finishedSnowmanClips1);
		finishedSnowmanClips.Add(finishedSnowmanClips2);
		finishedSnowmanClips.Add(finishedSnowmanClips3);
		generallyNegativeClips.Add(generallyNegativeClips1);
		generallyNegativeClips.Add(generallyNegativeClips2);
		generallyNegativeClips.Add(generallyNegativeClips3);

		audioSourcePool = new AudioSource[audioPoolMax];
		for (int i = 0; i < audioPoolMax; i++) {
			audioSourcePool [i] = ((GameObject)GameObject.Instantiate(Resources.Load("Prefabs/AudioSource"))).GetComponent<AudioSource>();
			audioSourcePool [i].transform.parent = gameObject.transform;
		}
	}


	
	private AudioSource GetNextAudioSource ()
	{
		for (int i = 0; i < audioPoolMax; i++) {
			if (!audioSourcePool [i].isPlaying) {
				audioSourcePool [i].volume = 1;
				audioSourcePool [i].pitch = 1;
				audioSourcePool [i].loop = false;

				return audioSourcePool [i];
			}
		}
		Debug.LogWarning ("Ran out of available audio sources");
		audioSourcePool [0].Stop ();
		audioSourcePool [0].volume = 1;
		audioSourcePool [0].pitch = 1;
		audioSourcePool [0].loop = false;
		return audioSourcePool [0];
	}

	private void PlayRandomClip(AudioClip[] clips){
		if (!muteSFX) {
			AudioSource a = GetNextAudioSource ();
			int r = Random.Range (0, clips.Length);
			a.clip = clips [r];
			a.volume = 1f;
			a.Play ();
		}
	}

	public void PlayHappyToTakeJob (int kid)
	{

		PlayRandomClip (happyToTakeJobClips[kid]);
	}

	public void PlaySadToTakeJob (int kid)
	{
		PlayRandomClip (sadToTakeJobClips[kid]);
	}

	public void PlaySellSnowman (int kid)
	{
		PlayRandomClip (sellSnowmanClips);
		PlayRandomClip (finishedSnowmanClips[kid]);
	}

	public void PlayStartGame(){
		PlayRandomClip (startGameClips);
	}

	public void PlayEndGame(){
		PlayRandomClip (endGameClips);
	}

	public void StartBackgroundMusic(){
		backgroundMusicSource.Play ();
	}

	public void StopBackgroundMusic(){
		backgroundMusicSource.Stop ();
	}

	public void PlayGenerallyHappy(int kid){
		PlayRandomClip (generallyHappyClips);
	}

	public void PlayGenerallyNegative(int kid){
		PlayRandomClip (generallyNegativeClips[kid]);
	}
	



	
	
}
