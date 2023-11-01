using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
public class UIButton : UISelectable {
	[SerializeField] UnityEvent onClick;
	[SerializeField] Transition transition;
	[SerializeField] protected Image mainImage;
	protected Button button;
	override public bool Interactable => button.interactable;
	public override void Initialize () {
		base.Initialize ();
		button = GetComponent<Button> ();
		button.onClick.RemoveAllListeners ();
		button.onClick.AddListener (OnClick);
	}
	public virtual void OnClick () {
		onClick.Invoke ();
		SoundManager.Instance.PlayEffectSound ("onButtonClick");
	}

	public override void ToggleTransition (bool state) {
		mainImage.sprite = state ? transition.active : transition.inactive;
	}

	[System.Serializable]
	public struct Transition {
		public Sprite active;
		public Sprite inactive;
	}
}
