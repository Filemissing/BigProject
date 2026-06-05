using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Item")]
    public Item item;
    
    [Header("References")]
    [SerializeField] private InventoryViewPresenter viewPresenter;
    [SerializeField] private Image image;

    public void OnClick()
    {
        if (item != null)
            viewPresenter.UpdateView(item);
    }

    public void UpdateSlot(Item newItem)
    {
        item = newItem;
        
        if (item != null)
            image.sprite = item.sprite;
    }

    public void Awake()
    {
        UpdateSlot(item);
    }
}
