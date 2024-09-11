using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Items", order = 1)]
public class ItemSO : ScriptableObject
{
    [SerializeField]
    private string itemName;
    public string ItemName
    {
        get => itemName; private set => itemName = value;
    }
    [SerializeField]
    private GameObject itemPrefab;
    public GameObject ItemPrefab
    {
        get => itemPrefab; private set => itemPrefab = value;
    }
    [SerializeField]
    private float dropRate;
    public float DropRate
    {
        get => dropRate; private set => dropRate = value;
    }
}