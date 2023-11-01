using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSerializer : MonoSingleton<GameSerializer> {
	public static void Save (string fileName) {
		PlayerSaveData saveData = new PlayerSaveData ();
		List<ISerializable> serializableObjects = SerializableMatcher.Instance.GetAllCollectionElements ();
		foreach (var serializable in serializableObjects) {
			serializable.SaveGame (ref saveData);
		}
		saveData.playerScore = PlayerData.Instance.Score;
		saveData.playerLevelScore = LevelManager.Instance ? LevelManager.Instance.CurrentScore : 0;
		saveData.isMenuSave = LevelManager.Instance == null;
		JsonHelper.SaveToJson<PlayerSaveData> (saveData, fileName);
	}

	public static void Load (string fileName) {
		var playerSavedData = JsonHelper.ReadFromJson<PlayerSaveData> (fileName);
		GameplaySceneManager.Coroutine.StartCoroutine (LoadSaveState (playerSavedData));
	}
	static IEnumerator LoadSaveState (PlayerSaveData playerSavedData) {
		ObjectPool.Instance.ClearAllPools ();
		var asyngLoadLevel = SceneManager.LoadSceneAsync (playerSavedData.currentSceneName, LoadSceneMode.Single);
		while (asyngLoadLevel.progress < 1)
			yield return null;

		ObjectPool.Instance.ReloadPool ();

		Scene newScene = SceneManager.GetSceneByName (playerSavedData.currentSceneName);
		SceneManager.SetActiveScene (newScene);
		Events.Scene.OnSceneLoaded.Invoke ();

		if (!playerSavedData.isMenuSave) {
			LevelManager.Instance.CreatePlayer (playerSavedData.playerLives, playerSavedData.playerPosition);
			foreach (var item in playerSavedData.items) {
				var savedItem = ObjectPool.Instance.GetFromPool (item.poolName);
				savedItem.prefab.transform.position = item.position;
				savedItem.prefab.transform.localScale = Vector3.one * 3;
			}

			List<ISerializable> serializableObjects = SerializableMatcher.Instance.GetAllCollectionElements ();
			foreach (var serializable in serializableObjects)
				serializable.LoadGame (playerSavedData);

			PlayerData.Instance.SetScore (playerSavedData.playerScore);
			Events.Gameplay.Level.OnGetPoint.Invoke (playerSavedData.playerLevelScore);

			Events.Gameplay.State.OnGameStateChanged.Invoke (LevelManager.GameState.play);
			yield return null;
		}
	}
}
