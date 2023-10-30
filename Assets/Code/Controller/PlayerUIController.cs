using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerUIController : MonoSingleton<PlayerUIController>, InputBinds.IUIActions {
	[SerializeField] bool initialActive;
	static InputBinds _controll;
	bool gamePadInUse;
	public bool GamepadUsing => gamePadInUse;
	public static InputBinds Input {
		get {
			if (_controll == null)
				_controll = new InputBinds ();
			return _controll;
		}
	}

	protected override void Awake () {
		base.Awake ();
		Input.UI.SetCallbacks (this);
		Events.Gameplay.State.OnGameStateChanged += OnStateChange;
	}
	private void OnEnable () {
		if (initialActive)
			Input.Enable();
	}

	private void OnDestroy () {
		Events.Gameplay.State.OnGameStateChanged -= OnStateChange;
	}

	private void Update () {
		bool gamePadUsing = gamePadInUse;
		if (Keyboard.current != null && Keyboard.current.anyKey.isPressed)
			gamePadUsing = false;
		else if (Gamepad.current != null) {
			foreach (var controll in Gamepad.current.allControls) {
				if (controll is ButtonControl button)
					if (button.wasPressedThisFrame) {
						gamePadUsing = true;
						break;
					}
			}
		}
		if (gamePadInUse == gamePadUsing)
			return;
		gamePadInUse = gamePadUsing;
		Events.Player.Controller.OnChangeControllerState.Invoke ();

	}
	void OnStateChange (LevelManager.GameState state) {
		switch (state) {
			case LevelManager.GameState.play:
			Input.Disable ();
			break;
			case LevelManager.GameState.pause:
			case LevelManager.GameState.endgame:
			Input.Enable ();
			break;
			default:
			break;
		}
	}
	public void OnBack (InputAction.CallbackContext context) {
		if (context.performed) {
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
		if (!context.started)
			return;
		if (UIView.current)
			UIView.current.OnNavigate (context.ReadValue<Vector2> ());
	}

	public void OnSelection (InputAction.CallbackContext context) {
		if (!context.started)
			return;
		if (UIView.current)
			UIView.current.OnAction ();
	}
}
