using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _startPos;

    public float speed;

    private Weapon _weapon;

    public void StartBullet(Weapon weapon)
    {
        _weapon = weapon;
        _startPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _weapon.fxManager.PlayImpactPS(transform.position, transform.forward, Color.red);
            other.GetComponent<Enemy>().GetHit();
            Destroy(gameObject);
        }
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
