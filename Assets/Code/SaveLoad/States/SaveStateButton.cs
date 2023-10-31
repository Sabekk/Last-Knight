using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveStateButton : SerializationStateButton {

	public override void OnClick () {
		base.OnClick ();
		GameSerializer.Save (stateName);
		Parent.Refresh ();
	}
}
