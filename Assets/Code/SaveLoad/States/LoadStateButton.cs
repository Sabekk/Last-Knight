using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadStateButton : SerializationStateButton {
	public override void OnClick () {
		base.OnClick ();
		GameSerializer.Load (stateName);
	}
}
