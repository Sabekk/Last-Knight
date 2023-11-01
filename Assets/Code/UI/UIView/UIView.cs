using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIView : MonoBehaviour {
	[SerializeField] string viewName;
	UIView previous;
	UISelectable currentSelection;
	protected List<UISelectable> selections;
	public string Name => viewName;

	public static UIView current;

	public virtual void Initialize () {
		selections = new List<UISelectable> ();
		InitializeStaticButtons ();
	}

	protected void InitializeStaticButtons () {
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
				Select (GetFirstInteractable ());
		} else {
			if (currentSelection)
				Deselect (currentSelection);
		}
	}
	protected virtual UISelectable GetFirstInteractable () {
		foreach (var selection in selections) {
			if (selection.Interactable)
				return selection;
		}
		return null;
	}
	public virtual void BackToPrevious () {
		if (previous)
			previous.Activate ();
		else
			Deactivate ();
	}
	public virtual void Select (UISelectable selectable) {
		if (selectable && !selectable.Interactable)
			return;

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
		UISelectable elementToSelect = null;
		if (currentSelection == null) {
			if (selections.Count == 0)
				return;
			else
				elementToSelect = GetFirstInteractable ();
		} else {
			for (int i = 0; i < selections.Count; i++) {
				if (selections[i] == currentSelection) {
					currentIndex = i;
					break;
				}
			}

			int startingIndex = currentIndex;
			if (dir > 0) {
				for (int i = startingIndex; i < selections.Count; i++) {
					if (currentSelection!= selections[i] && selections[i].Interactable) {
						elementToSelect = selections[i];
						break;
					}
				}
				if (elementToSelect == null) {
					for (int i = 0; i < startingIndex; i++) {
						if (currentSelection != selections[i] && selections[i].Interactable) {
							elementToSelect = selections[i];
							break;
						}
					}
				}

			} else if (dir < 0) {
				for (int i = startingIndex; i >=0; i--) {
					if (currentSelection != selections[i] && selections[i].Interactable) {
						elementToSelect = selections[i];
						break;
					}
				}
				if (elementToSelect == null) {
					for (int i = selections.Count-1; i >= startingIndex; i--) {
						if (currentSelection != selections[i] && selections[i].Interactable) {
							elementToSelect = selections[i];
							break;
						}
					}
				}
			}

			//currentIndex += dir;
			//if (currentIndex >= selections.Count)
			//		currentIndex = 0;
			//if (currentIndex < 0)
			//	currentIndex = selections.Count - 1;
		}

		//elementToSelect = selections[currentIndex];
		Select (elementToSelect);
	}
}
