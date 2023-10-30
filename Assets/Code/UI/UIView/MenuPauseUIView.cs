using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPauseUIView : UIView {
	public override void Initialize () {
		base.Initialize ();
		Events.Player.Input.OnPause += OnPause;
	}
	private void OnDestroy () {
		Events.Player.Input.OnPause -= OnPause;
	}

	void OnPause () {
		switch (LevelManager.Instance.CurrentGameState) {
			case LevelManager.GameState.play:
			Activate ();
			Events.Gameplay.State.OnGameStateChanged.Invoke (LevelManager.GameState.pause);
			break;
			case LevelManager.GameState.pause:
			ResumeGame ();
			break;
			case LevelManager.GameState.endgame:
			break;
			default:
			break;
		}
	}
	public void OpenOptions () {
		Events.UI.View.OnCallView.Invoke ("optionsView");
	}
	public void OpenSaveView () {
		Events.UI.View.OnCallView.Invoke ("saveView");
	}
	public void OpenLoadView () {
		Events.UI.View.OnCallView.Invoke ("loadView");
	}
	public void ResumeGame () {
		Deactivate ();
		Events.Gameplay.State.OnGameStateChanged.Invoke (LevelManager.GameState.play);
	}
	public override void BackToPrevious () {
		ResumeGame ();
	}
}
