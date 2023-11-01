using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUIView : UIView {
	[SerializeField] Slider musicVolume;
	[SerializeField] Slider effectsVolume;

	private void Awake () {
		musicVolume.onValueChanged.RemoveAllListeners ();
		effectsVolume.onValueChanged.RemoveAllListeners ();

		musicVolume.onValueChanged.AddListener (ChangeMusicVolume);
		effectsVolume.onValueChanged.AddListener (ChangeEffectsVolume);
	}
	public override void Refresh () {
		base.Refresh ();
		musicVolume.value = AudioData.Instance.MusicVolume;
		effectsVolume.value = AudioData.Instance.EffectsVolume;
	}
	void ChangeMusicVolume (float volume) {
		SoundManager.Instance.SetMusicVolume (volume);
	}

	void ChangeEffectsVolume (float volume) {
		SoundManager.Instance.SetEffectVolume (volume);
	}
}
