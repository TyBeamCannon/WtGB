using UnityEngine;
using System.Collections.Generic;



public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public enum TurnState { PlayerTurn, EnemyTurn, Win, Lose }

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

    }

    public void BeginPlayerTurn()
    {

    }

    public void BeginEnemyTurn()
    {

    }

    public void BossDied()
    {

    }

    public void PlayerDied()
    {

    }
}
