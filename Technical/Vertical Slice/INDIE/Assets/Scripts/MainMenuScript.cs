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
		Application.LoadLevel(Application.loadedLevel + 1);
		
	}

	public void NewGame() {
		saveScript.NewGame();
		Application.LoadLevel(Application.loadedLevel + 1);

	}

	public void ExitGame() {
		Application.Quit();
	}
}
