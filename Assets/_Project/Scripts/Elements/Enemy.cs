using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public HealthBar healthBar;
    public int startHealth;
    private int _currentHealth;

    public ActionState enemyActionState;

    private Player _player;
    private NavMeshAgent _agent;

    private bool _isAttackInProgress;

    private Coroutine _attackCoroutine;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _currentHealth = startHealth;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_player.isDead)
        {
            _agent.isStopped = true;
            return;
        }

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
        if (enemyActionState == ActionState.Attacking && !_isAttackInProgress)
        {
            _agent.isStopped = true;
            _attackCoroutine = StartCoroutine(AttackCoroutine());
        }
        else if (enemyActionState == ActionState.WalkingTowardsPlayer && !_isAttackInProgress)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_player.transform.position);
        }
    }

    IEnumerator AttackCoroutine()
    {
        _isAttackInProgress = true;
        transform.DOLookAt(_player.transform.position, .1f);
        yield return new WaitForSeconds(2);
        var angle = Vector3.Angle(transform.forward, _player.transform.position - transform.position);
        if ((_player.transform.position - transform.position).magnitude < 2 && angle < 45)
        {
            _player.GetHit();
        }
        _isAttackInProgress = false;
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
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
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