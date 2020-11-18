using System;

// Game States
// for now we are only using these two
public enum GameState { MAIN_MENU, IN_GAME }

public class GameStateChangedEventArgs : EventArgs
{
    public GameState GameState { get; set; }
}

public delegate void OnGameStateChangeHandler(object sender, GameStateChangedEventArgs e);

public class GameManager
{
    protected GameManager() { }
    private static GameManager instance = null;
    public event OnGameStateChangeHandler OnStateChange;
    public GameState gameState { get; private set; }
    public Game currentGame { get; private set; }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }

    }

    public void SetGameState(GameState state)
    {
        gameState = state;
        if (OnStateChange != null)
        {
            GameStateChangedEventArgs args = new GameStateChangedEventArgs(){GameState = state};
            OnStateChange(this, args);
        }
    }

    public void SetCurrentGame(Game game)
    {
        currentGame = game;

    }

    public void StartGame(Game game){
        this.SetGameState(GameState.IN_GAME);
        this.SetCurrentGame(game);
    }

    public void ExitGame(){
        this.SetGameState(GameState.MAIN_MENU);
        this.SetCurrentGame(null);
    }

    public void OnApplicationQuit()
    {
        instance = null;
    }
}