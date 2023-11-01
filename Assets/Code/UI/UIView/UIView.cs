using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIView : MonoBehaviour {
	[SerializeField] string viewName;
	UIView previous;
	UISelectable currentSelection;
	List<UISelectable> selections;
	public string Name => viewName;

	public static UIView current;
	public virtual void Initialize () {
		selections = new List<UISelectable> ();
		selections.AddRange (GetComponentsInChildren<UISelectable> (true));
		foreach (var button in selections) {
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
		if (currentSelection)
			Deselect (currentSelection);
	}
	public virtual void OnActivate () {
		Events.Player.Controller.OnChangeControllerState += Refresh;
	}
	public virtual void OnDeactivate () {
		Events.Player.Controller.OnChangeControllerState -= Refresh;
	}
	public virtual void Refresh () {
		if (PlayerUIController.Instance.GamepadUsing) {
			if (selections.Count > 0)
				Select (selections[0]);
		} else {
			if (currentSelection)
				Deselect (currentSelection);
		}
	}
	public virtual void BackToPrevious () {
		if (previous)
			previous.Activate ();
		else
			Deactivate ();
	}
	public virtual void Select (UISelectable selectable) {
		if (currentSelection == selectable)
			return;
		if (currentSelection) {
			currentSelection.Deselect ();
		}
		currentSelection = selectable;
		if (currentSelection)
			currentSelection.Select ();
	}
	public virtual void Deselect (UISelectable selectable) {
		if (currentSelection && currentSelection == selectable)
			currentSelection.Deselect ();
		currentSelection = null;
	}
	public virtual void OnNavigate (Vector2 direction) {
		if (direction.y > 0)
			CycleSelections (-1);
		else if (direction.y < 0)
			CycleSelections (1);
		if (currentSelection is UISlider slider) {
			if (direction.x != 0)
				slider.ChangeValue (direction.x);
		}
	}
	public virtual void OnAction () {
		if (!currentSelection)
			return;
		else if (currentSelection is UIButton button)
			button.OnClick ();
	}
	void CycleSelections (int dir) {
		int currentIndex = 0;
		UISelectable elementToSelect;
		if (currentSelection == null) {
			if (selections.Count == 0)
				return;
			else
				elementToSelect = selections[0];
		} else {
			for (int i = 0; i < selections.Count; i++) {
				if (selections[i] == currentSelection) {
					currentIndex = i;
					break;
				}
			}
			currentIndex += dir;
			if (currentIndex >= selections.Count)
				currentIndex = 0;
			if (currentIndex < 0)
				currentIndex = selections.Count - 1;
		}

		elementToSelect = selections[currentIndex];
		Select (elementToSelect);
	}
}
