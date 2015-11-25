using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TerminalScript : MonoBehaviour {

	System.Random getRandom = new System.Random();

	private int[] password = new int[3];
	private GameObject[] passwordObjects;
	private bool locked;
	private GameObject terminalUI;
	private bool active = false;
	private GameObject player;
	private bool opened = false;

	public int id;
	public GameObject linkedDoor;
	public GameObject codeInput;
	public GameObject openDoorButton;
	public GameObject _canvas;

	void Start() {
		locked = true;
		password [0] = getRandom.Next(10);
		password [1] = getRandom.Next(10);
		password [2] = getRandom.Next(10);

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

	public void UseTerminal() {
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

	public void OpenDoor() {
		locked = false;
		active = false;
		Destroy(terminalUI);
		Destroy(linkedDoor);
		opened = true;
	}

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
