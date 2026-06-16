using UnityEngine;

public class ItemGiver : MonoBehaviour
{
    public void GiveItem(Item item)
    {
        GameManager.instance.inventoryData.AddItem(item);
    }
}
