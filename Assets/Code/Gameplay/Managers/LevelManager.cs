using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
	[SerializeField] ScoreItemsInitializer scoreItems;
    int currentScore;
	public int CurrentScore => currentScore;

	protected override void Awake () {
		base.Awake ();
		Events.Gameplay.Level.OnGetPoint += OnGetPoints;
	}
	private void Start () {
		scoreItems.Initialize ();
	}
	private void OnDestroy () {
		Events.Gameplay.Level.OnGetPoint -= OnGetPoints;
	}

	void OnGetPoints (int points) {
		currentScore += points;
		Events.UI.Stauts.OnRefreshPlayerScore.Invoke ();
	}
}
