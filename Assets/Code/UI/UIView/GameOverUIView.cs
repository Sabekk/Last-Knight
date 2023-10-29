using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUIView : UIView {
	[SerializeField] TMP_Text score;

	public override void OnActivate () {
		base.OnActivate ();
		score.SetText (LevelManager.Instance.CurrentScore.ToString ());
	}

	public override void Initialize () {
		base.Initialize ();
		Events.Gameplay.Player.OnDeath += OnDeath;
	}
	private void OnDestroy () {
		Events.Gameplay.Player.OnDeath -= OnDeath;
	}
	void OnDeath () {
		Events.Gameplay.State.OnGameStateChanged.Invoke (LevelManager.GameState.gameover);
		Activate ();
	}

	public void RestartLevel () {

	}

	public void OpenLoadView () {
		Events.UI.View.OnCallView.Invoke ("loadView");
	}

	public void ReturnToMainMenu () {

	}

	public override void BackToPrevious () {

	}
}
