using UnityEngine;

public class SlotsHolderPresenter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform slotsHolder;

    void Awake()
    {
        for (int i = 0; i < GameManager.instance.inventoryData.inventory.Count; i++)
        {
            slotsHolder.GetChild(i).GetComponent<InventorySlot>().UpdateSlot(GameManager.instance.inventoryData.inventory[i]);
        }
    }
}
