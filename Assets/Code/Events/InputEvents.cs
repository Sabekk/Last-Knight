public class InputEvents {
	public PlayerStatus Stauts { get; private set; } = new PlayerStatus ();
	public ViewStatus View { get; private set; } = new ViewStatus ();
	/// <summary>
	/// Events of player status
	/// </summary>
	public class PlayerStatus {
		/// <summary>
		/// Called when need to refresh player score
		/// </summary>
		public Events.Event OnRefreshPlayerScore = new Events.Event ();
	}

	/// <summary>
	/// Events of ui views
	/// </summary>
	public class ViewStatus {
		/// <summary>
		/// Called when call for ui view
		/// </summary>
		public Events.Event<string> OnCallView = new Events.Event<string> ();
	}

}
