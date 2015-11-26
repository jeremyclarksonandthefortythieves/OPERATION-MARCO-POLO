using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadSaveScript : MonoBehaviour {

	string url = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "/Marco Polo/";
	string fileName = "SaveData.sd";
	public SaveData saveData;

	void Awake() {
		DontDestroyOnLoad(this.gameObject);
		if (!File.Exists(url + fileName)) NewGame();
	}

	void createFolder(string folder) {
		//creates a path on the local hard drive.
		if (!System.IO.Directory.Exists(folder)) {
			System.IO.Directory.CreateDirectory(folder);
		}
	}



	public void NewGame() {
		if(File.Exists(url + fileName)) File.Delete(url + fileName);

		saveData.money = 0;
		saveData.currentLevel = 1;

		createFolder(url);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(url + fileName);
		bf.Serialize(file, saveData);
		file.Close();

	}

	public void Save() {
		PlayerControl player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

		saveData.money = player.money;
		saveData.currentLevel = Application.loadedLevel;


		createFolder(url);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(url + fileName);
		bf.Serialize(file, saveData);
		file.Close();
	}

	public void Load() {
		if (File.Exists(url + fileName)) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(url + fileName, FileMode.Open);
			saveData = (SaveData)bf.Deserialize(file);
			file.Close();
		}
	}

	public void LoadSkillStats() {
		PlayerControl player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

		Load();

		player.money = saveData.money;
	}
}
