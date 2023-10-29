using UnityEngine;

public class GameplayEvents {
	public Moving Move { get; private set; } = new Moving ();
	public PlayerCharacter Player { get; private set; } = new PlayerCharacter ();
	public CurrentLevel Level { get; private set; } = new CurrentLevel ();
	public GameState State { get; private set; } = new GameState ();

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
		/// Called when player get hit. Current health
		/// </summary>
		public Events.Event<int> OnGetHit = new Events.Event<int> ();
		/// <summary>
		/// Called when player death
		/// </summary>
		public Events.Event OnDeath = new Events.Event ();
	}

	/// <summary>
	/// Events of current playing level
	/// </summary>
	public class CurrentLevel {
		/// <summary>
		/// Called when player got points
		/// </summary>
		public Events.Event<int> OnGetPoint = new Events.Event<int> ();
	}

	/// <summary>
	/// Events of game state
	/// </summary>
	public class GameState {
		/// <summary>
		/// Called when game state changed
		/// </summary>
		public Events.Event<LevelManager.GameState> OnGameStateChanged = new Events.Event<LevelManager.GameState> ();
	}
}
