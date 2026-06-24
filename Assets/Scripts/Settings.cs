using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Scriptable Objects/Settings")]
public class Settings : ScriptableObject
{
    [Header("Controls")]
    public int mouseSensitivity = 50;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public KeyCode kickKey = KeyCode.Q;
    public KeyCode inventoryKey = KeyCode.I;
    public KeyCode journalKey = KeyCode.J;
    public KeyCode pauseKey = KeyCode.Escape;
    
    [Header("Audio")]
    public int masterVolume = 50;

    [Header("Misc")]
    public bool shutdownButton = false;
}
