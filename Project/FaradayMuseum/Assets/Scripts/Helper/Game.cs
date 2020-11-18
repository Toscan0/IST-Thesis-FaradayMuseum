using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Transform mainCanvas;

    private GameObject gameSpecificCanvas;

    public BoxBlur ARCameraBlur;

    public GameObject ObjectOverview;

    private GameManager gameManager = GameManager.Instance;

    public void StartGame()
    {
        if (gameManager.gameState == GameState.MAIN_MENU)
        {
            ARCameraBlur.Blur();
            ObjectOverview.SetActive(true);


            gameManager.StartGame(this);
        }
    }

    public void LostFocus()
    {
        if (gameManager.gameState == GameState.IN_GAME)
        {
            ARCameraBlur.ResetBlur();
        }
    }

    public void ExitGame()
    {
        ARCameraBlur.ResetBlur();
        gameManager.ExitGame();
    }
}
