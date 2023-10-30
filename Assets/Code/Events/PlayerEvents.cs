public class PlayerEvents {
	public InputHolder Input { get; private set; } = new InputHolder ();
	public ControllerHolder Controller { get; private set; } = new ControllerHolder ();
	/// <summary>
	/// Events of player inputs
	/// </summary>
	public class InputHolder {
		/// <summary>
		/// Called when player pause game
		/// </summary>
		public Events.Event OnPause = new Events.Event ();
	}
	/// <summary>
	/// Events of current controller state
	/// </summary>
	public class ControllerHolder {
		/// <summary>
		/// Called when player pause game
		/// </summary>
		public Events.Event OnChangeControllerState = new Events.Event ();
	}

}
