using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InitialExplicationDisplay : PopUps
{
    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    [SerializeField]
    private TargetManager targetManager;
    [SerializeField]
    private SettingsManager settingsManager;
    [SerializeField]
    private InitialExplication[] initialExplications;
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
        animator.SetTrigger("FadeIn");

        string expertiseLevel = settingsManager.GetExpertiseLevel();
        string targetID = targetManager.TargetID;

        for (int i = 0; i < initialExplications.Length; i++)
        {
            if(initialExplications[i].expertiseLevel == expertiseLevel &&
                initialExplications[i].artifactID == targetID)
            {
                title.text = initialExplications[i].title;
                description.text = initialExplications[i].description;
            }
        }

        if (firstTime)
        {
            OnPopUpEnable?.Invoke("IE");
            firstTime = false;
        }
        
    }

    public void Disable()
    {
        StartCoroutine(ClosePopUp(animator, gameObject));
    }

    public void OnQuitButtonClick()
    {
        singleton.AddGameEvent(LogEventType.Click, "Close initialExplanation");

        Disable();
        OnPopUpDisable?.Invoke();
    }
}
