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

	public void RestartLevel () {
		GameplaySceneManager.RestartCurrentScene ();
	}

	public void OpenLoadView () {
		Events.UI.View.OnCallView.Invoke ("loadView");
	}

	public void ReturnToMainMenu () {
		GameplaySceneManager.LoadMainMenu ();
	}

	public override void BackToPrevious () {

	}
}
