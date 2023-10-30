using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour, InputBinds.IPlayerActions {
	static InputBinds _controll;
	public static InputBinds Input {
		get {
			if (_controll == null)
				_controll = new InputBinds ();
			return _controll;
		}
	}

	private void Awake () {
		Input.Player.SetCallbacks (this);
		Events.Gameplay.State.OnGameStateChanged += OnStateChange;
	}
	void OnEnable () {
		Input.Enable ();
	}

	void OnDisable () {
		Input.Disable ();
	}
	void OnStateChange (LevelManager.GameState state) {
		switch (state) {
			case LevelManager.GameState.play:
			Input.Enable ();
			break;
			case LevelManager.GameState.pause:
			case LevelManager.GameState.endgame:
			Input.Disable ();
			break;
			default:
			break;
		}
	}

	public void OnJump (InputAction.CallbackContext context) {
		if (context.performed)
			Events.Gameplay.Move.OnJump.Invoke ();
	}

	public void OnMovement (InputAction.CallbackContext context) {
		Events.Gameplay.Move.OnMoveInDirection.Invoke (context.ReadValue<Vector2> ());
	}

	public void OnOpenMenu (InputAction.CallbackContext context) {
		if (context.started)
			Events.Player.Input.OnPause.Invoke ();
	}
}
