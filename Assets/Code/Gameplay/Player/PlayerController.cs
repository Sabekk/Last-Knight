using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {
	enum MovingState { idle, running, jumping, falling }
	[SerializeField] float speed;
	[SerializeField] float jumpPower;
	[SerializeField] float gravity;

	SpriteRenderer bodySprite;
	Animator animator;
	Rigidbody2D rb;
	BoxCollider2D playerCollider;
	bool isRightDirection;
	MovingState currentState;
	Vector2 direction = Vector2.zero;

	public bool IsGrounded => Physics2D.BoxCast (transform.position, playerCollider.size, 0, Vector2.down, playerCollider.bounds.extents.y + 0.2f, LayerMask.GetMask ("ground"));


	private void Awake () {
		bodySprite = GetComponent<SpriteRenderer> ();
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		playerCollider = GetComponent<BoxCollider2D> ();
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
				bodySprite.flipX = false;
			}
		} else if (direction.x < 0) {
			if (isRightDirection) {
				isRightDirection = false;
				bodySprite.flipX = true;
			}
		}
	}
	void OnJump () {
		if (!IsGrounded)
			return;
		rb.velocity += Vector2.up * jumpPower;
	}

	void UpdateState () {
		if (!IsGrounded) {
			if (rb.velocity.y > 1.5f) {
				currentState = MovingState.jumping;
				Debug.Log ("jumping");
			}
			else if (rb.velocity.y < 1.5f) {
				Debug.Log ("falling");
				currentState = MovingState.falling;

			}
		} else {
			if (direction.x > 0 || direction.x < 0) {
				currentState = MovingState.running;
				Debug.Log ("running");
			}
			else
				currentState = MovingState.idle;
		}
		animator.SetInteger ("CurrentState", (int)currentState);
	}
}
