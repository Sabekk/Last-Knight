using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UIView
{
	public void StartNewGame () {
		GameplaySceneManager.LoadScene ("Tutorial");
	}
	public void OpenLevelsChooser () {
		Events.UI.View.OnCallView.Invoke ("levelChooseView");
	}
	public void OpenOptions () {
		Events.UI.View.OnCallView.Invoke ("optionsView");
	}
	public void OpenLoadView () {
		Events.UI.View.OnCallView.Invoke ("loadView");
	}

	public void QuitGame () {
		
	}
	public override void BackToPrevious () {

	}
}
