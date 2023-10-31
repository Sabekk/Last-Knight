using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveUIView : UIView
{
	List<SaveStateButton> saveStates;

	public override void Initialize () {
		base.Initialize ();
		saveStates = new List<SaveStateButton> ();
		saveStates.AddRange (GetComponentsInChildren<SaveStateButton> (true));
	}

	public override void Refresh () {
		base.Refresh ();
		for (int i = 0; i < saveStates.Count; i++) {
			string stateName = "State" + i.ToString ();
			var playerSavedData = JsonHelper.ReadFromJson<PlayerSaveData> (stateName);
			saveStates[i].SetState (stateName, playerSavedData.playerScore.ToString (), playerSavedData.unlockedLevels.ToString ());
		}
	}
}
