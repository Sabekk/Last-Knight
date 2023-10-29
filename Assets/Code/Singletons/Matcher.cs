using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Matcher<T1, T2, T3> : Singleton<T3> where T1 : class where T2 : class where T3 : Matcher<T1, T2, T3>, new() {

	Dictionary<T1, T2> collection;
	public Matcher () {
		collection = new Dictionary<T1, T2> ();
	}
	~Matcher () {
		collection.Clear ();
	}

	public void AddToCollection (T1 element, T2 type) {
		collection[element] = type;
	}

	public bool CheckCollection (T1 element, out T2 type) {
		return collection.TryGetValue (element, out type);
	}
}
