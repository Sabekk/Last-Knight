using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUIController : MonoBehaviour, InputBinds.IUIActions {
	static InputBinds _controll;
	public static InputBinds Input {
		get {
			if (_controll == null)
				_controll = new InputBinds ();
			return _controll;
		}
	}

	private void Awake () {
		Input.UI.SetCallbacks (this);
		Events.Gameplay.State.OnGameStateChanged += OnStateChange;
	}

	void OnStateChange (LevelManager.GameState state) {
		switch (state) {
			case LevelManager.GameState.play:
			Input.Disable ();
			break;
			case LevelManager.GameState.pause:
			case LevelManager.GameState.gameover:
			Input.Enable ();
			break;
			default:
			break;
		}
	}
	public void OnBack (InputAction.CallbackContext context) {
		if(context.performed) {
			if (UIView.current) {
				UIView.current.BackToPrevious ();
			}
		}
	}

	public void OnClose (InputAction.CallbackContext context) {
		if (context.started)
			Events.Player.Input.OnPause.Invoke ();
	}

	public void OnNavigation (InputAction.CallbackContext context) {

	}

	public void OnSelection (InputAction.CallbackContext context) {

	}
}
