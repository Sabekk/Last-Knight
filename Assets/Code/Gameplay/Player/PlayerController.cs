using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Rigidbody2D))]
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
	bool isMovable;

	MovingState currentState;
	Vector2 direction = Vector2.zero;

	public bool IsGrounded => Physics2D.BoxCast (transform.position, playerCollider.size, 0, Vector2.down, playerCollider.bounds.extents.y + 0.2f, LayerMask.GetMask ("ground"));

	private void Awake () {
		isRightDirection = true;
		isMovable = true;
		bodySprite = GetComponent<SpriteRenderer> ();
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		playerCollider = GetComponent<BoxCollider2D> ();

		Events.Gameplay.Move.OnMoveInDirection += OnMove;
		Events.Gameplay.Move.OnJump += OnJump;
		Events.Gameplay.Player.OnDeath += OnDeath;
	}
	private void OnDestroy () {
		Dispose ();
	}
	private void Update () {
		if (!isMovable)
			return;
		MovePlayer ();
		UpdateState ();
	}
	void Dispose () {
		Events.Gameplay.Move.OnMoveInDirection -= OnMove;
		Events.Gameplay.Move.OnJump -= OnJump;
		Events.Gameplay.Player.OnDeath -= OnDeath;
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
		SoundManager.Instance.PlayEffectSound ("jump");
		rb.velocity += Vector2.up * jumpPower;
	}

	void UpdateState () {
		if (!IsGrounded) {
			if (rb.velocity.y > 1.5f)
				currentState = MovingState.jumping;
			else if (rb.velocity.y < 1.5f)
				currentState = MovingState.falling;
		} else {
			if (direction.x > 0 || direction.x < 0)
				currentState = MovingState.running;
			else
				currentState = MovingState.idle;
		}
		animator.SetInteger ("CurrentState", (int)currentState);
	}

	void OnDeath () {
		Dispose ();
		rb.velocity = Vector2.zero;
		direction = Vector2.zero;
		isMovable = false;
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
	}
}
