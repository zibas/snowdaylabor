using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
	
	public bool muteSFX = false;
	public bool muteMusic = false;


	public AudioClip[] happyToTakeJobClips;
	public AudioClip[] neutralToTakeJobClips;
	public AudioClip[] sadToTakeJobClips;
	public AudioClip[] sellSnowmanClips;

	public AudioClip[] generallyHappyClips;
	public AudioClip[] generallyNegativeClips;
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

	public void PlayHappyToTakeJob ()
	{
		PlayRandomClip (happyToTakeJobClips);
	}

	public void PlaySadToTakeJob ()
	{
		PlayRandomClip (sadToTakeJobClips);
	}

	public void PlaySellSnowman ()
	{
		PlayRandomClip (sellSnowmanClips);
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

	public void PlayGenerallyHappy(){
		PlayRandomClip (generallyHappyClips);
	}

	public void PlayGenerallyNegative(){
		PlayRandomClip (generallyNegativeClips);
	}
	



	
	
}
