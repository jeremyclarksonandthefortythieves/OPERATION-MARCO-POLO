using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class upgradeMenu : MonoBehaviour
{

	public Canvas upgradeScreen;
	public Image silencer,
				 Smoke,
				 Mine;
	public Text notEnough,
				tokens;

	//private PlayerControl player;
	private LoadSaveScript player;
	private float textTimer = 2;

	// Use this for initialization
	void Start() {
		//  player = GameObject.Find("menuController").GetComponent<PlayerControl>();
		notEnough.enabled = false;
		player = GameObject.FindGameObjectWithTag("GameController").GetComponent<LoadSaveScript>();
		player.Load();
	}

	// Update is called once per frame
	void Update() {
		tokens.text = "Money: " + player.saveData.money.ToString();

		if (notEnough.enabled) {
			textTimer -= Time.deltaTime;

			if (textTimer <= 0) {
				notEnough.enabled = false;
				textTimer = 2;
			}
		}
	}

	public void Silencer() {
		if (player.saveData.money >= 3) {
			player.saveData.money -= 3;
			player.saveData.silencer = true;
			silencer.enabled = false;
		} else if (player.saveData.money < 3) {
			notEnough.enabled = true;
		}

	}

	public void smokeGrenade() {
		if (player.saveData.money >= 3) {
			player.saveData.money -= 3;
			player.saveData.smoke += 1;
			Smoke.enabled = false;
		} else if (player.saveData.money < 3) {
			notEnough.enabled = true;
		}
	}

	public void proximityMine() {
		if (player.saveData.money >= 3) {
			player.saveData.money -= 3;
			player.saveData.mine += 1;
			Mine.enabled = false;
		} else if (player.saveData.money < 3) {
			notEnough.enabled = true;
		}
	}

	public void weaponDMGU1() {
		if (player.saveData.money >= 2) {
			player.saveData.money -= 2;
			// player.saveData.bulletDamage += 2;
		} else if (player.saveData.money < 2) {
			notEnough.enabled = true;
		}
	}

	public void NextLevel() {
		player.SaveInUI();
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
