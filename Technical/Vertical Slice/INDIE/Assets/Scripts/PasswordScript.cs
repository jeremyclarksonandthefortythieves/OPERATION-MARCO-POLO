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

	public void GetPassword() {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<PlayerControl>().controlsEnabled = false;
		img = Instantiate(passwordImage);
		img.transform.SetParent(_canvas.transform, false);
		Text text = img.GetComponentInChildren<UnityEngine.UI.Text>();
		text.text = password[0].ToString() + password[1].ToString() + password[2].ToString();
		activated = true;
	}
}
