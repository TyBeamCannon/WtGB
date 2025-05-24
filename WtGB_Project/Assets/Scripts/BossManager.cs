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
    public Animator anim;

    bool isBossTurn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        Attack();
    }

    public void Attack()
    {
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
        anim.SetTrigger("die");
        TurnManager.Instance.BossDied();
    }

    public void EndTurn()
    {
        isBossTurn = false;
        TurnManager.Instance.NextTurn();
    }
}
