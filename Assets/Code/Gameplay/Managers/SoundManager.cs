using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager> {
	[SerializeField] EffectAudioSource backgroundSound;
	List<EffectAudioSource> effectsSources;

	const string EFFECT_SOURCE = "sound_effectSource";

	float effectsVolume;
	float musicVolume;


	protected override void Awake () {
		base.Awake ();
		effectsSources = new List<EffectAudioSource> ();
		Events.Scene.OnSceneLoaded += RefreshBackgroundSound;
	}
	private void Start () {
		RefreshBackgroundSound ();
		RefreshSoundsVolume ();
	}

	private void OnDestroy () {
		Events.Scene.OnSceneLoaded -= RefreshBackgroundSound;
	}

	private void Update () {
		for (int i = effectsSources.Count - 1; i >= 0; i--) {
			if (!effectsSources[i].IsPlaying)
				effectsSources.RemoveAt (i);
		}
	}
	void RefreshBackgroundSound () {
		PlayBackgroundSound (GameplaySceneManager.CurrentSceneName);
	}
	public void PlayEffectSound (string soundName) {
		AudioClip clip = SoundContainer.Instance.FindAudioClip (soundName);
		if (clip) {
			EffectAudioSource effectSource = ObjectPool.Instance.GetFromPool (EFFECT_SOURCE).GetComponent<EffectAudioSource> ();
			effectSource.Source.volume = effectsVolume;
			effectSource.PlayClip (clip);
			effectsSources.Add (effectSource);
		}
	}

	public void PlayBackgroundSound (string soundName) {
		AudioClip clip = SoundContainer.Instance.FindLevelSound (soundName);
		if (clip) {
			backgroundSound.PlayClip (clip, true);
		}
	}

	public void SetMusicVolume (float volume) {
		AudioData.Instance.SetMusicVolume (volume);
		RefreshSoundsVolume ();
	}

	public void SetEffectVolume (float volume) {
		AudioData.Instance.SetEffectsVolume (volume);
		RefreshSoundsVolume ();
	}

	void RefreshSoundsVolume () {
		effectsVolume = AudioData.Instance.EffectsVolume;
		musicVolume = AudioData.Instance.MusicVolume;
		foreach (var source in effectsSources) {
			source.Source.volume = effectsVolume;
		}
		backgroundSound.Source.volume = musicVolume;
	}
}
