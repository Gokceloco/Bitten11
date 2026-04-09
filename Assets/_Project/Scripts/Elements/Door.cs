using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    public Transform leftLeaf;
    public Transform rightLeaf;

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
        leftLeaf.DOLocalMoveZ(1.5f, .2f);
        rightLeaf.DOLocalMoveZ(-3.1f, .2f);
    }
    public void UnlockDoor()
    {
        _isDoorLocked = false;
    }
}
