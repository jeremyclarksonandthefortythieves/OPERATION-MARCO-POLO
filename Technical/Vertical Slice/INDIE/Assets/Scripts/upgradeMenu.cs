using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class upgradeMenu : MonoBehaviour
{

	public Canvas upgradeScreen;
    public Image silencer,
                 Smoke,
                 Mine,
                 silencerDone,
                 smokeDone,
                 mineDone;
	public Text notEnough,
				tokens;

    private Image logo1,
                 logo2,
                 logo3,
                 logo4,
                 logo5;

    //private PlayerControl player;
    private LoadSaveScript player;
	private float textTimer = 2;

	// Use this for initialization
	void Start() {
		//  player = GameObject.Find("menuController").GetComponent<PlayerControl>();
		notEnough.enabled = false;
		player = GameObject.FindGameObjectWithTag("GameController").GetComponent<LoadSaveScript>();
		player.Load();

        silencerDone.enabled = false;
        smokeDone.enabled = false;
        mineDone.enabled = false;
        logo1 = GameObject.Find("1logo").GetComponent<Image>();
        logo2 = GameObject.Find("2logo").GetComponent<Image>();
        logo3 = GameObject.Find("3logo").GetComponent<Image>();
        logo4 = GameObject.Find("4logo").GetComponent<Image>();
        logo5 = GameObject.Find("5logo").GetComponent<Image>();

        logo1.enabled = false;
        logo2.enabled = false;
        logo3.enabled = false;
        logo4.enabled = false;
        logo5.enabled = false;
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
		if (player.saveData.money >= 3 && !player.saveData.silencer) {
			player.saveData.money -= 3;
			player.saveData.silencer = true;
            silencerDone.enabled = true;
		} else if (player.saveData.money < 3) {
			notEnough.enabled = true;
		}

	}

	public void smokeGrenade() {
		if (player.saveData.money >= 3) {
			player.saveData.money -= 3;
			player.saveData.smoke += 1;
			smokeDone.enabled = true;
		} else if (player.saveData.money < 3) {
			notEnough.enabled = true;
		}
	}

	public void proximityMine() {
		if (player.saveData.money >= 3) {
			player.saveData.money -= 3;
			player.saveData.mine += 1;
			mineDone.enabled = true;
		} else if (player.saveData.money < 3) {
			notEnough.enabled = true;
		}
	}

	public void weaponDMGU1() {
		if (player.saveData.money >= 2) {
			player.saveData.money -= 2;
           // player.saveData.bulletDamage *= 1.2f;
            logo1.enabled = true;
		} else if (player.saveData.money < 2) {
			notEnough.enabled = true;
		}
	}

    public void weaponDMGU2()
    {
        if (player.saveData.money >= 3)
        {
            player.saveData.money -= 3;
            //player.saveData.bulletDamage *= 1.2f;
            logo1.enabled = true;
        }
        else if (player.saveData.money < 3)
        {
            notEnough.enabled = true;
        }
    }

    public void weaponDMGU3()
    {
        if (player.saveData.money >= 4)
        {
            player.saveData.money -= 4;
            //  player.saveData.bulletDamage *= 1.2f;
            logo1.enabled = true;
        }
        else if (player.saveData.money < 4)
        {
            notEnough.enabled = true;
        }
    }

    public void weaponDMGU4()
    {
        if (player.saveData.money >= 5)
        {
            player.saveData.money -= 5;
            //  player.saveData.bulletDamage *= 1.2f;
            logo1.enabled = true;
        }
        else if (player.saveData.money < 5)
        {
            notEnough.enabled = true;
        }
    }

    public void weaponDMGU5()
    {
        if (player.saveData.money >= 6)
        {
            player.saveData.money -= 6;
            //  player.saveData.bulletDamage *= 1.2f;
            logo1.enabled = true;
        }
        else if (player.saveData.money < 6)
        {
            notEnough.enabled = true;
        }
    }

    public void NextLevel() {
		player.SaveInUI();
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
