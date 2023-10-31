using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, ICollectable, ObjectPool.IPoolable, ISerializable {
	string poolableName;
	Collider2D itemCollider;
	public ObjectPool.PoolObject Poolable { get; set; }
	protected bool collected;
	public bool Collected => collected;
	public void Collect () {
		OnCollect ();
	}
	private void Awake () {
		itemCollider = GetComponent<Collider2D> ();
	}
	public virtual void Initialize () {
		collected = false;
	}

	private void OnEnable () {
		CollectableMatcher.Instance.AddToCollection (itemCollider, this);
		SerializableMatcher.Instance.AddToCollection (gameObject, this);
	}

	private void OnDisable () {
		CollectableMatcher.Instance.RemoveFromCollection (itemCollider);
		SerializableMatcher.Instance.RemoveFromCollection (gameObject);
	}

	public virtual void OnCollect () {
		collected = true;
	}

	public void AssignPoolable (ObjectPool.PoolObject poolable) {
		Poolable = poolable;
		poolableName = Poolable.name;
	}

	public void LoadGame (PlayerSaveData saveData) {
	}

	public void SaveGame (ref PlayerSaveData saveData) {
		saveData.items.Add (new Items {
			position = transform.position,
			poolName = Poolable.name,
		});
	}
}
