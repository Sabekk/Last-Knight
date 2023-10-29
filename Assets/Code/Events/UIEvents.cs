public class UIEvents {
	public PlayerStatus Stauts { get; private set; } = new PlayerStatus ();
	/// <summary>
	/// Events ofplayer status
	/// </summary>
	public class PlayerStatus {
		/// <summary>
		/// Called when need to refresh player score
		/// </summary>
		public Events.Event OnRefreshPlayerScore = new Events.Event ();
	}
}
