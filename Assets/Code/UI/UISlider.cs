using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Slider))]
public class UISlider : UISelectable
{
	[SerializeField] GameObject selection;
	[SerializeField] float changingMultiple;
	Slider slider;
	public Slider Slider => slider;
	public override void Initialize () {
		base.Initialize ();
		slider = GetComponent<Slider> ();
	}
	public override void ToggleTransition (bool state) {
		selection.SetActive (state);
	}

	public void ChangeValue(float direction) {
		slider.value += direction * changingMultiple;
	}
}
