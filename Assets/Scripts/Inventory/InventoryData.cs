using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    public event Action OnItemAdded;
    public event Action OnItemRemoved;
    
    
    // Use to add an item to the inventory
    public void AddItem(Item item)
    {
        inventory.Add(item);
        OnItemAdded?.Invoke();
    }
    
    // Use to remove an item from the inventory
    public void RemoveItem(Item item)
    {
        inventory.Remove(item);
        OnItemRemoved?.Invoke();
    }

    public static bool TryRemoveItem(Item item)
    {
        InventoryData instance = GameManager.instance.inventoryData;
        if (instance.inventory.Contains(item))
        {
            instance.RemoveItem(item);
            return true;
        }
        return false;
    }
}
