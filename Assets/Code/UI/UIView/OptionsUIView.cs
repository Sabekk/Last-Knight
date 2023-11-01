using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUIView : UIView {
	[SerializeField] UISlider musicVolume;
	[SerializeField] UISlider effectsVolume;

	private void Awake () {
		musicVolume.Slider.onValueChanged.RemoveAllListeners ();
		effectsVolume.Slider.onValueChanged.RemoveAllListeners ();

		musicVolume.Slider.onValueChanged.AddListener (ChangeMusicVolume);
		effectsVolume.Slider.onValueChanged.AddListener (ChangeEffectsVolume);
	}
	public override void Refresh () {
		base.Refresh ();
		musicVolume.Slider.value = AudioData.Instance.MusicVolume;
		effectsVolume.Slider.value = AudioData.Instance.EffectsVolume;
	}
	void ChangeMusicVolume (float volume) {
		SoundManager.Instance.SetMusicVolume (volume);
	}

	void ChangeEffectsVolume (float volume) {
		SoundManager.Instance.SetEffectVolume (volume);
	}
}
