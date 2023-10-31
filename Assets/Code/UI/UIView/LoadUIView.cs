using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadUIView : UIView {
	List<LoadStateButton> loadStates;

	public override void Initialize () {
		base.Initialize ();
		loadStates = new List<LoadStateButton> ();
		loadStates.AddRange (GetComponentsInChildren<LoadStateButton> (true));
	}

	public override void Refresh () {
		base.Refresh ();
		for (int i = 0; i < loadStates.Count; i++) {
			string stateName = "State" + i.ToString ();
			var playerSavedData = JsonHelper.ReadFromJson<PlayerSaveData> (stateName);
			loadStates[i].SetState (stateName, playerSavedData != null ? playerSavedData.playerScore.ToString () : "0", playerSavedData != null ? playerSavedData.unlockedLevels.ToString () : "0");
		}
	}
}
