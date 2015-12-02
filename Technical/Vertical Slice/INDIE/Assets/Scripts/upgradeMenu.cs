using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class upgradeMenu : MonoBehaviour {

    public Canvas upgradeScreen;
    public Image silencer,
                 Smoke,
                 Mine,
                 silencerDone,
                 smokeDone,
                 mineDone;

    public Text notEnough,
                tokens;

    private PlayerControl player;
    private float textTimer = 2;
    private bool boughtSilencer,
                 boughtSmoke,
                 boughtMine;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("menuController").GetComponent<PlayerControl>();
        notEnough.enabled = false;
        boughtMine = false;
        boughtSilencer = false;
        boughtSmoke = false;
        silencerDone.enabled = false;
        smokeDone.enabled = false;
        mineDone.enabled = false;

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

        if (boughtMine)
            mineDone.enabled = true;

        if (boughtSilencer)
            silencerDone.enabled = true;

        if (boughtSmoke)
            smokeDone.enabled = true;
	}

    public void Silencer()
    {
        if(player.money >= 3)
        {
            player.money -= 3;
            boughtSilencer = true;
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
            boughtSmoke = true;
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
            boughtMine = true;
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
            player.bulletDamage += 1;
        }

        else if(player.money < 2)
        {
            notEnough.enabled = true;
        }
    }
}
