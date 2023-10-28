using UnityEngine;

public class GameplayEvents {
	public Moving Move { get; private set; } = new Moving ();
	public PlayerCharacter Player { get; private set; } = new PlayerCharacter ();

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
	/// <summary>
	/// Events of player character
	/// </summary>
	public class PlayerCharacter {
		/// <summary>
		/// Called when player get hit. Current health/max health
		/// </summary>
		public Events.Event<int, int> OnGetHit = new Events.Event<int, int> ();
		/// <summary>
		/// Called when player death
		/// </summary>
		public Events.Event OnDeath = new Events.Event ();
	}
}
