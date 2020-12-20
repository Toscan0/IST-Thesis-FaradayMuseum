using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Malee;

[CreateAssetMenu(fileName = "Achievements Database", menuName = "Databases/Achievements Database")]
public class AchievementDatabase : ScriptableObject
{
    [Reorderable (sortable = false, paginate = false)]
    public AchivementsArray achievements;

    [System.Serializable]
    public class AchivementsArray : ReorderableArray<Achievement> { }

}
