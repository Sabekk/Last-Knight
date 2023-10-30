using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager> {
	public enum GameState { play, pause, endgame }
	[SerializeField] ScoreItemsInitializer scoreItems;
	GameState currentState;
	int currentScore;
	public int CurrentScore => currentScore;
	public GameState CurrentGameState => currentState;

	protected override void Awake () {
		base.Awake ();
		Events.Gameplay.Level.OnGetPoint += OnGetPoints;
		Events.Gameplay.State.OnGameStateChanged += OnGameStateChanged;
		Events.Player.Input.OnPause += OnPause;
		Events.Gameplay.Level.OnLevelFinish += OnFinish;
		Events.Gameplay.Player.OnDeath += OnDeath;
	}
	private void Start () {
		InitializeLevel ();
		scoreItems.Initialize ();
	}
	private void OnDestroy () {
		Events.Gameplay.Level.OnGetPoint -= OnGetPoints;
		Events.Gameplay.State.OnGameStateChanged -= OnGameStateChanged;
		Events.Player.Input.OnPause -= OnPause;
		Events.Gameplay.Level.OnLevelFinish -= OnFinish;
		Events.Gameplay.Player.OnDeath -= OnDeath;
	}

	void InitializeLevel () {
		Time.timeScale = 1;
		ObjectPool.Instance.ReloadPool ();
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
		Events.Gameplay.State.OnGameStateChanged.Invoke (GameState.endgame);
		Events.UI.View.OnCallView.Invoke ("finishView");
	}
	void OnDeath () {
		Events.Gameplay.State.OnGameStateChanged.Invoke (GameState.endgame);
		Events.UI.View.OnCallView.Invoke ("gameOverView");
	}
}
