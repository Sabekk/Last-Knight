using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
public class UIButton : MonoBehaviour {
	[SerializeField] UnityEvent onClick;
	[SerializeField] Transition transition;
	[SerializeField] Image mainImage;
	protected Button button;

	protected virtual void Awake () {
		button = GetComponent<Button> ();
		button.onClick.RemoveAllListeners ();
		button.onClick.AddListener (OnClick);
	}
	public virtual void Initialize () {
		ToggleTransition (false);
	}
	public virtual void OnClick () {
		onClick.Invoke ();
	}
	public virtual void ToggleTransition (bool state) {
		mainImage.sprite = state ? transition.active : transition.inactive;
	}
	[System.Serializable]
	public struct Transition {
		public Sprite active;
		public Sprite inactive;
	}
}
