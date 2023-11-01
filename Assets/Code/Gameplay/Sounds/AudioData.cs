using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "AudioData", menuName = "Singleton/AudioData")]
public class AudioData : ScriptableSingleton<AudioData>
{
	[SerializeField] float musicVolume;
	[SerializeField] float effectsVolume;
	public float MusicVolume => musicVolume;
	public float EffectsVolume => effectsVolume;
	public void SetMusicVolume (float volume) {
		musicVolume = volume;
	}
	public void SetEffectsVolume (float volume) {
		effectsVolume = volume;
	}
}
