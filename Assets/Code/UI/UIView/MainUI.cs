using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour {
	[SerializeField] UIView[] views;
	[SerializeField] GameObject background;
	[SerializeField] UIView initialActivation;

	private void Start () {
		Initialize ();
		if (initialActivation)
			initialActivation.Activate ();
	}
	private void OnDestroy () {
		Events.UI.View.OnCallView -= ActivateView;
	}

	void Initialize () {
		Events.UI.View.OnCallView += ActivateView;

		foreach (var view in views) {
			view.Initialize ();
		}
	}

	void ActivateView (string name) {
		foreach (var view in views) {
			if (view.Name == name) {
				view.Activate ();
				break;
			}
		}
	}

}
