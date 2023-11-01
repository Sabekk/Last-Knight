using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SoundContainer", menuName = "Singleton/SoundContainer")]
public class SoundContainer : ScriptableSingleton<SoundContainer>
{
	[SerializeField] List<SoundEffects> sounds;
	[SerializeField] List<BackgroundSound> levelSounds;

	public AudioClip FindAudioClip (string name) {
		foreach (var sound in sounds) {
			if (sound.name == name)
				return sound.clip;
		}
		return null;
	}

	public AudioClip FindLevelSound (string name) {
		foreach (var sound in levelSounds) {
			if (sound.levelName == name)
				return sound.clip;
		}
		return null;
	}

	[System.Serializable]
	struct SoundEffects {
		public string name;
		public AudioClip clip;
	}

	[System.Serializable]
	struct BackgroundSound {
		public string levelName;
		public AudioClip clip;
	}
}
