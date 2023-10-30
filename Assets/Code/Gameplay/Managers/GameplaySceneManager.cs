using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameplaySceneManager
{
    public static void LoadScene (string name) {
		
	}
	public static void RestartCurrentScene () {
		ObjectPool.Instance.ClearAllPools ();
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
