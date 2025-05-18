using UnityEngine;
using System.Collections;

public enum TurnState {PlayerTurn, EnemyTurn }

public class TurnManager : MonoBehaviour
{

    public TurnState currentState;
    bool playerHasActed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = TurnState.PlayerTurn;
        StartCoroutine(HandleTurn());
    }


    IEnumerator HandleTurn()
    {
        while (true)
        {
            switch (currentState)
            {
                case TurnState.PlayerTurn:
                    playerHasActed = false;
                    yield return new WaitUntil(() => playerHasActed);
                    currentState = TurnState.EnemyTurn;
                    break;

                case TurnState.EnemyTurn:
                    playerHasActed = false;
                    yield return new WaitUntil(() => playerHasActed);
                    currentState = TurnState.PlayerTurn;
                    break;
            }

            yield return null;
        }
    }
        public void OnPlayerActedComplete()
        {
          playerHasActed = true;
        }
    }
