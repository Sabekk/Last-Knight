using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadUIView : UIView {
	List<SerializationStateButton> states;
	public override void Initialize () {
		base.Initialize ();
		states = new List<SerializationStateButton> ();
		states.AddRange (GetComponentsInChildren<SerializationStateButton> (true));
	}

	public override void Refresh () {
		base.Refresh ();
		for (int i = 0; i < states.Count; i++) {
			string stateName = "State" + i.ToString ();
			var playerSavedData = JsonHelper.ReadFromJson<PlayerSaveData> (stateName);
			states[i].SetState (stateName, playerSavedData != null ? playerSavedData.playerScore.ToString () : "0", playerSavedData != null ? playerSavedData.unlockedLevels.ToString () : "0");
		}
	}
}
