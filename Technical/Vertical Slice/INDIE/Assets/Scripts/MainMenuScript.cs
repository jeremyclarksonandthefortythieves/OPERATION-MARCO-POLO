using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {


	private LoadSaveScript saveScript;

	void Start() {
		saveScript = GameObject.FindGameObjectWithTag("GameController").GetComponent<LoadSaveScript>();
		saveScript.Load();
		DontDestroyOnLoad(gameObject);
	}

	public void ContinueGame() {
		Application.LoadLevel(0);
		
	}

	public void NewGame() {
		saveScript.NewGame();
		Application.LoadLevel(0);
	}

	public void ExitGame() {
		Application.Quit();
	}
}
