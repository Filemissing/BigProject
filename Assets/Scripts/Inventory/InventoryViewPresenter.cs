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
    [SerializeField] private Item emptyItem;
    
    public void UpdateView(Item item)
    {
        title.text = item.title;
        description.text = item.description;
        itemInspect.UpdateItem(item);
    }

    private void Awake()
    {
        UpdateView(emptyItem);
        TryUpdateViewToDefault();
    }

    void TryUpdateViewToDefault()
    {
        if (title.text != "") return;
        
        if (GameManager.instance.inventoryData.inventory.Count > 0)
            UpdateView(GameManager.instance.inventoryData.inventory[0]);
        else
        {
            title.text = "";
            description.text = "";
            itemInspect.SetEmpty();
        }
    }
    
    // Event Bindings
    void OnEnable()
    {
        GameManager.instance.inventoryData.OnItemAdded += TryUpdateViewToDefault;
        GameManager.instance.inventoryData.OnItemRemoved += TryUpdateViewToDefault;
    }
    
    void OnDisable()
    {
        GameManager.instance.inventoryData.OnItemAdded -= TryUpdateViewToDefault;
        GameManager.instance.inventoryData.OnItemRemoved -= TryUpdateViewToDefault;
    }
}
