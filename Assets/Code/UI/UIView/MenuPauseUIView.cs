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
		if (LevelManager.Instance.CurrentGameState == LevelManager.GameState.pause)
			ResumeGame ();
		else {
			Events.UI.View.OnCallView.Invoke ("pauseMenu");
			Events.Gameplay.State.OnGameStateChanged.Invoke (LevelManager.GameState.pause);
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
		Deactiavate ();
		Events.Gameplay.State.OnGameStateChanged.Invoke (LevelManager.GameState.play);
	}
	public override void BackToPrevious () {
		ResumeGame ();
	}
}
