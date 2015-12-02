using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class upgradeMenu : MonoBehaviour {

    public Canvas upgradeScreen;
    public Image silencer,
                 Mine,
                 Smoke;
    public Text notEnough;

    private PlayerControl player;
    private float textTimer = 2;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("menuController").GetComponent<PlayerControl>();
        notEnough.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	if(notEnough.enabled)
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
            
        }

        if(player.money < 3)
        {
            notEnough.enabled = true;
        }

    }

    public void smokeGrenade()
    {
        if (player.money < 3)
        {
            notEnough.enabled = true;
        }
    }

    public void proximityMine()
    {
        if (player.money < 3)
        {
            notEnough.enabled = true;
        }
    }

    public void weaponDMGUpgrader()
    {

    }
}
