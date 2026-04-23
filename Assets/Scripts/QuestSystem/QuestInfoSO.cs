using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "Scriptable Objects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    //serialize field so we can see in the inspector
    [field: SerializeField] public string id { get; private set; } // is what is going to be used to reference any specific quest across the system, needs to be unique to each quest

    [Header("General")]
    public string displayName;

    [Header("Requirements")]
    public QuestInfoSO[] questPrerequisites;

    // [Header("Item Requirements")]
    //public ItemRequirement[] requiredItems; // items needed to start this quest

    [Header("Steps")]
    public GameObject[] questStepPrefab;

    [Header("Rewards")]
    public string[] doorsToOpen;
    public ItemReward[] itemRewards;// wire up anything here in the inspector

    //ensure the id is always the name of the scriptable object asset
    private void OnValidate()
    {
#if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
#endif
    }

    [System.Serializable]
    public class ItemReward
    {
        public string itemID;   // will match id on your future ItemInfoSO
        public int quantity;    // how many of the item to give
    }
}

