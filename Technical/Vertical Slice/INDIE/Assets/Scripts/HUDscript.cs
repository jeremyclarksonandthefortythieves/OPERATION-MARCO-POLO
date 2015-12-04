using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDscript : MonoBehaviour {

    public Image crouch,
                 stand,
                 smoke,
                 mine;

    private PlayerControl player;
    
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        crouch.enabled = false;
        stand.enabled = true;
        mine.enabled = false;
        smoke.enabled = false;
    }

	void Update () {
        if (player.smokeAmount > 0)
        {
            smoke.enabled = true;
        }
        else
            smoke.enabled = false;

        if (player.distractionAmount > 0)
        {
            mine.enabled = true;
        }
        else
            mine.enabled = false;

        if (player.sneaking)
        {
            crouch.enabled = true;
            stand.enabled = false;
           
        }
            
        else
        {
            stand.enabled = true;
            crouch.enabled = false;
        }
	}
}
