using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryViewPresenter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private ItemInspect itemInspect;
    
    public void UpdateView(Item item)
    {
        title.text = item.title;
        description.text = item.description;
        itemInspect.UpdateItem(item);
    }

    private void Awake()
    {
        if (GameManager.instance.inventoryData.inventory.Count > 0)
            UpdateView(GameManager.instance.inventoryData.inventory[0]);
    }
}
