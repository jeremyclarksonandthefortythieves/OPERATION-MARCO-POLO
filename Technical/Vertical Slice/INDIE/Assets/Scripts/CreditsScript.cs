using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour {

	public float timer = 3;

	void Update() {
		timer -= 1f * Time.deltaTime;
		if (timer < 0f) Application.LoadLevel("MainMenu");
	}
}
