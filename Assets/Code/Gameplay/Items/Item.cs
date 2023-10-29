using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, ICollectable, ObjectPool.IPoolable {
	Collider2D itemCollider;

	public ObjectPool.PoolObject Poolable { get; set; }

	public void Collect () {
		Debug.Log ("collect");
		OnCollect ();
	}

	public virtual void Initialize () {
		itemCollider = GetComponent<Collider2D> ();
		CollectableMatcher.Instance.AddToCollection (itemCollider, this);
	}

	public virtual void OnCollect () {

	}

	public void AssignPoolable (ObjectPool.PoolObject poolable) {
		Poolable = poolable;
	}
}
