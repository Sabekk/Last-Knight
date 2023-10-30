using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
	[SerializeField] UnityEvent onClick;
	[SerializeField] Transition transition;
	[SerializeField] Image mainImage;
	UIView parent;
	protected Button button;

	public virtual void Initialize (UIView view) {
		parent = view;
		button = GetComponent<Button> ();
		button.onClick.RemoveAllListeners ();
		button.onClick.AddListener (OnClick);
		ToggleTransition (false);
	}
	public virtual void OnClick () {
		onClick.Invoke ();
	}
	
	public virtual void ToggleTransition (bool state) {
		mainImage.sprite = state ? transition.active : transition.inactive;
	}

	public void OnPointerEnter (PointerEventData eventData) {
		parent.Select (this);
	}

	public void OnPointerExit (PointerEventData eventData) {
		parent.Deselect (this);
	}

	public virtual void Select () {
		ToggleTransition (true);
	}
	public virtual void Deselect () {
		ToggleTransition (false);
	}

	[System.Serializable]
	public struct Transition {
		public Sprite active;
		public Sprite inactive;
	}
}
