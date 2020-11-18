public class LeaveMenu : GUIControl
{
    GameManager gameManager = GameManager.Instance;

    public void OnExitGame()
    {
        GUIManager.ShowAndHide("MainMenu", this);
        gameManager.currentGame.ExitGame();
    }
}
