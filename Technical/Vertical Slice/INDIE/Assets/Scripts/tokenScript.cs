using UnityEngine;
using System.Collections;

public class tokenScript : MonoBehaviour {

    private int killedEnemy = 0;
    private bool detected = false;
    private PlayerControl player;
    private enemyAI AI;

	// Use this for initialization
	void Start () {
        AI = GameObject.FindGameObjectWithTag("Enemey").GetComponent<enemyAI>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}