using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ExplanationDisplay : PopUps
{
    public static UsabilityTestsSingleton singleton = UsabilityTestsSingleton.Instance();

    [SerializeField]
    private TargetManager targetManager;
    [SerializeField]
    private SettingsManager settingsManager;
    [SerializeField]
    private Explanation[] explanation;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private TextMeshProUGUI description;

    private string ID;
    List<string> alreadyOpen = new List<string>();

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        EnableAndPopulate();
    }

    public string GetID()
    {
        return ID;
    }

    public void SetID(string id)
    {
        ID = id;

        if (IsActive)
        {
            EnableAndPopulate();
        }
    }

    public void OnQuitButtonClick()
    {
        singleton.AddGameEvent(LogEventType.Click, "Close Explanation");

        StartCoroutine(ClosePopUp(animator, gameObject));
        OnPopUpDisable?.Invoke();
    }

    private void EnableAndPopulate()
    {
        IsActive = true; 

        animator.SetTrigger("FadeIn");

        string expertiseLevel = settingsManager.GetExpertiseLevel();
        string targetID = targetManager.TargetID;

        for (int i = 0; i < explanation.Length; i++)
        {
            if (explanation[i].expertiseLevel == expertiseLevel &&
                explanation[i].artifactID == targetID &&
                explanation[i].ID == ID)
            {
                title.text = explanation[i].title;
                description.text = explanation[i].description;
            }
        }

        if (!alreadyOpen.Contains(ID))
        {
            OnPopUpEnable?.Invoke(ID);
            alreadyOpen.Add(ID);
        }
    }
}