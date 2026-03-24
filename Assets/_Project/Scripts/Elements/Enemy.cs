using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthBar healthBar;
    public int startHealth;
    private int _currentHealth;

    public ActionState enemyActionState;

    private Player _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _currentHealth = startHealth;
    }

    private void Update()
    {
        //Make Desicion
        if ((_player.transform.position - transform.position).magnitude < 2)
        {
            enemyActionState = ActionState.Attacking;
        }
        else if ((_player.transform.position - transform.position).magnitude < 10)
        {
            enemyActionState = ActionState.WalkingTowardsPlayer;
        }



        //Take Action



    }

    public void GetHit()
    {
        _currentHealth--;
        healthBar.SetFillBar((float)_currentHealth / startHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}


public enum ActionState
{
    Idle,
    WalkingTowardsPlayer,
    Attacking,
    GettingHit,
}