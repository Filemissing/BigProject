using System;
using UnityEngine;

public class OnStartUnlockCursor : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
