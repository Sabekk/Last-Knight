using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SerializationStateButton : UIButton
{
	[SerializeField] TMP_Text state;
	[SerializeField] TMP_Text score;
	[SerializeField] TMP_Text levels;

	protected string stateName;
	public string StateName => stateName;

	public void SetState (string state, string score, string levels) {
		stateName = state;
		this.state.SetText (state);
		this.score.SetText (score);
		this.levels.SetText (levels);

		
	}
}
