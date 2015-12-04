using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelController : MonoBehaviour {

	private bool objective1 = false;
	private bool objective2 = false;

	public GameObject obj1UI;
	public GameObject obj2UI;

	public GameObject fadePanel;

	private GameObject gameController;
	private GameObject player;

	void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
		gameController = GameObject.FindGameObjectWithTag("GameController");
		gameController.GetComponent<LoadSaveScript>().Load();
		StartCoroutine(FadeIn());
	}

	public void SetObjective1(bool b = true) {
		objective1 = b;
		obj1UI.SetActive(b);
	}

	public void SetObjective2(bool b = true) {
		objective2 = b;
		obj2UI.SetActive(b);
	}

	//Checks if objectives are complete and gives player exp
	public void CompleteLevel() {
		GetComponent<LoadSaveScript>().Save();
		if (objective1) {
			player.GetComponent<PlayerControl>().GetExp();
			//Debug.Log("completed with no detection");
		}
		if (objective2) {
			player.GetComponent<PlayerControl>().GetExp();
			//Debug.Log("completed without kills!");
		}
		//fade
		StartCoroutine(FadeOut());
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
