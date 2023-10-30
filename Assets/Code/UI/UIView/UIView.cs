using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIView : MonoBehaviour {
	[SerializeField] string viewName;
	public static UIView current;
	UIView previous;
	UIButton currentButton;
	List<UIButton> buttons;
	public string Name => viewName;

	public virtual void Initialize () {
		buttons = new List<UIButton> ();
		buttons.AddRange (GetComponentsInChildren<UIButton> (true));
		foreach (var button in buttons) {
			button.Initialize ();
		}

	}

	public virtual void Activate () {
		OnActivate ();
		gameObject.SetActive (true);
		if (previous != this)
			previous = current;
		if (current)
			current.Deactivate ();
		current = this;

		Refresh ();
	}
	public virtual void Deactivate () {
		OnDeactivate ();
		gameObject.SetActive (false);
		if (current == this)
			current = null;
		if (currentButton)
			Deselect (currentButton);
	}
	public virtual void OnActivate () {
		Events.Player.Controller.OnChangeControllerState += Refresh;
	}
	public virtual void OnDeactivate () {
		Events.Player.Controller.OnChangeControllerState -= Refresh;
	}
	public virtual void Refresh () {
		if (PlayerUIController.Instance.GamepadUsing) {
			if (buttons.Count > 0)
				Select (buttons[0]);
		} else {
			if (currentButton)
				Deselect (currentButton);
		}
	}
	public virtual void BackToPrevious () {
		if (previous)
			previous.Activate ();
		else
			Deactivate ();
	}
	public virtual void Select (UIButton button) {
		if (currentButton == button)
			return;
		if (currentButton) {
			currentButton.Deselect ();
		}
		currentButton = button;
		if (currentButton)
			currentButton.Select ();
	}
	public virtual void Deselect (UIButton button) {
		if (currentButton && currentButton == button)
			currentButton.Deselect ();
		currentButton = null;
	}
	public virtual void OnNavigate (Vector2 direction) {
		if (direction.y > 0)
			CycleButtons (-1);
		else if (direction.y < 0)
			CycleButtons (1);
	}
	public virtual void OnAction () {
		if (!currentButton)
			return;
		else
			currentButton.OnClick ();
	}
	void CycleButtons (int dir) {
		int currentIndex = 0;
		UIButton buttonToSelect;
		if (currentButton == null) {
			if (buttons.Count == 0)
				return;
			else
				buttonToSelect = buttons[0];
		} else {
			for (int i = 0; i < buttons.Count; i++) {
				if (buttons[i] == currentButton) {
					currentIndex = i;
					break;
				}
			}
			currentIndex += dir;
			if (currentIndex >= buttons.Count)
				currentIndex = 0;
			if (currentIndex < 0)
				currentIndex = buttons.Count - 1;
		}

		buttonToSelect = buttons[currentIndex];
		Select (buttonToSelect);
	}
}
