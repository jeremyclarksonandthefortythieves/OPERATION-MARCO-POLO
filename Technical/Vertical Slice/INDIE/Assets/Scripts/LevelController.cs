using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {

	public bool noDetection = true;
	public bool noKills = true;
	public GameObject loadUI;

	private GameObject gameController;
	private GameObject player;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		gameController = GameObject.FindGameObjectWithTag("GameController");
		gameController.GetComponent<LoadSaveScript>().Load();
	}

	//Checks if objectives are complete and gives player exp
	//
	public void CompleteLevel() {
		GetComponent<LoadSaveScript>().Save();
		if (noDetection) {
			player.GetComponent<PlayerControl>().GetExp();
			Debug.Log("completed with no detection");
		}
		if (noKills) {
			player.GetComponent<PlayerControl>().GetExp();
			Debug.Log("completed without kills!");
		}
		NextLevel();
		//spawns the upgrade menu here with button for next level
	}

	//asyncs load new level in the background
	//while watching a loading UI
	public void NextLevel() {
		gameController.GetComponent<LoadSaveScript>().Save();
		Application.LoadLevel(Application.loadedLevel + 1);

	}

	public void ContinueGame() {
		Application.LoadLevel(gameController.GetComponent<LoadSaveScript>().saveData.currentLevel);
		
	}

	public void NewGame() {
		gameController.GetComponent<LoadSaveScript>().NewGame();
		Application.LoadLevel(gameController.GetComponent<LoadSaveScript>().saveData.currentLevel);
	}

	public void ExitGame() {
		Application.Quit();
	}
}
