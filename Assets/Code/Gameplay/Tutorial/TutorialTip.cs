using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTip : MonoBehaviour
{
	[SerializeField] GameObject pcVersion;
	[SerializeField] GameObject consoleVersion;

	private void Awake () {
		Events.Player.Controller.OnChangeControllerState += ChangeTutorial;
	}

	private void Start () {
		ChangeTutorial ();
	}
	private void OnDestroy () {
		Events.Player.Controller.OnChangeControllerState -= ChangeTutorial;
	}
	void ChangeTutorial () {
		pcVersion.SetActive (!PlayerUIController.Instance.GamepadUsing);
		consoleVersion.SetActive (PlayerUIController.Instance.GamepadUsing);
	}
}
