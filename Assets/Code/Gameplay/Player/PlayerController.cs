using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour {
	[SerializeField] float speed;
	[SerializeField] float jumpPower;
	[SerializeField] float gravity;
	Rigidbody2D rb;

	Vector2 direction = Vector2.zero;

	private void Awake () {
		rb = GetComponent<Rigidbody2D> ();
		Events.Gameplay.Move.OnMoveInDirection += OnMove;
		Events.Gameplay.Move.OnJump += OnJump;
	}
	private void OnDestroy () {
		Events.Gameplay.Move.OnMoveInDirection -= OnMove;
		Events.Gameplay.Move.OnJump -= OnJump;
	}
	private void Update () {
		MovePlayer ();
	}
	void MovePlayer () {
		rb.velocity = new Vector2 (direction.x * speed, rb.velocity.y);
	}
	private void OnMove (Vector2 direction) {
		this.direction = direction;
	}
	void OnJump () {
		rb.velocity += Vector2.up * jumpPower;
	}
}
