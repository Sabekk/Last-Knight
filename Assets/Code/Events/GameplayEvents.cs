using UnityEngine;

public class GameplayEvents {
	public Moving Move { get; private set; } = new Moving ();

	/// <summary>
	/// Events of player moving
	/// </summary>
	public class Moving {
		/// <summary>
		/// Called when player jump
		/// </summary>
		public Events.Event OnJump = new Events.Event ();
		/// <summary>
		/// Called when player make move
		/// </summary>
		public Events.Event<Vector2> OnMoveInDirection = new Events.Event<Vector2> ();
	}
}
