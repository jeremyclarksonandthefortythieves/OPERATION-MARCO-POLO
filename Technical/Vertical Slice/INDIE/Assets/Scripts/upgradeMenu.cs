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

    private Image logo1,
                  logo2,
                  logo3,
                  logo4,
                  logo5;

    private Button silent,
                   smoke,
                   mine,
                   weap1,
                   weap2,
                   weap3,
                   weap4,
                   weap5;

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

        silent = GameObject.Find("Gadged1").GetComponent<Button>();
        smoke = GameObject.Find("Gadged2").GetComponent<Button>();
        mine = GameObject.Find("Gadged3").GetComponent<Button>();
        weap1 = GameObject.Find("WeaponDmg1").GetComponent<Button>();
        weap2 = GameObject.Find("WeaponDmg2").GetComponent<Button>();
        weap3 = GameObject.Find("WeaponDmg3").GetComponent<Button>();
        weap4 = GameObject.Find("WeaponDmg4").GetComponent<Button>();
        weap5 = GameObject.Find("WeaponDmg5").GetComponent<Button>();
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
            silent.enabled = false;
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
            smoke.enabled = false;
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
            mine.enabled = false;
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
            logo1.enabled = true;
            weap1.enabled = false;
        }

        else if(player.money < 2)
        {
            notEnough.enabled = true;
        }
    }

    public void weaponDMGU2()
    {
        if (player.money >= 3)
        {
            player.money -= 3;
            player.bulletDamage += 1;
            logo2.enabled = true;
            weap2.enabled = false;
        }

        else if (player.money < 3)
        {
            notEnough.enabled = true;
        }
    }

    public void weaponDMGU3()
    {
        if (player.money >= 4)
        {
            player.money -= 4;
            player.bulletDamage += 1;
            logo3.enabled = true;
            weap3.enabled = false;
        }

        else if (player.money < 4)
        {
            notEnough.enabled = true;
        }
    }

    public void weaponDMGU4()
    {
        if (player.money >= 5)
        {
            player.money -= 5;
            player.bulletDamage += 1;
            logo4.enabled = true;
            weap4.enabled = false;
        }

        else if (player.money < 5)
        {
            notEnough.enabled = true;
        }
    }

    public void weaponDMGU5()
    {
        if (player.money >= 6)
        {
            player.money -= 6;
            player.bulletDamage += 1;
            logo5.enabled = true;
            weap5.enabled = false;
        }

        else if (player.money < 6)
        {
            notEnough.enabled = true;
        }
    }

}
