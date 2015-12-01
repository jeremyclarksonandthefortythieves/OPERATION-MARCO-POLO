using UnityEngine;
using System.Collections;

public class pauseMenuScript : MonoBehaviour {

    public Canvas pauseMenu;

    enemyAI enemyAI;
    PlayerControl playerControl;

    public bool canvasOn = false;

    /* private Button pauseExit,
                   pauseContinue;
     */
    // Use this for initialization
    void Start()
    {
        enemyAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<enemyAI>();
        playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();

        pauseMenu.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.enabled = true;
            canvasOn = true;
        }

        // If pause menu is on stop time in game and back to normal when canvas is off
        if (canvasOn)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

    }

    public void pauseExit()
    {
        Application.Quit();
    }

    public void pauseContinue()
    {
        pauseMenu.enabled = false;
        canvasOn = false;
    }
}
