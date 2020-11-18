using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedMenu : MonoBehaviour {

    public GameObject exit;
    GameManager gameManager;
    // Use this for initialization
    private void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.OnStateChange += HandleOnStateChange;
    }

    void Start () {
        if (gameManager.gameState == GameState.IN_GAME)
        {
            exit.SetActive(true);
        }
        if (gameManager.gameState == GameState.MAIN_MENU)
        {
            exit.SetActive(false);
        }
    }

    private void HandleOnStateChange()
    {
        if (gameManager.gameState == GameState.IN_GAME)
        {
            exit.SetActive(true);
        }
        if(gameManager.gameState == GameState.MAIN_MENU)
        {
            exit.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
}
