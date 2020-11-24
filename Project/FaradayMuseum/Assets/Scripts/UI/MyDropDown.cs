using UnityEngine;
using UnityEngine.EventSystems;

public class MyDropDown : GUIControl, IPointerClickHandler
{
    public GameObject dropdownBorder;
    public GameObject hint;

    public MyTrackableEventHandler myTrackableEventHandler;

    private bool myBool = true;

    public override string Name { get { return "DropDown"; } }

    private GameManager gameManager = GameManager.Instance;
    // Use this for initialization
    private void Awake()
    {
        gameManager.OnStateChange += HandleOnStateChange;
    }

    void OnDisable() {
        gameManager.OnStateChange -= HandleOnStateChange;
    }

    void Start()
    {
        if (gameManager.gameState == GameState.IN_GAME)
        {
            OnHide();
        }
        if (gameManager.gameState == GameState.MAIN_MENU)
        {
            OnHide();
        }
    }

    private void HandleOnStateChange(object sender, GameStateChangedEventArgs args)
    {
        switch (args.GameState)
        {
            case GameState.IN_GAME:
                OnHide();
                break;
            case GameState.MAIN_MENU:
                OnShow();
                break;
            default:
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        hint.SetActive(false);
        dropdownBorder.SetActive(false);

        myTrackableEventHandler.giveHint = false;
    }

    public void Cancel()
    {
        myTrackableEventHandler.giveHint = true;
    }
}
