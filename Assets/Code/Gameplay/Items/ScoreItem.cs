using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : Item {

	[SerializeField] int points;
	public override void Initialize () {
		base.Initialize ();
		transform.localScale = Vector2.one * 3;
	}
	public override void OnCollect () {
		base.OnCollect ();
		Events.Gameplay.Level.OnGetPoint.Invoke (points);
		ObjectPool.Instance.ReturnToPool (this);
	}
}
