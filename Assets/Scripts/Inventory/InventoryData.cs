using System.Collections.Generic;
using UnityEngine;

public class InventoryData : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();

    
    
    // Use to add an item to the inventory
    public void AddItem(Item item)
    {
        inventory.Add(item);
    }
}
