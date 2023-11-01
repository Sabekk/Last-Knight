using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishUIView : UIView {
	[SerializeField] TMP_Text score;
	[SerializeField] GameObject nextLevelButton;

	string nextLevel;
	public override void OnActivate () {
		base.OnActivate ();
		score.SetText (LevelManager.Instance.CurrentScore.ToString ());
		if (LevelManager.Instance.ReadyForNextLevel) {
			nextLevel = "Level_" + PlayerData.Instance.UnlockedLevel.ToString ();
			nextLevelButton.SetActive (true);
		} else
			nextLevelButton.SetActive (false);
	}

	public void NextLevel () {
		GameplaySceneManager.LoadInitializedScene (nextLevel, true);
	}

	public void ReturnToMainMenu () {
		GameplaySceneManager.LoadMainMenu ();
	}

	public override void BackToPrevious () {

	}
}
