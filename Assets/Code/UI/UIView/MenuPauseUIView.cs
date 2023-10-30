using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPauseUIView : UIView {
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
	}
	public override void OnDeactivate () {
		base.OnDeactivate ();
		Events.Gameplay.State.OnGameStateChanged.Invoke (LevelManager.GameState.play);
	}
	public override void BackToPrevious () {
		ResumeGame ();
	}
}
