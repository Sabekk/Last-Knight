using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class EffectAudioSource : MonoBehaviour, ObjectPool.IPoolable {
	AudioSource source;
	bool isPlaying;
	public bool IsPlaying => isPlaying;
	public ObjectPool.PoolObject Poolable { get; set; }
	private void Awake () {
		source = GetComponent<AudioSource> ();
	}
	private void Update () {
		if (!isPlaying)
			return;
		if (source.isPlaying)
			return;

		isPlaying = false;
		ObjectPool.Instance.ReturnToPool (this);
	}
	public void AssignPoolable (ObjectPool.PoolObject poolable) {
		Poolable = poolable;
	}
	public void PlayClip (AudioClip clip, bool loop = false) {
		source.clip = clip;
		source.loop = loop;
		source.Play ();
		isPlaying = true;
	}

	public void Stop () {
		source.Stop ();
	}
}
