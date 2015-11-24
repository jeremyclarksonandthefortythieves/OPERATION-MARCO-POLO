using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class TerminalScript : MonoBehaviour {

	System.Random getRandom = new System.Random();

	private int[] password = new int[3];
	private GameObject[] passwordObjects;
	private bool locked;
	private GameObject inputUI = null;
	private bool active = false;
	private GameObject player;


	public int id;
	public GameObject codeInput;
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

	public string GetPassword() {
		return password[0].ToString() + password[1].ToString() + password[2].ToString();
	}

	public void UseTerminal() {

		if (locked && inputUI == null && !active) {
			Debug.Log(GetPassword());
			active = true;
			inputUI = Instantiate(codeInput) as GameObject;
			inputUI.transform.SetParent(_canvas.transform, false);
			inputUI.GetComponent<InputField>().onEndEdit.AddListener(delegate { EnterPassword(inputUI.GetComponent<InputField>().text); });

		} else if(!locked && !active) {
			Debug.Log("Unlocked");
		}
	}

	void OnTriggerExit(Collider coll) {
		if (active && coll.gameObject.tag == "Player") {
			Debug.Log("player away from terminal");
			Destroy(inputUI);
			inputUI = null;
			active = false;
		}
	}

	public void EnterPassword(string s) {
		if (s == GetPassword()) {
			Debug.Log("Good pass");
			locked = false;
			Destroy(inputUI);
			inputUI = null;
		} else {
			Debug.Log("Wrong Password");
		}
	}
}
