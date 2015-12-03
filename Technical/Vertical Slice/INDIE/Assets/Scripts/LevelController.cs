using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : MonoBehaviour {

	public bool noDetection = true;
	public bool noKills = true;
	public GameObject fadePanel;

	private GameObject gameController;
	private GameObject player;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		gameController = GameObject.FindGameObjectWithTag("GameController");
		gameController.GetComponent<LoadSaveScript>().Load();
		StartCoroutine(FadeIn());

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
		//GameObject fade = Instantiate(fadePanel) as GameObject;
		//fade
		StartCoroutine(FadeOut());
		//NextLevel();
		//spawns the upgrade menu here with button for next level
	}

	IEnumerator FadeOut() {
		float i = 0;
		Image img = fadePanel.GetComponent<Image>();
		Color col = img.color;

        while (i < 1.2f) {
			i += 1f * Time.deltaTime;

			col.a = i;
			img.color = col;

			yield return null;
        }
		NextLevel();
	}

	IEnumerator FadeIn() {
		float i = 1.1f;
		Image img = fadePanel.GetComponent<Image>();
		Color col = img.color;


		while (i >= 0f) {
			i -= 1f * Time.deltaTime;

			col.a = i;
			img.color = col;

			yield return null;
		}
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
