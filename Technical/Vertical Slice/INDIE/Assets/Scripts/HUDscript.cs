using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDscript : MonoBehaviour {

    public Image crouch;
    public Image stand;

    private PlayerControl player;
    
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        crouch.enabled = false;
        stand.enabled = true;
	}
	
	// Update is called once per frame
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
