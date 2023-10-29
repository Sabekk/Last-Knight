using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIView : MonoBehaviour {
	[SerializeField] string viewName;
	public static UIView current;
	UIView previous;
	public string Name => viewName;

	public virtual void Initialize () {

	}
	public virtual void Actiavate () {
		OnActivate ();
		gameObject.SetActive (true);
		if (previous != this)
			previous = current;
		if (current)
			current.Deactiavate ();
		current = this;
	}
	public virtual void Deactiavate () {
		OnDeactivate ();
		gameObject.SetActive (false);
		if (current == this)
			current = null;
	}
	public virtual void OnActivate () {

	}
	public virtual void OnDeactivate () {

	}
	public virtual void OnRefresh () {
	}
	public virtual void BackToPrevious () {
		if (previous)
			previous.Actiavate ();
		else
			Deactiavate ();
	}
}
