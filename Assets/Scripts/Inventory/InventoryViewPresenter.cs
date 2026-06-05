using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryViewPresenter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Image image;
    
    public void UpdateView(Item item)
    {
        title.text = item.title;
        description.text = item.description;
        image.sprite = item.sprite;
    }

    private void Awake()
    {
        if (GameManager.instance.inventoryData.inventory.Count > 0)
            UpdateView(GameManager.instance.inventoryData.inventory[0]);
    }
}
