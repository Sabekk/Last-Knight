using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISelectable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	UIView parent;
	public UIView Parent => parent;

	public virtual void Initialize () {
		parent = GetComponentInParent<UIView> (true);
		ToggleTransition (false);
	}

	public virtual void Select () {
		SoundManager.Instance.PlayEffectSound ("onButtonSelect");
		ToggleTransition (true);
	}

	public virtual void Deselect () {
		ToggleTransition (false);
	}

	public void OnPointerEnter (PointerEventData eventData) {
		Parent.Select (this);
	}

	public void OnPointerExit (PointerEventData eventData) {
		Parent.Deselect (this);
	}

	public virtual void ToggleTransition (bool state) {

	}
}
