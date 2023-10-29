using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatusHUD : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    [SerializeField] GameObject[] lives;

	private void Awake () {
		Events.UI.Stauts.OnRefreshPlayerScore += OnRefresh;
		Events.Gameplay.Player.OnGetHit += OnRefreshLives;
	}
	private void OnDestroy () {
		Events.UI.Stauts.OnRefreshPlayerScore -= OnRefresh;
		Events.Gameplay.Player.OnGetHit -= OnRefreshLives;
	}
	void OnRefresh () {
		score.SetText (LevelManager.Instance.CurrentScore.ToString());
	}
	void OnRefreshLives(int current) {
		for (int i = 0; i < lives.Length; i++) {
			lives[i].SetActive (current > i);
		}
	}
}
