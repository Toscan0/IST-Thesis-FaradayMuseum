using UnityEngine;

using Malee;

[CreateAssetMenu(fileName = "TargetIDs Database", menuName = "Databases/TargetIDs Database")]
public class TargetIDsDatabase : ScriptableObject
{
    [Reorderable (sortable = false, paginate = false)]
    public TargetIDsArray targetIDs;

    [System.Serializable]
    public class TargetIDsArray : ReorderableArray<TargetID> { }

}
