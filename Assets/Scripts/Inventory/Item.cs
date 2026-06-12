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
    public Vector3 defaultRotation;
    public Vector3 defaultScale = Vector3.one;
}
