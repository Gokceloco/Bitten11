using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    private bool _isDoorLocked = true;
    private bool _isPlayerInRange;
    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && _isPlayerInRange && !_isDoorLocked)
        {
            OpenDoor();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInRange = false;
        }
    }
    void OpenDoor()
    {
        gameObject.SetActive(false);
    }
    public void UnlockDoor()
    {
        _isDoorLocked = false;
    }
}
