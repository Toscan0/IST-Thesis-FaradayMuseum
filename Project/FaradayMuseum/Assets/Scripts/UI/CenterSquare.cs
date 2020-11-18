public class CenterSquare : GUIControl
{
    public override string Name { get { return "CenterSquare"; } }

    private GameManager gameManager = GameManager.Instance;

    private void Awake()
    {
        gameManager.OnStateChange += HandleOnStateChange;
    }

    void OnDisable() {
        gameManager.OnStateChange -= HandleOnStateChange;
    }

    private void HandleOnStateChange(object sender, GameStateChangedEventArgs args)
    {
        if (args.GameState == GameState.IN_GAME)
        {
            OnHide();
        }
    }
}
