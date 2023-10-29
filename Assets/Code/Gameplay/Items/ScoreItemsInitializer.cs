using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItemsInitializer : MonoBehaviour {
	readonly string[] possibleItems = {
		"scoreItem_banana",
		"scoreItem_apple",
		"scoreItem_cherry",
		"scoreItem_kiwi",
		"scoreItem_melon",
		"scoreItem_orange",
		"scoreItem_pineapple",
		"scoreItem_strawberry"
		};
	List<Transform> scoreItemsHolders;

	private void Awake () {
		scoreItemsHolders = new List<Transform> ();
		scoreItemsHolders.AddRange (GetComponentsInChildren<Transform> ());
		scoreItemsHolders.Remove (transform);
		foreach (var holder in scoreItemsHolders) {
			ScoreItem item = ObjectPool.Instance.GetFromPool (possibleItems[Random.Range (0, possibleItems.Length - 1)]).GetComponent<ScoreItem> ();
			item.Initialize ();
			item.transform.SetParent (holder);
			item.transform.position = holder.position;
		}
	}
	private void OnDestroy () {
		scoreItemsHolders.Clear ();
	}
}
