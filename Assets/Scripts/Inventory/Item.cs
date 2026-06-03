using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [Header("Basic Info")]
    public string title;
    [TextArea(3, 6)] public string description;
    
    [Header("Visuals")]
    public Sprite sprite;
    public GameObject model;
}
