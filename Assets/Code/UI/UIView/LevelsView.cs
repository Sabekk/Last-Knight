using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsView : UIView {
	[SerializeField] Transform buttonsHolder;

	const string LEVEL_BUTTON = "UI_levelButton";

	public override void OnActivate () {
		base.OnActivate ();
		for (int i = 0; i < 5; i++) {
			UILevelButton levelButton = ObjectPool.Instance.GetFromPool (LEVEL_BUTTON).GetComponent<UILevelButton> ();
			levelButton.transform.SetParent (buttonsHolder);
			levelButton.Initialize (i.ToString (), i == 0 ? "Tutorial" : "Level_" + i, PlayerData.Instance.UnlockedLevel >= i);
			levelButton.transform.localScale = Vector3.one;
			selections.Add (levelButton);
		}
	}

	public override void OnDeactivate () {
		base.OnDeactivate ();
		for (int i = selections.Count-1; i >=0; i--) {
			if(selections[i] is ObjectPool.IPoolable poolable) {
				ObjectPool.Instance.ReturnToPool (poolable);
			}
		}
	}

	protected override UISelectable GetFirstInteractable () {
		foreach (var selection in selections) {
			if (selection.Interactable && selection is UILevelButton)
				return selection;
		}
		return base.GetFirstInteractable ();
	}
}
