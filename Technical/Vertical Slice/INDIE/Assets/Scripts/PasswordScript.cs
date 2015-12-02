using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PasswordScript : MonoBehaviour {

	private int[] password;
	private GameObject img;
	private bool activated;

	public GameObject _canvas;
	public int id;
	public GameObject passwordImage;

	void Update() {
		if (activated) {
			//removes UI from scene. and enables player controls again
			if (Input.GetKeyDown(KeyCode.E)) {
				Destroy(img);
				activated = false;
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				player.GetComponent<PlayerControl>().controlsEnabled = true;
			}
		}

	}

	public void SetPassword(int[] pass) {
		password = pass;
	}

	//Function called when player interacts with gameobject
	
	public void GetPassword() {
		//Player cant move when he interacted with this gameobject
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<PlayerControl>().controlsEnabled = false;

		//loads an UI object. the object has a child with a component.
		//Change that text into the password
		img = Instantiate(passwordImage);
		img.transform.SetParent(_canvas.transform, false);
		Text text = img.GetComponentInChildren<UnityEngine.UI.Text>();
		text.text = password[0].ToString() + password[1].ToString() + password[2].ToString();
		activated = true;
	}
}
