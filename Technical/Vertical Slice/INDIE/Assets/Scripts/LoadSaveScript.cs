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

		if (!File.Exists(url + fileName)) {
			NewGame();
		}
	}

	void createFolder(string folder) {
		//creates a path on the local hard drive.
		if (!System.IO.Directory.Exists(folder)) {
			System.IO.Directory.CreateDirectory(folder);
		}
	}


	//removes the file. all values in savedata are reset creates new file. 
	public void NewGame() {
		if(File.Exists(url + fileName)) File.Delete(url + fileName);

		saveData.money = 0;
		saveData.exp = 0;
		saveData.mine = 0;
		saveData.smoke = 0;
		saveData.silencer = false;
        saveData.bulletDamage = 2.0f;

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
		saveData.exp = player.exp;
		saveData.mine = player.distractionAmount;
		saveData.smoke = player.smokeAmount;
		saveData.silencer = false;
        saveData.bulletDamage = player.bulletDamage;


		saveData.currentLevel = Application.loadedLevel;



		createFolder(url);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(url + fileName);
		bf.Serialize(file, saveData);
		file.Close();
	}

	public void SaveInUI() {

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

		player.silencerEnabled = saveData.silencer;
		player.distractionAmount = saveData.mine;
		player.smokeAmount = saveData.smoke;
		player.money = saveData.money;
		player.exp = saveData.exp;
	}
}
