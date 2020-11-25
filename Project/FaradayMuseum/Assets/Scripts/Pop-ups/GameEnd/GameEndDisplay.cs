using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GameEndDisplay : PopUps
{
    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    [SerializeField]
    private TargetManager targetManager;
    [SerializeField]
    private SettingsManager settingsManager;
    [SerializeField]
    private GameEnd[] gameEnds;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI description;

        private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        string targetID = targetManager.TargetID;

        for (int i = 0; i < gameEnds.Length; i++)
        {
            if(gameEnds[i].artifactID == targetID)
            {
                title.text = gameEnds[i].title;
                description.text = gameEnds[i].description;
            }
        }

        if (firstTime)
        {
            OnPopUpEnable?.Invoke("End");
            firstTime = false;
        }
    }
    
    public void OnQuitButtonClick()
    {
        singleton.AddGameEvent(LogEventType.Click, "Close EndGame");

        StartCoroutine(ClosePopUp(animator, gameObject));
        OnPopUpDisable?.Invoke();
    }
}
