using UnityEngine;
using System.Collections.Generic;


public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public enum TurnState { PlayerTurn, EnemyTurn, Win, Lose }
    public TurnState state = TurnState.PlayerTurn;

    public List<EnemyController> enemies = new List<EnemyController>();
    int currentEnemyIndex = 0;

    public void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

     void Start()
    {
        BeginPlayerTurn();
    }

    public void NextTurn()
    {
        if(state == TurnState.PlayerTurn)
        {
            BeginEnemyTurn();
        }
        else if(state == TurnState.EnemyTurn)
        {
            currentEnemyIndex++;
            if(currentEnemyIndex < enemies.Count)
            {
                enemies[currentEnemyIndex].StartTurn();
            }
            else
            {
                BeginPlayerTurn();
            }
        }
    }

    public void BeginPlayerTurn()
    {
        state = TurnState.PlayerTurn;
        currentEnemyIndex = 0;

        Debug.Log("Player Turn has started");
    }

    public void BeginEnemyTurn()
    {
        state = TurnState.EnemyTurn;
        Debug.Log("EnemyTurn has started");

        enemies.RemoveAll(e => e == null);

        if(enemies.Count > 0)
        {
            currentEnemyIndex = 0;
            enemies[currentEnemyIndex].StartTurn();
        }
        else
        {
            BeginPlayerTurn();
        }
    }

    public void BossDied()
    {
        enemies.RemoveAll(e => e == null);
        if(enemies.Count == 0)
        {
            state = TurnState.Win;
            Debug.Log("You win!");
        }
    }

    public void PlayerDied()
    {
        state = TurnState.Lose;
        Debug.Log("You Lose!");
    }
}
