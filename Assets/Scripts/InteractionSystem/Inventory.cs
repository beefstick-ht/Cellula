using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public bool HasKey = false;
    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame) HasKey = !HasKey;
    }
}
