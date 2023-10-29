using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager> {
	public enum GameState { play, pause }
	[SerializeField] ScoreItemsInitializer scoreItems;
	GameState currentState;
	int currentScore;
	public int CurrentScore => currentScore;
	public GameState CurrentGameState => currentState;

	protected override void Awake () {
		base.Awake ();
		Events.Gameplay.Level.OnGetPoint += OnGetPoints;
		Events.Gameplay.State.OnGameStateChanged += OnGameStateChanged;
	}
	private void Start () {
		scoreItems.Initialize ();
	}
	private void OnDestroy () {
		Events.Gameplay.Level.OnGetPoint -= OnGetPoints;
		Events.Gameplay.State.OnGameStateChanged -= OnGameStateChanged;
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
			default:
			break;
		}
	}
}
