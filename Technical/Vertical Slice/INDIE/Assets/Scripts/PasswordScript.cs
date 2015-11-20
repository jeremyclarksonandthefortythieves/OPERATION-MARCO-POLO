using UnityEngine;
using System.Collections;

public class PasswordScript : MonoBehaviour {

	public int id;
	private int[] password;

	public void SetPassword(int[] pass) {
		password = pass;
	}

	public string GetPassword() {
		return password[0].ToString() + password[1].ToString() + password[2].ToString();
	}
}
