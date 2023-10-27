using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {
	enum MovingState { idle, running, jumping, falling }
	[SerializeField] float speed;
	[SerializeField] float jumpPower;
	[SerializeField] float gravity;
	Rigidbody2D rb;
	Animator animator;
	bool isRightDirection;
	MovingState currentState;
	Vector2 direction = Vector2.zero;
	Quaternion leftDirection = new Quaternion (0, 90, 0, 0);
	Quaternion rightDirection = new Quaternion (0, 0, 0, 0);


	private void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		isRightDirection = true;
		Events.Gameplay.Move.OnMoveInDirection += OnMove;
		Events.Gameplay.Move.OnJump += OnJump;
	}
	private void OnDestroy () {
		Events.Gameplay.Move.OnMoveInDirection -= OnMove;
		Events.Gameplay.Move.OnJump -= OnJump;
	}
	private void Update () {
		MovePlayer ();
		UpdateState ();
	}
	void MovePlayer () {
		rb.velocity = new Vector2 (direction.x * speed, rb.velocity.y);
	}
	private void OnMove (Vector2 direction) {
		this.direction = direction;

		if (direction.x > 0) {
			if (!isRightDirection) {
				isRightDirection = true;
				transform.rotation = rightDirection;
			}
		} else if (direction.x < 0) {
			if (isRightDirection) {
				isRightDirection = false;
				transform.rotation = leftDirection;
			}
		}
	}
	void OnJump () {
		rb.velocity += Vector2.up * jumpPower;
	}

	void UpdateState () {
		if (rb.velocity.y > 0)
			currentState = MovingState.jumping;
		else if (rb.velocity.y < 0)
			currentState = MovingState.falling;
		else {
			if (direction.x > 0 || direction.x < 0)
				currentState = MovingState.running;
			else
				currentState = MovingState.idle;
		}
		animator.SetInteger ("CurrentState", (int)currentState);
	}
}
