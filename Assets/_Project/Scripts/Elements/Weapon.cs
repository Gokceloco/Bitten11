using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    public GameDirector gameDirector;

    public Bullet bulletPrefab;

    public LevelManager levelManager;

    public Transform shootPosition;

    public float attackRate;
    private float _lastShootTime;    


    private void Update()
    {
        if (gameDirector.gameState == GameState.GamePlay 
            && Mouse.current.leftButton.isPressed 
            && Time.time - _lastShootTime > attackRate)
        {
            Shoot();
            _lastShootTime = Time.time;
        }
    }

    void Shoot()
    {
        var newBullet = Instantiate(bulletPrefab);

        levelManager.MakeChildToCurrentLevel(newBullet.transform);

        newBullet.transform.position = shootPosition.position;
        newBullet.transform.LookAt(newBullet.transform.position 
            + shootPosition.forward);
        gameDirector.audioManager.PlayShootAS();
    }
}
