using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameplaySceneManager
{
	public static void LoadMainMenu () {
		LoadScene ("MainMenu");
	}
    public static void LoadScene (string name) {
		ObjectPool.Instance.ClearAllPools ();
		SceneManager.LoadScene (name);
	}
	public static void RestartCurrentScene () {
		ObjectPool.Instance.ClearAllPools ();
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
