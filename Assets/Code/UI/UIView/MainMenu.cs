using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UIView {
	public void StartNewGame () {
		GameplaySceneManager.LoadInitializedScene ("Tutorial", true);
	}
	public void OpenLevelsChooser () {
		Events.UI.View.OnCallView.Invoke ("levelsView");
	}
	public void OpenOptions () {
		Events.UI.View.OnCallView.Invoke ("optionsView");
	}
	public void OpenLoadView () {
		Events.UI.View.OnCallView.Invoke ("loadView");
	}

	public void QuitGame () {
		Application.Quit ();
	}
	public override void BackToPrevious () {

	}
}
