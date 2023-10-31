using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ObjectPoolList", menuName = "Singleton/ObjectPoolList")]
public class ObjectPoolList : ScriptableSingleton<ObjectPoolList>
{
	[SerializeField] List<PoolInstance> instances;
	public List<PoolInstance> Instances => instances;

	public PoolInstance GetPoolInstanceByName(string name) {
		foreach (var instance in instances) {
			if (instance.name == name)
				return instance;
		}
		return null;
	}

	[System.Serializable]
	public class PoolInstance {
		public string name;
		public GameObject prefab;
		public int size;
	}
}
