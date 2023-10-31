using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "PlayerData", menuName = "Singleton/PlayerData")]
public class PlayerData : ScriptableSingleton<PlayerData> {
	[SerializeField] int score;
	[SerializeField] int unlockedLevel;
	public int Score => score;
	public int UnlockedLevel => unlockedLevel;
	public void SetScore (int scoreValue) {
		score = scoreValue;
	}
	public void SetUnlockedLevel (int level) {
		unlockedLevel = level;
	}
}
