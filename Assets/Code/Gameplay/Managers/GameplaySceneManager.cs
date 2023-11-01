using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameplaySceneManager {
	static TemporaryCoroutine loadCoroutine;
	public static TemporaryCoroutine Coroutine {
		get {
			if (loadCoroutine == null) {
				loadCoroutine = new GameObject ().AddComponent<TemporaryCoroutine> ();
				Object.DontDestroyOnLoad (loadCoroutine.gameObject);
			}
			return loadCoroutine;
		}
	}
	public static string CurrentSceneName => SceneManager.GetActiveScene ().name;
	public static void LoadMainMenu () {
		LoadInitializedScene ("MainMenu", false);
	}

	public static void LoadInitializedScene (string name, bool initializeLevel) {
		Coroutine.StartCoroutine (LoadScene (name, initializeLevel));
	}
	public static IEnumerator LoadScene (string name, bool initializeLevel) {
		ObjectPool.Instance.ClearAllPools ();
		var asyngLoadLevel = SceneManager.LoadSceneAsync (name, LoadSceneMode.Single);
		while (asyngLoadLevel.progress < 1)
			yield return null;

		ObjectPool.Instance.ReloadPool ();

		Scene newScene = SceneManager.GetSceneByName (name);
		SceneManager.SetActiveScene (newScene);
		Events.Scene.OnSceneLoaded.Invoke ();

		if (initializeLevel)
			LevelManager.Instance.Initialize ();
	}
	public static void LoadScene (string name) {
		ObjectPool.Instance.ClearAllPools ();
		SceneManager.LoadScene (name);
	}

	public static void RestartCurrentScene () {
		LoadInitializedScene (SceneManager.GetActiveScene ().name, true);
	}
}
