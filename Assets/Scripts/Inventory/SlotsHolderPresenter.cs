using UnityEngine;

public class SlotsHolderPresenter : MonoBehaviour
{
    [Header("TEMP")]
    [SerializeField] private InventoryData inventoryData;
    
    [Header("References")]
    [SerializeField] private RectTransform slotsHolder;

    void Awake()
    {
        for (int i = 0; i < inventoryData.inventory.Count; i++)
        {
            slotsHolder.GetChild(i).GetComponent<InventorySlot>().UpdateSlot(inventoryData.inventory[i]);
        }
    }
}
