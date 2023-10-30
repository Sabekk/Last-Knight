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

	public void NextLevel () {

	}

	public void ReturnToMainMenu () {
		GameplaySceneManager.LoadMainMenu ();
	}

	public override void BackToPrevious () {

	}
}
