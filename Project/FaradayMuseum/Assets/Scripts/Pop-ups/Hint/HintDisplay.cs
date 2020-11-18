using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HintDisplay : PopUps
{
    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    [SerializeField]
    private TargetManager targetManager;
    [SerializeField]
    private SettingsManager settingsManager;
    [SerializeField]
    private Hint[] hint;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI description;

    private string ID;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        string expertiseLevel = settingsManager.GetExpertiseLevel();
        string targetID = targetManager.GetTargetID();

        for (int i = 0; i < hint.Length; i++)
        {
            if (hint[i].expertiseLevel == expertiseLevel &&
                hint[i].artifactID == targetID &&
                hint[i].ID == ID)
            {
                title.text = hint[i].title;
                description.text = hint[i].description;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void OnQuitButtonClick()
    {
        singleton.AddGameEvent(LogEventType.Click, "Close Hint");

        StartCoroutine(ClosePopUp(animator, gameObject));
    }

    public string GetID()
    {
        return ID;
    }

    public void SetID(string id)
    {
        ID = id;
    }
}
