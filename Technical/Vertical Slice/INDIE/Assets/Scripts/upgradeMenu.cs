using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class upgradeMenu : MonoBehaviour {

    public Canvas upgradeScreen;
    public Image silencer,
                 Smoke,
                 Mine;
    public Text notEnough,
                tokens;

    private PlayerControl player;
    private float textTimer = 2;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("menuController").GetComponent<PlayerControl>();
        notEnough.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        tokens.text = player.money.ToString();

        if (notEnough.enabled)
        {
            textTimer -= Time.deltaTime;

            if(textTimer <= 0)
            {
                notEnough.enabled = false;
                textTimer = 2;
            }
        }
	}

    public void Silencer()
    {
        if(player.money >= 3)
        {
            player.money -= 3;
			player.silencerEnabled = true;
            silencer.enabled = false;
        }

        else if(player.money < 3)
        {
            notEnough.enabled = true;
        }

    }

    public void smokeGrenade()
    {
        if (player.money >= 3)
        {
            player.money -= 3;
			player.smokeAmount += 1;
			Smoke.enabled = false;
        }

        else if (player.money < 3)
        {
            notEnough.enabled = true;
        }
    }

    public void proximityMine()
    {
        if (player.money >= 3)
        {
            player.money -= 3;
			player.distractionAmount += 1;
			Mine.enabled = false;
        }

        else if (player.money < 3)
        {
            notEnough.enabled = true;
        }
    }

    public void weaponDMGU1()
    {
        if(player.money >= 2)
        {
            player.money -= 2;
            player.bulletDamage += 2;
        }

        else if(player.money < 2)
        {
            notEnough.enabled = true;
        }
    }
}
