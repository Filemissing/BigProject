using UnityEngine;

public class SlotsHolderPresenter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform slotsHolder;

    void Awake()
    {
        UpdateSlots();
    }

    void UpdateSlots()
    {
        for (int i = 0; i < GameManager.instance.inventoryData.inventory.Count; i++)
        {
            slotsHolder.GetChild(i).GetComponent<InventorySlot>().UpdateSlot(GameManager.instance.inventoryData.inventory[i]);
        }
    }
    
    // Event Bindings
    void OnEnable()
    {
        GameManager.instance.inventoryData.OnItemAdded += UpdateSlots;
        GameManager.instance.inventoryData.OnItemRemoved += UpdateSlots;
    }
    
    void OnDisable()
    {
        GameManager.instance.inventoryData.OnItemAdded -= UpdateSlots;
        GameManager.instance.inventoryData.OnItemRemoved -= UpdateSlots;
    }
}
