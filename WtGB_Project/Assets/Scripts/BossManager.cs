using UnityEngine;
using UnityEngine.AI;

public class BossManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public enum BossState{Idle, Attacking, Enraged, Dead}
    public BossState state = BossState.Idle;

    public float attackCooldown = 2f;
    public float cooldownTimer;

    public Transform player;
    public NavMeshAgent agent;
    public Animator anim;

    bool isBossTurn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        agent.GetComponent<NavMeshAgent>();
        cooldownTimer = 0f;
    }

    public void StartTurn()
    {
        if (state == BossState.Dead) return;

        isBossTurn = true;
        ExecuteTurn();
    }

    public void ExecuteTurn()
    {
        if(currentHealth <= maxHealth / 2 && state != BossState.Enraged)
        {
            state = BossState.Enraged;
            attackCooldown *= 0.75f;
        } 
        MoveToPlayer();
    }

    void MoveToPlayer()
    {
        if(agent != null && player != null)
        {
            agent.SetDestination(player.position);
            anim.SetBool("is moving", true);

            Invoke(nameof(Attack), 1.5f);
        }
        else
        {
            Attack();
        }
    }

    public void Attack()
    {
        agent.isStopped = true;
        anim.SetBool("isMoving", false);
        anim.SetTrigger("attack");

        EndTurn();
    }

    public void TakeDamage(int damage)
    {
        if (state == BossState.Dead) return;

        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }


    public void Die()
    {
        state = BossState.Dead;
        if (agent) agent.isStopped = true;
        anim.SetTrigger("die");
        TurnManager.Instance.BossDied();
    }

    public void EndTurn()
    {
        isBossTurn = false;
        TurnManager.Instance.NextTurn();
    }
}
