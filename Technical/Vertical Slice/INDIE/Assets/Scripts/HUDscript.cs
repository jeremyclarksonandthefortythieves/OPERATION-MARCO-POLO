using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDscript : MonoBehaviour {

    public Image crouch;
    public Image stand;

    private PlayerControl player;
    
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        crouch.enabled = false;
        stand.enabled = true;
	}

	void Update () {
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
