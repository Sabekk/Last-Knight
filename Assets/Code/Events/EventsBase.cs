using System;

public static class Events {
	static UIEvents _ui;
	static GameplayEvents _gameplay;
	static PlayerEvents _player;
	static SceneEvents _scene;
	public static UIEvents UI {
		get {
			if (_ui == null)
				_ui = new UIEvents ();
			return _ui;
		}
	}

	public static GameplayEvents Gameplay {
		get {
			if (_gameplay == null)
				_gameplay = new GameplayEvents ();
			return _gameplay;
		}
	}

	public static PlayerEvents Player {
		get {
			if (_player == null)
				_player = new PlayerEvents ();
			return _player;
		}
	}
	public static SceneEvents Scene {
		get {
			if (_scene == null)
				_scene = new SceneEvents ();
			return _scene;
		}
	}

	public abstract class EventBase {
		event Action _event;

		public EventBase () { }
		public void Subscribe (Action subscriber) {
			_event += subscriber;
		}
		public void Unsubscribe (Action subscriber) {
			_event -= subscriber;
		}
		public static EventBase operator + (EventBase e, Action subscriber) {
			e.Subscribe (subscriber);
			return e;
		}
		public static EventBase operator - (EventBase e, Action subscriber) {
			e.Unsubscribe (subscriber);
			return e;
		}

		protected void InvokeBase () {
			if (_event != null)
				_event.Invoke ();
		}
	}

	public class Event : EventBase {
		public Event () : base () { }
		public void Invoke () {
			InvokeBase ();
		}
		public static Event operator + (Event e, Action subscriber) {
			e.Subscribe (subscriber);
			return e;
		}
		public static Event operator - (Event e, Action subscriber) {
			e.Unsubscribe (subscriber);
			return e;
		}
	}
	public class Event<T> : EventBase {
		event Action<T> _event;

		public Event () : base () {
		}
		public void Subscribe (Action<T> subscriber) {
			_event += subscriber;
		}
		public void Unsubscribe (Action<T> subscriber) {
			_event -= subscriber;
		}

		public static Event<T> operator + (Event<T> e, Action<T> subscriber) {
			e.Subscribe (subscriber);
			return e;
		}
		public static Event<T> operator - (Event<T> e, Action<T> subscriber) {
			e.Unsubscribe (subscriber);
			return e;
		}

		public void Invoke (T args) {
			if (_event != null)
				_event.Invoke (args);
			
			InvokeBase ();
		}
	}

	public class Event<T1, T2> : EventBase {
		event Action<T1, T2> _event;

		public Event () : base () {
		}
		public void Subscribe (Action<T1, T2> subscriber) {
			_event += subscriber;
		}
		public void Unsubscribe (Action<T1, T2> subscriber) {
			_event -= subscriber;
		}

		public static Event<T1, T2> operator + (Event<T1, T2> e, Action<T1, T2> subscriber) {
			e.Subscribe (subscriber);
			return e;
		}
		public static Event<T1, T2> operator - (Event<T1, T2> e, Action<T1, T2> subscriber) {
			e.Unsubscribe (subscriber);
			return e;
		}

		public void Invoke (T1 args1, T2 args2) {
			if (_event != null)
				_event.Invoke (args1, args2);

			InvokeBase ();
		}
	}
}
