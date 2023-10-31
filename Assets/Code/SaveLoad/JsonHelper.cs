using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonHelper {
	public static string GetPath (string fileName) {
		return Application.persistentDataPath + "/" + fileName + ".json";
	}
	public static void SaveToJson<T> (T elementToSave, string filename) {
		string saveDataJson = JsonUtility.ToJson (elementToSave, true);
		WriteFile (filename, saveDataJson);
	}
	public static T ReadFromJson<T> (string filename) {
		string path = Application.persistentDataPath + "/" + filename + ".json";
		if (!File.Exists (path))
			return default (T);

		string content = File.ReadAllText (path);

		if (string.IsNullOrEmpty (content))
			return default (T);

		T res = JsonUtility.FromJson<T> (content);
		return res;
	}

	public static void WriteFile (string fileName, string saveDataJson) {
		FileStream fileStream = new FileStream (GetPath (fileName), FileMode.Create);
		using (StreamWriter writer = new StreamWriter (fileStream)) {
			writer.Write (saveDataJson);
		}
	}
}
