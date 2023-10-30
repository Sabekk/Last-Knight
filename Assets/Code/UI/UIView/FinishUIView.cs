using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishUIView : UIView {
	[SerializeField] TMP_Text score;

	public override void OnActivate () {
		base.OnActivate ();
		score.SetText (LevelManager.Instance.CurrentScore.ToString ());
	}

	public override void Initialize () {
		base.Initialize ();
		Events.Gameplay.Level.OnLevelFinish += OnFinish;
	}
	private void OnDestroy () {
		Events.Gameplay.Level.OnLevelFinish -= OnFinish;
	}
	void OnFinish () {
		Events.Gameplay.State.OnGameStateChanged.Invoke (LevelManager.GameState.endgame);
		Activate ();
	}

	public void NextLevel () {

	}

	public void ReturnToMainMenu () {

	}

	public override void BackToPrevious () {

	}
}
