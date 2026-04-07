using DG.Tweening;
using System.Collections;
using TMPro;
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

    private Animator _animator;

    private string _curAnimationKey;

    private bool _isDead;

    public Collider aliveCollider;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _currentHealth = startHealth;
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (_player.isDead || _isDead)
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
            SwitchAnimationState("Walk");
        }
    }

    void SwitchAnimationState(string key, bool forceAnimation = false)
    {
        if (_curAnimationKey != key || forceAnimation)
        {
            _animator.CrossFade(key, .1f);
            _curAnimationKey = key;
        }
    }

    IEnumerator AttackCoroutine()
    {
        SwitchAnimationState("Attack", true);
        _isAttackInProgress = true;
        transform.DOLookAt(_player.transform.position, .1f);
        yield return new WaitForSeconds(1.1f);
        var angle = Vector3.Angle(transform.forward, _player.transform.position - transform.position);
        if ((_player.transform.position - transform.position).magnitude < 2 && angle < 45)
        {
            _player.GetHit();
        }
        yield return new WaitForSeconds(1.6f);
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
        _isDead = true;
        aliveCollider.enabled = false;
        GetComponentInParent<Level>().EnemyKilled(this);
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
        SwitchAnimationState("Die");
        Destroy(gameObject, 3);
    }
}


public enum ActionState
{
    Idle,
    WalkingTowardsPlayer,
    Attacking,
    GettingHit,
}