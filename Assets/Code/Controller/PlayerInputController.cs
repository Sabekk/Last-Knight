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
	}

	void OnEnable () {
		Input.Enable ();
	}

	void OnDisable () {
		Input.Disable ();
	}

	public void OnAim (InputAction.CallbackContext context) {
	}

	public void OnAttack (InputAction.CallbackContext context) {
	}

	public void OnItem1 (InputAction.CallbackContext context) {
	}

	public void OnItem2 (InputAction.CallbackContext context) {
	}

	public void OnItem3 (InputAction.CallbackContext context) {
	}

	public void OnItem4 (InputAction.CallbackContext context) {
	}

	public void OnItem5 (InputAction.CallbackContext context) {
	}

	public void OnJump (InputAction.CallbackContext context) {
		if (context.performed)
			Events.Gameplay.Move.OnJump.Invoke ();
	}

	public void OnMovement (InputAction.CallbackContext context) {
		Events.Gameplay.Move.OnMoveInDirection.Invoke (context.ReadValue<Vector2> ());
	}

	public void OnNextItem (InputAction.CallbackContext context) {
	}

	public void OnOpenMenu (InputAction.CallbackContext context) {
	}

	public void OnPreviousItem (InputAction.CallbackContext context) {
	}
}
