using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Malee;

[CreateAssetMenu()]
public class AchievementDatabase : ScriptableObject
{
    [Reorderable (sortable = false, paginate = false)]
    public AchivementsArray achievements;

    [System.Serializable]
    public class AchivementsArray : ReorderableArray<Achievement> { }

}
