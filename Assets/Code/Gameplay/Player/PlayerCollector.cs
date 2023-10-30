using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour {
	private void OnTriggerEnter2D (Collider2D collision) {
		if (collision.gameObject.tag == "FinishZone")
			Events.Gameplay.Level.OnLevelFinish.Invoke ();
		if (CollectableMatcher.Instance.CheckCollection (collision, out ICollectable collectable)) {
			collectable.Collect ();
		}
	}
}
