using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TerminalScript : MonoBehaviour {

	System.Random getRandom = new System.Random();

	private int[] password = new int[3];
	private GameObject[] passwordObjects;
	private GameObject terminalUI;
	private bool active = false;
	private GameObject player;
	private bool opened = false;

	public bool locked = true;
	public int id;
	public GameObject linkedDoor;
	public GameObject codeInput;
	public GameObject openDoorButton;
	public GameObject _canvas;

	void Start() {
		password[0] = getRandom.Next(10);
		password[1] = getRandom.Next(10);
		password[2] = getRandom.Next(10);
		//searches for objects that show the password. if ID is the same as the terminal change password of the password object to this one 
		player = GameObject.FindGameObjectWithTag("Player");
		passwordObjects = GameObject.FindGameObjectsWithTag("Password");
		foreach(GameObject p in passwordObjects) {
			if(p.GetComponent<PasswordScript>().id == id){
				p.GetComponent<PasswordScript>().SetPassword(password);
			}
		}
	}

	void Update() {
		if (active) {
			//removes UI from scene. and enables player controls again
			if (Input.GetKeyDown(KeyCode.E)) {
				Destroy(terminalUI);
				Destroy(terminalUI);
				terminalUI = null;
				player.GetComponent<PlayerControl>().controlsEnabled = true;
				active = false;

			}
		}
	}

	public string GetPassword() {
		return password[0].ToString() + password[1].ToString() + password[2].ToString();
	}

	//checks if terminal is unlocked and shows UI depending if ulocked
	public void UseTerminal() {
		Debug.Log("Use terminal");
		if (locked && !active) {
			Debug.Log(GetPassword());
			active = true;
			terminalUI = Instantiate(codeInput) as GameObject;
			terminalUI.transform.SetParent(_canvas.transform, false);
			terminalUI.GetComponent<InputField>().onEndEdit.AddListener(delegate { EnterPassword(terminalUI.GetComponent<InputField>().text); });
			player.GetComponent<PlayerControl>().controlsEnabled = false;

		} else if(!locked && !active && !opened) {
			active = true;
			terminalUI = Instantiate(openDoorButton) as GameObject;
			terminalUI.transform.SetParent(_canvas.transform, false);
			terminalUI.GetComponent<Button>().onClick.AddListener(delegate { OpenDoor(); });
			player.GetComponent<PlayerControl>().controlsEnabled = false;

		}
	}

	//opens the linked door
	public void OpenDoor() {
		if (Application.loadedLevel == 3) GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>().noDetection = true;

		locked = false;
		active = false;
		Destroy(terminalUI);
		Destroy(linkedDoor);
		opened = true;
		terminalUI = null;
		player.GetComponent<PlayerControl>().controlsEnabled = true;

	}

	//gets the string from the ui input
	public void EnterPassword(string s) {
		if (s == GetPassword()) {
			Debug.Log("Good pass");
			locked = false;
			active = false;
			Destroy(terminalUI);
			UseTerminal();
		} else {
			Debug.Log("Wrong Password");
		}
	}
}
