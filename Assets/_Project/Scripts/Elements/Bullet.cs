using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _startPos;

    public float speed;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;

        if ((transform.position - _startPos).magnitude > 50)
        {
            Destroy(gameObject);
        }
    }
}
