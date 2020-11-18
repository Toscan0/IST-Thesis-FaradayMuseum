using UnityEngine;

public class ExtendedMenu : GUIControl {

    public override string Name { get { return "ExtendedMenu"; } }

    public GameObject exit;
    private GameManager gameManager = GameManager.Instance;


    private void Awake()
    {
        gameManager.OnStateChange += HandleOnStateChange;
    }

    void OnDisable() {
        gameManager.OnStateChange -= HandleOnStateChange;
    }


    void Start () {
        switch (gameManager.gameState)
        {
            case GameState.IN_GAME:
                exit.SetActive(true);
                break;
            case GameState.MAIN_MENU:
                exit.SetActive(false);
                break;
            default:
                break;
        }
    }

    private void HandleOnStateChange(object sender, GameStateChangedEventArgs args)
    {
        switch (args.GameState)
        {
            case GameState.IN_GAME:
                exit.SetActive(true);
                break;
            case GameState.MAIN_MENU:
                exit.SetActive(false);
                break;
            default:
                break;
        }
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
