using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public float levelTime;

    private List<Enemy> _enemyList = new List<Enemy>();
    public void StartLevel()
    {
        _enemyList = new List<Enemy>(GetComponentsInChildren<Enemy>());
    }

    public void EnemyKilled(Enemy enemy)
    {
        _enemyList.Remove(enemy);
        if (_enemyList.Count == 0)
        {
            GetComponentInChildren<Door>().UnlockDoor();
        }
    }
}
