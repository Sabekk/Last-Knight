using UnityEngine;

public class PoolableEffect : MonoBehaviour, ObjectPool.IPoolable {
	public ObjectPool.PoolObject Poolable { get; set; }

	public void AssignPoolable (ObjectPool.PoolObject poolable) {
		Poolable = poolable;
	}

	void OnParticleSystemStopped () {
		OnEffectFinish ();
		ObjectPool.Instance.ReturnToPool (this);
	}
	protected virtual void OnEffectFinish () {

	}

}
