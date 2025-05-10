using UnityEngine;
using System.Collections;

namespace CardStats
{
    public class CardExecutor : MonoBehaviour
    {
        public Card card;

        public void execute()
        {
            switch (card.cardType)
            {
                case Card.CardType.Attack:
                    ExecuteAttack();
                    break;
                case Card.CardType.Defense:
                    ExecuteDefense();
                    break;
                case Card.CardType.Counter:
                    ExecuteCounter();
                    break;
                case Card.CardType.Silly:
                    ExecuteSilly();
                    break;
            }
        }

        void ExecuteAttack()
        {
            int damage = Random.Range(card.damageMin, card.damageMax + 1);
            Debug.Log("Attack! Delt {damage} damage.");
        }

        void ExecuteDefense()
        {
            int block = Random.Range(card.defenseMin, card.defenseMax + 1);
            Debug.Log("Defended with {block} points.");
        }

        IEnumerator ExecuteCounter()
        {
            Debug.Log("Preparing counter... waiting {card.counterTime}s");
            yield return new WaitForSeconds(card.counterTime);

            if(Random.value < card.counterChance)
            {
                Debug.Log("Counter successful! Delt {card.counterDamage} damage within {card.counter.Range} range");
            }
            else
            {
                Debug.Log("Counter Failed");
            }
        }

        IEnumerator ExecuteSilly()
        {
            Debug.Log("Casting silly spell... ({card.castTime}s)}");
            yield return new WaitForSeconds(card.castTime);

            Debug.Log("Silly side effect triggered: {card.sideEffect}");
            yield return new WaitForSeconds(card.cooldown);

            Debug.Log("Silly Spell is ready again");
        }
    }

}