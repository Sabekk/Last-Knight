using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableSingleton<T> {
	protected static T _instance;
	public static T Instance {
		get {
			if (_instance == null)
				SetInstane ();
			return _instance;
		}
	}

	static void SetInstane () {
		_instance = Resources.Load ("Singletons/" + typeof (T).ToString (), typeof (T)) as T;
	}
}