using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager> {
	public enum GameState { play, pause, endgame }
	[SerializeField] ScoreItemsInitializer scoreItems;
	[SerializeField] Transform startingPosition;
	GameState currentState;
	int currentScore;
	bool readyForNextLevel = false;
	public int CurrentScore => currentScore;
	public GameState CurrentGameState => currentState;
	PlayerHealth player;
	const int maxPlayerHealth = 3;
	public bool ReadyForNextLevel => readyForNextLevel;

	protected override void Awake () {
		base.Awake ();
		Events.Gameplay.Level.OnGetPoint += OnGetPoints;
		Events.Gameplay.State.OnGameStateChanged += OnGameStateChanged;
		Events.Player.Input.OnPause += OnPause;
		Events.Gameplay.Level.OnLevelFinish += OnFinish;
		Events.Gameplay.Player.OnDeath += OnDeath;
	}

	private void OnDestroy () {
		Events.Gameplay.Level.OnGetPoint -= OnGetPoints;
		Events.Gameplay.State.OnGameStateChanged -= OnGameStateChanged;
		Events.Player.Input.OnPause -= OnPause;
		Events.Gameplay.Level.OnLevelFinish -= OnFinish;
		Events.Gameplay.Player.OnDeath -= OnDeath;
	}

	public void Initialize () {
		Time.timeScale = 1;
		CreatePlayer (maxPlayerHealth, startingPosition.position);
		scoreItems.Initialize ();
	}

	public void CreatePlayer (int health, Vector2 startPosition) {
		if (!player)
			player = ObjectPool.Instance.GetFromPool ("player").GetComponent<PlayerHealth> ();
		player.SetData (health, startPosition);
	}

	void OnGetPoints (int points) {
		currentScore += points;
		Events.UI.Stauts.OnRefreshPlayerScore.Invoke ();
	}
	void OnGameStateChanged (GameState state) {
		currentState = state;

		switch (currentState) {
			case GameState.play:
			Time.timeScale = 1;
			break;
			case GameState.pause:
			Time.timeScale = 0;
			break;
			case GameState.endgame:
			break;
			default:
			break;
		}
	}

	void OnPause () {
		switch (CurrentGameState) {
			case GameState.play:
			Events.Gameplay.State.OnGameStateChanged.Invoke (GameState.pause);
			Events.UI.View.OnCallView.Invoke ("pauseMenu");
			break;
			case GameState.pause:
			Events.Gameplay.State.OnGameStateChanged.Invoke (GameState.play);
			break;
			case GameState.endgame:
			break;
			default:
			break;
		}
	}

	void OnFinish () {
		int currentLevel = GetCurrentLevel ();

		if (CheckLevelExists (currentLevel + 1))
			readyForNextLevel = true;

		if (PlayerData.Instance.UnlockedLevel <= currentLevel && readyForNextLevel)
			PlayerData.Instance.AddUnlockedLevel ();

		Events.Gameplay.State.OnGameStateChanged.Invoke (GameState.endgame);
		Events.UI.View.OnCallView.Invoke ("finishView");
		PlayerData.Instance.SetScore (PlayerData.Instance.Score + currentScore);
		SoundManager.Instance.PlayEffectSound ("win");
	}
	void OnDeath () {
		Events.Gameplay.State.OnGameStateChanged.Invoke (GameState.endgame);
		Events.UI.View.OnCallView.Invoke ("gameOverView");
		SoundManager.Instance.PlayEffectSound ("gameOver");
	}
	int GetCurrentLevel () {
		string sceneName = GameplaySceneManager.CurrentSceneName;
		int index = sceneName.IndexOf ('_');
		if (index < 0)
			return 0;
		index++;
		return int.Parse (sceneName.Substring (index, sceneName.Length - index));
	}

	public bool CheckLevelExists (int level) {
		string nextLevelName = "Level_" + level.ToString ();
		return Application.CanStreamedLevelBeLoaded (nextLevelName);
	}
}
