using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerializable {
	void LoadGame (PlayerSaveData saveData);
	void SaveGame (ref PlayerSaveData saveData);
}
