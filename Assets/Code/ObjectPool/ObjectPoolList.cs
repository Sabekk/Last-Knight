using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ObjectPoolList", menuName = "Singleton/ObjectPoolList")]
public class ObjectPoolList : ScriptableSingleton<ObjectPoolList>
{
	[SerializeField] List<PoolInstance> instances;
	public List<PoolInstance> Instances => instances;

	[System.Serializable]
	public struct PoolInstance {
		public string name;
		public GameObject prefab;
		public int size;
	}
}
