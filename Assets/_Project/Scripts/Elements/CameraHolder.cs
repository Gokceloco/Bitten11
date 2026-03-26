using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform followObject;

    private void Update()
    {
        var pos = followObject.position;
        pos.y = 0;
        transform.position = pos;
    }
}
