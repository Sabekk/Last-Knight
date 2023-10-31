using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSaveData {
	public int playerLives;
	public int playerScore;
	public int playerLevelScore;
	public int unlockedLevels;
	public Vector2 playerPosition;
	public List<Items> items;
	public string currentSceneName;

	public PlayerSaveData () {
		playerLives = 3;
		playerScore = 0;
		playerLevelScore = 0;
		unlockedLevels = 0;
		playerPosition = Vector2.zero;
		items = new List<Items> ();
		currentSceneName = GameplaySceneManager.CurrentSceneName;
	}
}

[System.Serializable]
public struct Items {
	public string poolName;
	public Vector2 position;
}