public class PlayerEvents {
	public InputHolder Input { get; private set; } = new InputHolder ();
	/// <summary>
	/// Events of player inputs
	/// </summary>
	public class InputHolder {
		/// <summary>
		/// Called when player pause game
		/// </summary>
		public Events.Event OnPause = new Events.Event ();
	}

}
