using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILevelButton : UIButton, ObjectPool.IPoolable {
	[SerializeField] Sprite enabled;
	[SerializeField] Sprite disabled;
	[SerializeField] TMP_Text title;
	string levelName;

	public ObjectPool.PoolObject Poolable { get; set; }

	public override void OnClick () {
		base.OnClick ();
		GameplaySceneManager.LoadInitializedScene (levelName, true);
	}
	public void Initialize (string title, string levelName, bool isUnlocked) {
		Initialize ();
		this.title.SetText (title);
		this.levelName = levelName;
		button.interactable = isUnlocked;
		mainImage.sprite = isUnlocked ? enabled : disabled;
	}

	public void AssignPoolable (ObjectPool.PoolObject poolable) {
		Poolable = poolable;
	}
}
